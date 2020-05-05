using System.Collections.Generic;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger.Structures.InstanceStructure
{
    public interface ITypeReferenceMergerStructure<TNode, TProperty> where TNode : IMergerStructure
    {
        Dictionary<MergerTypeStructure, List<TNode>> Nodes { get; }

        Dictionary<MergerTypeProperty, List<TProperty>> Properties { get; }
    }
}