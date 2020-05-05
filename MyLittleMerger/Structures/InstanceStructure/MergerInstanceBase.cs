using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger.Structures.InstanceStructure
{
    public abstract class MergerInstanceBase
    {
        public virtual object Value { get; set; }

        public bool HasValue => Value != null;

        public MergerTypeBase TypeBase { get; }

        protected MergerInstanceBase(MergerTypeBase typeBase)
        {
            TypeBase = typeBase;
        }

        public override string ToString()
        {
            var print = $"{TypeBase}";
            if (TypeBase.IsArray && Value is object[] array)
            {
                print += $"({array.Length})";
            }
            return print;
        }

        public void UpdateValue(object instance, MergerInstanceBase instanceValue)
        {
            TypeBase.Property.SetValue(instance, instanceValue.Value);
        }
    }
}