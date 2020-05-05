using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MyLittleMerger.Helpers;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger.Structures.InstanceStructure
{
    public class MergerInstanceStructure : MergerInstanceBase, IMergerStructure, ITypeReferenceMergerStructure<MergerInstanceStructure, MergerInstanceProperty>
    {
        public MergerTypeStructure TypeStructure { get; }

        public MergerOptions Options => TypeStructure.Options;

        public Dictionary<MergerTypeStructure, List<MergerInstanceStructure>> Nodes { get; }

        public Dictionary<MergerTypeProperty, List<MergerInstanceProperty>> Properties { get;  }

        internal MergerInstanceStructure(MergerTypeStructure typeStructure, object value) : base(typeStructure)
        {
            TypeStructure = typeStructure;
            Value = value;
            Nodes = new Dictionary<MergerTypeStructure, List<MergerInstanceStructure>>(TypeStructure.Nodes.Count);
            Properties = new Dictionary<MergerTypeProperty, List<MergerInstanceProperty>>(TypeStructure.Properties.Count);
            foreach (var node in TypeStructure.Nodes)
            {
                var nodeValue = HasValue ? node.Property.GetValue(Value) : null;
                if (node.IsArray)
                {
                    var nodes = ((object[])nodeValue)?
                        .Select(itemValue => new MergerInstanceStructure(node, itemValue)).ToList();
                    Nodes.Add(node, nodes);
                }
                else
                {
                    Nodes.AddFirst(node, new MergerInstanceStructure(node, nodeValue));
                }
            }
            foreach (var property in TypeStructure.Properties)
            {
                var propertyValue = HasValue ? property.Property.GetValue(Value) : null;
                if (property.IsArray)
                {
                    var properties = ((object[])propertyValue)?
                        .Select(itemValue => new MergerInstanceProperty(property, itemValue)).ToList();
                    Properties.Add(property, properties);
                }
                else
                {
                    Properties.AddFirst(property, new MergerInstanceProperty(property, propertyValue));
                }
            }
        }

        public override string ToString()
        {
            var print = base.ToString();
            if (!Nodes.Any() && !Properties.Any())
            {
                print += $": {TypeHelper.ToStringOrNull(Value)}";
            }
            return print;
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
    public class MergerInstanceStructure<T> : MergerInstanceStructure
    {
        public MergerInstanceStructure(MergerTypeStructure<T> structure, T value) : base(structure, value)
        {
        }
    }
}
