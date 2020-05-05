using System.Reflection;
using System.Collections.Generic;
using MyLittleMerger.Structures.InstanceStructure;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger
{
    public class MergerOptions
    {
        /// <summary>
        /// Adds the assembly of the given type to the node assembly list at initialization.
        /// </summary>
        public bool AutoAddTypeAssembly { get; set; }

        /// <summary>
        /// Defines a list of assemblies which is used to check if a property should be treated as value or substructure.
        /// </summary>
        public List<Assembly> NodeAssemblies { get; set; }

        /// <summary>
        /// Indent for printing <see cref="MergerTypeStructure"/> and <see cref="MergerInstanceStructure"/>.
        /// </summary>
        public string Indent { get; set; }

        public MergerOptions()
        {
            NodeAssemblies = new List<Assembly>();
            Indent = "    ";
        }
    }
}
