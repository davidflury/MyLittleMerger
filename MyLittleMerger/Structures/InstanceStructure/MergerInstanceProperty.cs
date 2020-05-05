using MyLittleMerger.Helpers;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger.Structures.InstanceStructure
{
    public class MergerInstanceProperty : MergerInstanceBase
    {
        public MergerTypeProperty TypeProperty { get; }

        public MergerInstanceProperty(MergerTypeProperty typeProperty, object value) : base(typeProperty)
        {
            TypeProperty = typeProperty;
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + $": {TypeHelper.ToStringOrNull(Value)}"; ;
        }
    }
}