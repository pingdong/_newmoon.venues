using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace PingDong.DDD
{
    public abstract class Enumeration : IComparable
    {
        #region ctor

        protected Enumeration() {}
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; }

        public int Id { get; }

        #endregion

        #region Base Methods

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Enumeration left, Enumeration right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            
            return left.Equals(right);
        }

        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }

        public static bool operator <(Enumeration left, Enumeration right)
        {
            return (Compare(left, right) < 0);
        }

        public static bool operator >(Enumeration left, Enumeration right)
        {
            return (Compare(left, right) > 0);
        }

        public static bool operator <=(Enumeration left, Enumeration right)
        {
            return (Compare(left, right) < 0);
        }

        public static bool operator >=(Enumeration left, Enumeration right)
        {
            return (Compare(left, right) > 0);
        }

        #endregion

        #region Methods

        public static int Compare(Enumeration left, Enumeration right)
        {
            if (ReferenceEquals(left, right))
                return 0;

            if (ReferenceEquals(left, null))
                return -1;
            
            return left.CompareTo(right);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var field in fields)
            {
                var instance = new T();
                var locatedValue = field.GetValue(instance) as T;

                if (locatedValue != null)
                    yield return locatedValue;
            }
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        #endregion

        #region IComparable

        public int CompareTo(object obj) => Id.CompareTo(((Enumeration) obj).Id);

        #endregion
    }
}
