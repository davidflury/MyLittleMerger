using System.Text;

namespace MyLittleMerger.Structures
{
    public interface IMergerStructure
    {
        string Print();

        void Print(StringBuilder stringBuilder, string indent = null, string additive = null);
    }
}