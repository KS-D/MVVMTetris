using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMTetris.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static int Remove<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
        
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            range.ToList().ForEach(collection.Add);
        }    
    }
}
