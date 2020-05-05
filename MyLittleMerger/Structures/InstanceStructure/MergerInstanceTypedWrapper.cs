namespace MyLittleMerger.Structures.InstanceStructure
{
    public class MergerInstanceTypedWrapper<T>
    {
        public T Value
        {
            get => (T)Instance.Value;
            set => Instance.Value = value;
        }

        public MergerInstanceBase Instance;

        public MergerInstanceTypedWrapper(MergerInstanceBase instance)
        {
            Instance = instance;
        }
    }
}