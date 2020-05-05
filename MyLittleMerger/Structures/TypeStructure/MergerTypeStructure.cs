using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using MyLittleMerger.Helpers;

namespace MyLittleMerger.Structures.TypeStructure
{
    public class MergerTypeStructure : MergerTypeBase, IMergerStructure
    {
        public MyLittleMerger Merger { get; }

        public MergerOptions Options => Merger.Options ?? new MergerOptions();

        public List<MergerTypeStructure> Nodes { get; }

        public List<MergerTypeProperty> Properties { get; }

        internal MergerTypeStructure(MyLittleMerger merger, PropertyInfo property, Type type, string name, bool isArray = false, bool isNullable = false)
            : this(merger, type, property, name, isArray, isNullable)
        {
        }

        internal MergerTypeStructure(MyLittleMerger merger, Type type, PropertyInfo propertyInfo = null, string name = "", bool isArray = false, bool isNullable = false)
            : base(propertyInfo, type, name, isArray, isNullable)
        {
            Merger = merger;
            Properties = new List<MergerTypeProperty>();
            Nodes = new List<MergerTypeStructure>();
            foreach (var property in type.GetProperties())
            {
                Type underlyingPropertyType;
                var propertyIsArray = false;
                var propertyIsNullable = false;
                if (property.PropertyType.IsArray)
                {
                    propertyIsArray = true;
                    underlyingPropertyType = property.PropertyType.GetElementType();
                }
                else if (property.PropertyType.TryGetUnderlyingNullable(out underlyingPropertyType))
                {
                    propertyIsNullable = true;
                }
                else
                {
                    underlyingPropertyType = property.PropertyType;
                }
                if (Options.NodeAssemblies.Any(a => Equals(a.FullName, underlyingPropertyType?.Assembly.FullName)))
                {
                    Nodes.Add(new MergerTypeStructure(merger, property, underlyingPropertyType, property.Name, isArray: propertyIsArray, isNullable: propertyIsNullable));
                }
                else
                {
                    Properties.Add(new MergerTypeProperty(property, underlyingPropertyType, property.Name, isArray: propertyIsArray, isNullable: propertyIsNullable));
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is MergerTypeStructure other &&
                   base.Equals(obj)
                   && Nodes.SequenceEqual(other.Nodes)
                   && Properties.SequenceEqual(other.Properties);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                   ^ Nodes.GetHashCodeAggregated()
                   ^ Properties.GetHashCodeAggregated();
        }

        public override string ToString()
        {
            return "+ " + base.ToString();
        }

        public string Print()
        {
            var stringBuilder = new StringBuilder();
            Print(stringBuilder, string.Empty, Options.Indent);
            return stringBuilder.ToString();
        }

        public void Print(StringBuilder stringBuilder, string indent, string additive)
        {
            stringBuilder.AppendLine($"{indent}{this}");
            foreach (var property in Properties)
            {
                stringBuilder.AppendLine($"{indent}{additive}{property}");
            }
            foreach (var node in Nodes)
            {
                node.Print(stringBuilder, indent + additive, additive);
            }
        }

        public override bool TryFind(string wanted, out MergerTypeBase result)
        {
            var parts = wanted.Split(new[] { '.' }, 2);
            var head = parts.FirstOrDefault();
            var tail = parts.SecondOrDefault();
            if (Equals(head, Identifier))
            {
                if (string.IsNullOrEmpty(tail))
                {
                    result = this; 
                    return true;
                }
                foreach (var property in Properties)
                {
                    if (property.TryFind(tail, out result))
                    {
                        return true;
                    }
                }
                foreach (var node in Nodes)
                {
                    if (node.TryFind(tail, out result))
                    {
                        return true;
                    }
                }
            }
            result = null;
            return false;
        }

        public override bool TryFind(PropertyInfo wanted, out MergerTypeBase result)
        {
            if (Equals(Property, wanted))
            {
                result = this;
                return true;
            }
            foreach (var property in Properties)
            {
                if (property.TryFind(wanted, out result))
                {
                    return true;
                }
            }
            foreach (var node in Nodes)
            {
                if (node.TryFind(wanted, out result))
                {
                    return true;
                }
            }
            result = null;
            return false;
        }
    }

    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class MergerTypeStructure<T> : MergerTypeStructure
    {
        public MergerTypeStructure(MyLittleMerger<T> merger) : base(merger, typeof(T))
        {

        }
    }
}
