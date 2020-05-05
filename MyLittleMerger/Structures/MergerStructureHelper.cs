using System.Text;

using MyLittleMerger.Structures.InstanceStructure;

namespace MyLittleMerger.Structures
{
    public static class MergerStructureHelper
    {
        public static string Print<TNode, TProperty>(ITypeReferenceMergerStructure<TNode, TProperty> item, string indent) where TNode : IMergerStructure
        {
            var stringBuilder = new StringBuilder();
            Print(item, stringBuilder, "", indent);
            return stringBuilder.ToString();
        }

        public static void Print<TNode, TProperty>(ITypeReferenceMergerStructure<TNode, TProperty> item, StringBuilder stringBuilder, string indent, string additive) where TNode : IMergerStructure
        {
            stringBuilder.AppendLine($"{indent}{item}");
            foreach (var property in item.Properties)
            {
                var itemIndent = indent + additive;
                if (property.Key.IsArray)
                {
                    stringBuilder.AppendLine($"{itemIndent}{property.Key}");
                    itemIndent += "  ";
                }
                if (property.Value == null)
                {
                    continue;
                }
                foreach (var propertyValue in property.Value)
                {
                    stringBuilder.AppendLine($"{itemIndent}{propertyValue}");
                }
            }
            foreach (var node in item.Nodes)
            {
                var itemIndent = indent + additive;
                if (node.Key.IsArray)
                {
                    stringBuilder.AppendLine($"{itemIndent}{node.Key}");
                    itemIndent += "  ";
                }
                if (node.Value == null)
                {
                    continue;
                }
                foreach (var nodeValue in node.Value)
                {
                    nodeValue.Print(stringBuilder, itemIndent, additive);
                }
            }
        }
    }
}
