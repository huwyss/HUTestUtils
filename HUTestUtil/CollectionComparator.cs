using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTestUtil
{
    public class CollectionComparator<T>
    {
        /// <summary>
        /// usage:
        /// var result = comparator.Compare(list1, list2);
        /// Assert.IsTrue(result.Result, result.Message);
        /// </summary>
        public CompareResult Compare(ICollection<T> firstList, ICollection<T> secondList)
        {
            if ((firstList == null && secondList != null) ||
                firstList != null && secondList == null)
            {
                return new CompareResult() { Result = false, Message = "One collection is null, the other has elements." };
            }

            if (firstList == null && secondList == null)
            {
                return new CompareResult() { Result = true, Message = "Both collections are null." };
            }

            if (firstList.Count != secondList.Count)
            {
                return new CompareResult() { Result = false, Message = "Sizes differ. First size: " + firstList.Count + ", second size: " + secondList.Count };
            }

            int i = 0;
            foreach (var itemInFirst in firstList)
            {
                if (!itemInFirst.Equals(secondList.ElementAt(i))) 
                {
                    return new CompareResult() { Result = false, Message = "Values index " + i + " differ. First collection: " + itemInFirst.ToString() + ", second: " + secondList.ElementAt(i) };
                }
                i++;
            }
            return new CompareResult() { Result = true, Message = "" };
        }
    }

    public struct CompareResult
    {
        public bool Result;
        public string Message;
    }
}
