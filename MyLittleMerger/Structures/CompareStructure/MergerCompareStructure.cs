using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MyLittleMerger.Helpers;
using MyLittleMerger.Structures.InstanceStructure;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger.Structures.CompareStructure
{
    public class MergerCompareStructure : MergerCompareBase<MergerInstanceStructure>, IMergerStructure, ITypeReferenceMergerStructure<MergerCompareStructure, MergerCompareProperty>
    {
        public MergerTypeStructure Structure => Instance?.TypeStructure ?? Compare?.TypeStructure;

        public MergerOptions Options => Structure.Options;

        public Dictionary<MergerTypeStructure, List<MergerCompareStructure>> Nodes { get; }

        public Dictionary<MergerTypeProperty, List<MergerCompareProperty>> Properties { get; set; }

        public override MergerCompareResult Result => Evaluate();

        internal MergerCompareStructure(MergerInstanceStructure instance, MergerInstanceStructure compare) : base(instance, compare)
        {
            if (instance != null && compare != null && !Equals(instance.TypeStructure, compare.TypeStructure))
            {
                throw new ArgumentException(string.Format("Type structures do not match:{0}{1}{0}Versus:{0}{2}", Environment.NewLine, instance, compare));
            }
            Nodes = new Dictionary<MergerTypeStructure, List<MergerCompareStructure>>(Structure.Nodes.Count);
            Properties = new Dictionary<MergerTypeProperty, List<MergerCompareProperty>>(Structure.Properties.Count);
            foreach (var typeNode in Structure.Nodes)
            {
                var instanceNodes = Instance?.Nodes[typeNode];
                var compareNodes = Compare?.Nodes[typeNode];
                var nodes =
                    new List<MergerCompareStructure>(Math.Max(instanceNodes?.Count ?? 0, compareNodes?.Count ?? 0));
                for (var i = 0; i < nodes.Capacity; i++)
                {
                    instanceNodes.TryGetByIndex(i, out var instanceNode);
                    compareNodes.TryGetByIndex(i, out var compareNode);
                    nodes.Add(new MergerCompareStructure(instanceNode, compareNode));
                }
                Nodes.Add(typeNode, nodes);
            }

            foreach (var typeProperty in Structure.Properties)
            {
                var instanceProperties = Instance?.Properties[typeProperty];
                var compareProperties = Compare?.Properties[typeProperty];
                var properties =
                    new List<MergerCompareProperty>(Math.Max(instanceProperties?.Count ?? 0, compareProperties?.Count ?? 0));
                for (var i = 0; i < properties.Capacity; i++)
                {
                    instanceProperties.TryGetByIndex(i, out var instanceProperty);
                    compareProperties.TryGetByIndex(i, out var compareProperty);
                    properties.Add(new MergerCompareProperty(instanceProperty, compareProperty));
                }
                Properties.Add(typeProperty, properties);
            }
        }

        private MergerCompareResult Evaluate()
        {
            if (!Nodes.Any() && !Properties.Any())
            {
                return MergerCompareEngine.Evaluate(Instance, Compare);
            }
            var results = Properties.SelectMany(k => k.Value).Select(p => p.Result)
                .Union(Nodes.SelectMany(k => k.Value).Select(n => n.Result))
                .Distinct()
                .ToList();
            return results.Count > 1 ? MergerCompareResult.Different : results.First();
        }

        public override void Resolve(object instance)
        {
            foreach (var property in Properties.SelectMany(p => p.Value))
            {
                property.Resolve(instance);
            }
            foreach (var node in Nodes.SelectMany(n => n.Value))
            {
                node.Resolve(instance);
            }
            base.Resolve(instance);
        }

        public string Print()
        {
            return MergerStructureHelper.Print(this, Options.Indent);
        }

        public void Print(StringBuilder stringBuilder, string indent = null, string additive = null)
        {
            MergerStructureHelper.Print(this, Options.Indent);
        }
    }

    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class MergerCompareStructure<T> : MergerCompareStructure
    {
        public MergerCompareStructure(MergerInstanceStructure<T> instance, MergerInstanceStructure<T> compare) : base(instance, compare)
        {
        }
    }
}
