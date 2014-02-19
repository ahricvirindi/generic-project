using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;

namespace GenericProject.Core.Data
{
    public static class Extensions
    {
        private static Random rand = new Random();

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        public static TResult IsNull<TTarget, TResult>(this TTarget target, Func<TTarget, TResult> callback, TResult valueIfNull = default(TResult))
            where TTarget: class { return (target == null) ? valueIfNull : callback(target); }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> target)
        {
            if (target == null)
                return Enumerable.Empty<T>();

            return target;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp) { return source.IndexOf(toCheck, comp) >= 0; }

        /// <summary>
        /// Copies over properties from one or more source object into the target object.  Properties are cached for so performance hit is minimal
        /// This extension method is a Generic wrapper for the Omu.ValueInjector library
        /// </summary>
        /// <typeparam name="T">Type of the destination object</typeparam>
        /// <param name="target">Target object (can be simply new T())</param>
        /// <param name="source">Source object(s) to copy from</param>
        /// <returns>Updated target</returns>
        public static T InjectFrom<T>(this T target, params object[] source) { return (T)StaticValueInjecter.InjectFrom(target, source); }

        public static T SelectRandom<T>(this IEnumerable<T> enumerable)
        {
            var index = rand.Next(0, enumerable.Count() - 1);
            return enumerable.ElementAt(index);
        }

        public static IEnumerable<T> SelectRandom<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.OrderByRandom().Take(count);
        }

        public static IEnumerable<T> OrderByRandom<T>(this IEnumerable<T> enumerable) { return enumerable.OrderBy(o => rand.Next()); }
    }
}