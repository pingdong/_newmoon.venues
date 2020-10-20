using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PingDong.Testings
{
    /// <summary>
    /// Compare two flat objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PropertiesComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T expected, T actual)
        {
            return AreEqual(expected, actual);
        }
        
        public int GetHashCode(T parameterValue)
        {
            return Tuple.Create(parameterValue).GetHashCode();
        }

        private static bool AreEqual(object objectA, object objectB)
        {
            // if any side is null
            if (objectA == null || objectB == null)
                return Equals(objectA, objectB);

            // both sides are not null
            var objectType = objectA.GetType();
            var properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead);

            foreach (var propertyInfo in properties)
            {
                var valueA = propertyInfo.GetValue(objectA, null);
                var valueB = propertyInfo.GetValue(objectB, null);

                // Is Primitive, ValueType or implementing IComparable
                if (CanCompareDirectly(propertyInfo.PropertyType))
                {
                    if (!AreValuesEqual(valueA, valueB))
                        return false;
                }

                // if it implements IEnumerable, then scan any items
                else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    // null check
                    if ((valueA == null && valueB != null) || (valueA != null && valueB == null))
                        return false;

                    if (valueA != null)
                    {
                        var collectionItems1 = ((IEnumerable) valueA).Cast<object>();
                        var collectionItems2 = ((IEnumerable) valueB).Cast<object>();
                        var count1 = collectionItems1.Count();
                        var count2 = collectionItems2.Count();

                        // Ensure have same number of items
                        if (count1 != count2)
                            return false;

                        // Compare each item
                        for (int i = 0; i < count1; i++)
                        {
                            var collectionItem1 = collectionItems1.ElementAt(i);
                            var collectionItem2 = collectionItems2.ElementAt(i);
                            var collectionItemType = collectionItem1.GetType();

                            if (CanCompareDirectly(collectionItemType))
                            {
                                if (!AreValuesEqual(collectionItem1, collectionItem2))
                                    return false;
                            }
                            else if (!AreEqual(collectionItem1, collectionItem2))
                            {
                                return false;
                            }
                        }
                    }
                }

                else if (propertyInfo.PropertyType.IsClass)
                {
                    if (!AreEqual(propertyInfo.GetValue(objectA, null), propertyInfo.GetValue(objectB, null)))
                        return false;
                }
            }

            return true;
        }

        private static bool CanCompareDirectly(Type type)
        {
            return type.IsPrimitive || type.IsValueType || typeof(IComparable).IsAssignableFrom(type);
        }

        private static bool AreValuesEqual(object valueA, object valueB)
        {
            var comparer = valueA as IComparable;

            if (valueA == null && valueB == null)
                return true;

            if (valueA == null || valueB == null)
                return false; // one of the values is null

            if (comparer != null && comparer.CompareTo(valueB) != 0)
                return false; // the comparison using IComparable failed

            var valueType = valueA.GetType();

            if (valueType.IsGenericType)
            {
                if (!AreEqual(valueA, valueB))
                    return false;
            }
            else
            {
                return Equals(valueA, valueB);
            }

            return true;
        }
    }
}
