using MyLittleMerger.Helpers;
using MyLittleMerger.Structures.InstanceStructure;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger.Structures.CompareStructure
{
    public abstract class MergerCompareBase<T> where T : MergerInstanceBase
    {
        public T Instance { get; }

        public T Compare { get; }

        public abstract MergerCompareResult Result { get; }

        public MergerTypeBase TypeBase => (Instance ?? Compare).TypeBase;

        protected MergerCompareBase(T instance, T compare)
        {
            Instance = instance;
            Compare = compare;
        }

        public virtual void Resolve(object instance)
        {
            var executed = false;
            var result = TypeBase.ResolvingAction?.Execute(Result, Instance, Compare, out executed);
            if (executed && result is T merged)
            {
                Instance.UpdateValue(instance, merged);
            }
        }

        public override string ToString()
        {
            return $"{Instance ?? Compare} - {TypeHelper.ToStringOrNull(Instance?.Value)} <> {TypeHelper.ToStringOrNull(Compare?.Value)}: {Result}";
        }
    }
}