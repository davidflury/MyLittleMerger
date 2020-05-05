using MyLittleMerger.Structures.CompareStructure;

namespace MyLittleMerger
{
    public class MergerResolvingOptions
    {
        /// <summary>
        /// Defines on which <see cref="MergerCompareResult"/> a resolving action is executed.
        /// Default: [<see cref="MergerCompareResult.Different" />]
        /// </summary>
        public MergerCompareResult[] RunOnResults { get; set; }

        public MergerResolvingOptions()
        {
            RunOnResults = new[] { MergerCompareResult.Different };
        }
    }
}