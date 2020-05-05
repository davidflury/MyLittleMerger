using MyLittleMerger.Structures.InstanceStructure;

namespace MyLittleMerger.Structures.CompareStructure
{
    public static class MergerCompareEngine
    {
        public static MergerCompareResult Evaluate(MergerInstanceBase instance, MergerInstanceBase compare)
        {
            if (Equals(instance?.Value, compare?.Value))
            {
                return MergerCompareResult.Equal;
            }
            if ((instance != null && instance.HasValue) && (compare == null || !compare.HasValue))
            {
                return MergerCompareResult.OnlyLeft;
            }
            if ((instance == null || !instance.HasValue) && (compare != null && compare.HasValue))
            {
                return MergerCompareResult.OnlyRight;
            }
            return MergerCompareResult.Different;
        }
    }
}