using MyLittleMerger.Structures.InstanceStructure;

namespace MyLittleMerger.Structures.CompareStructure
{
    public class MergerCompareProperty : MergerCompareBase<MergerInstanceProperty>
    {
        public override MergerCompareResult Result => MergerCompareEngine.Evaluate(Instance, Compare);

        public MergerCompareProperty(MergerInstanceProperty instance, MergerInstanceProperty compare) : base(instance, compare)
        {
        }
    }
}