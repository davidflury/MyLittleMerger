using System;
using System.Collections.Generic;
using System.Reflection;
using MyLittleMerger.Structures.InstanceStructure;

namespace MyLittleMerger.Structures.TypeStructure
{
    public abstract class MergerTypeBase
    {
        public string Identifier => !string.IsNullOrEmpty(Name) ? Name : Type.Name;

        private string Name { get; }

        public PropertyInfo Property { get; }

        public Type Type { get; }

        public bool IsArray { get; }

        public bool IsNullable { get; }

        public BaseMergerResolvingAction ResolvingAction;

        protected MergerTypeBase(PropertyInfo property, Type type, string name, bool isArray = false,
            bool isNullable = false)
        {
            Name = name;
            Property = property;
            Type = type;
            IsArray = isArray;
            IsNullable = isNullable;
        }

        public override bool Equals(object obj)
        {
            if (obj is MergerTypeBase other)
            {
                return Equals(Identifier, other.Identifier)
                       && Type == other.Type
                       && Equals(IsArray, other.IsArray)
                       && Equals(IsNullable, other.IsNullable);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode()
                   ^ Type.GetHashCode()
                   ^ IsArray.GetHashCode()
                   ^ IsNullable.GetHashCode();
        }

        public override string ToString()
        {
            var text = $"{Identifier} - {Type.Name}";
            if (IsArray)
            {
                text += "[]";
            }
            if (IsNullable)
            {
                text += "?";
            }
            return text;
        }

        public void RegisterResolver<TK>(Func<MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>, MergerInstanceTypedWrapper<TK>> resolver, MergerResolvingOptions options)
        {
            if (ResolvingAction != null)
            {
                throw new InvalidOperationException($"Resolver was already set for type '{this}': {ResolvingAction}");
            }
            ResolvingAction = new MergerResolvingAction<TK>(resolver, options);
        }

        public abstract bool TryFind(string wanted, out MergerTypeBase result);

        public abstract bool TryFind(PropertyInfo wanted, out MergerTypeBase result);
    }
}