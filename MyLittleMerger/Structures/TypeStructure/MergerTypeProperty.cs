using System;
using System.Reflection;

#pragma warning disable 659
namespace MyLittleMerger.Structures.TypeStructure
{
    public class MergerTypeProperty : MergerTypeBase
    {
        public MergerTypeProperty(PropertyInfo property, string name, bool isArray = false, bool isNullable = false) :
            this(property, property.PropertyType, name, isArray, isNullable)
        {
        }

        public MergerTypeProperty(PropertyInfo property, Type type, string name, bool isArray = false, bool isNullable = false)
            : base(property, type, name, isArray, isNullable)
        {
        }
        
        public override bool Equals(object obj)
        {
            return obj is MergerTypeProperty &&
                   base.Equals(obj);
        }

        public override string ToString()
        {
            return "- " + base.ToString();
        }

        public override bool TryFind(string wanted, out MergerTypeBase result)
        {
            if (Equals(wanted, Identifier))
            {
                result = this;
                return true;
            }
            result = null;
            return false;
        }

        public override bool TryFind(PropertyInfo wanted, out MergerTypeBase result)
        {
            if (Equals(wanted, Property))
            {
                result = this;
                return true;
            }
            result = null;
            return false;
        }
    }
}