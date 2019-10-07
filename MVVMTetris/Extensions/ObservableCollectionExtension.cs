using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMTetris.Extensions
{
    /// <summary>
    /// class <c>ObservableCollectionExtension</c>
    /// Extends observable collection with method that allow for groups of items
    /// to be added or removed.
    /// </summary>
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// Method <c>Remove<T></c>
        /// Overloads remove so that it takes a condition. Any item within the collection
        /// for which the condition is true is removed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int Remove<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
        
        /// <summary>
        /// Method <c>AddRange()</c>
        /// Adds all the items in an Enumerable collection to the ObservableCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="range"></param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            range.ToList().ForEach(collection.Add);
        }    
    }
}
