using System;
using System.Reflection;
using MyLittleMerger.Structures.CompareStructure;
using MyLittleMerger.Structures.InstanceStructure;
using MyLittleMerger.Structures.TypeStructure;

namespace MyLittleMerger
{
    public class MyLittleMerger
    {
        public Type Type { get; private set; }

        public bool Initialized { get; internal set; }

        public MergerOptions Options { get; internal set; }

        protected MyLittleMerger(Type type)
        {
            Type = type;
        }
    }

    public class MyLittleMerger<T> : MyLittleMerger
    {
        public MergerTypeStructure<T> Structure { get; internal set; }

        public MyLittleMerger() : base(typeof(T))
        {
        }

        public MyLittleMerger(MergerOptions mergerOptions) : this()
        {
            Options = mergerOptions;
            Initialize();
        }

        public MyLittleMerger<T> Initialize(MergerOptions mergerOptions)
        {
            Options = mergerOptions;
            Initialize();
            return this;
        }

        /// <summary>
        /// Initializes the structure.
        /// </summary>
        /// <returns>Itself for fluent usage</returns>
        public MyLittleMerger<T> Initialize()
        {
            if (Options == null)
            {
                Options = new MergerOptions();
            }
            if (Options.AutoAddTypeAssembly)
            {
                RegisterNodeAssembly(typeof(T).Assembly);
            }
            Structure = new MergerTypeStructure<T>(this);
            Initialized = true;
            return this;
        }

        /// <summary>
        /// Registers a resolving method by the property path.
        /// </summary>
        /// <param name="path">Path to the property like <example>ContactObject.Addresses.Street</example></param>
        /// <param name="resolver">Resolving action to execute on conflicts.</param>
        /// <param name="options">Optional options</param>
        /// <returns>Itself for fluent usage</returns>

        public MyLittleMerger<T> RegisterResolver<TK>(string path, Func<MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>> resolver, MergerResolvingOptions options = null)
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("Merger not initialized");
            }
            if (options == null)
            {
                options = new MergerResolvingOptions();
            }
            if (!Structure.TryFind(path, out var result))
            {
                throw new ArgumentException($"Could not find property with path '{path}'.");
            }
            result.RegisterResolver(resolver, options);
            return this;
        }

        /// <summary>
        /// Registers a resolving method by the property.
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="resolver">Resolving action to execute on conflicts.</param>
        /// <param name="options">Optional options</param>
        /// <returns>Itself for fluent usage</returns>

        public MyLittleMerger<T> RegisterResolver<TK>(PropertyInfo property, Func<MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>> resolver, MergerResolvingOptions options = null)
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("Merger not initialized");
            }
            if (options == null)
            {
                options = new MergerResolvingOptions();
            }
            if (!Structure.TryFind(property, out var result))
            {
                throw new ArgumentException($"Could not find property '{property.Name}'.");
            }
            result.RegisterResolver<TK>(resolver, options);
            return this;
        }

        /// <summary>
        /// Registers a resolving method by the property path.
        /// </summary>
        /// <param name="path">Path to the property like <example>ContactObject.Addresses.Street</example></param>
        /// <param name="resolver">Resolving action to execute on conflicts.</param>
        /// <param name="options">Optional options</param>
        /// <returns>Itself for fluent usage</returns>

        public MyLittleMerger<T> RegisterResolver(string path, Func<MergerInstanceTypedWrapper<object>, MergerInstanceTypedWrapper<object>, MergerInstanceTypedWrapper<object>> resolver, MergerResolvingOptions options = null)
        {
            return RegisterResolver<object>(path, resolver, options);
        }

        /// <summary>
        /// Registers a resolving method by the property.
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="resolver">Resolving action to execute on conflicts.</param>
        /// <param name="options">Optional options</param>
        /// <returns>Itself for fluent usage</returns>

        public MyLittleMerger<T> RegisterResolver(PropertyInfo property, Func<MergerInstanceTypedWrapper<object>, MergerInstanceTypedWrapper<object>, MergerInstanceTypedWrapper<object>> resolver, MergerResolvingOptions options = null)
        {
            return RegisterResolver<object>(property, resolver, options);
        }

        public void Resolve(T instance, T compare)
        {
            var compareStructure = new MergerCompareStructure<T>(
                new MergerInstanceStructure<T>(Structure, instance),
                new MergerInstanceStructure<T>(Structure, compare));
            compareStructure.Resolve(instance);
        }

        private void RegisterNodeAssembly(Assembly assembly)
        {
            Options.NodeAssemblies.Add(assembly);
        }
    }
}
