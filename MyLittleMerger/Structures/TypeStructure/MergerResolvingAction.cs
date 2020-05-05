using System;
using System.Linq;
using MyLittleMerger.Structures.CompareStructure;
using MyLittleMerger.Structures.InstanceStructure;

namespace MyLittleMerger.Structures.TypeStructure
{
    public abstract class BaseMergerResolvingAction
    {
        protected MergerResolvingOptions Options { get; }

        protected BaseMergerResolvingAction(MergerResolvingOptions options)
        {
            Options = options;
        }

        public MergerInstanceBase Execute(MergerCompareResult compareResult, MergerInstanceBase instance, MergerInstanceBase compare, out bool executed)
        {
            if (!Options.RunOnResults.Contains(compareResult))
            {
                executed = false;
                return instance;
            }
            executed = true;
            return ExecuteInternal(instance, compare);
        }

        protected abstract MergerInstanceBase ExecuteInternal(MergerInstanceBase instance, MergerInstanceBase compare);
    }

    public class MergerResolvingAction<TK> : BaseMergerResolvingAction
    {
        public Func<MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>> Resolver { get; }
    
        public MergerResolvingAction(Func<MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>> resolver) : this(resolver, new MergerResolvingOptions())
        {
        }
    
        public MergerResolvingAction(Func<MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>> resolver, MergerResolvingOptions options) : base(options)
        {
            Resolver = resolver;
        }

        protected override MergerInstanceBase ExecuteInternal(MergerInstanceBase instance, MergerInstanceBase compare)
        {
            return Resolver(new MergerInstanceTypedWrapper<TK>(instance), new MergerInstanceTypedWrapper<TK>(compare)).Instance;
        }
    }
}