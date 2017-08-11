using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTestUtil
{
    public class CollectionComparator<T>
    {
        public CollectionComparator(double precision = 0.00001)
        {
            Precision = precision;
        }
        /// <summary>
        /// Precision used to compare double and float.
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// usage:
        /// var comparator = new CollectionComparator<string>();
        /// var result = comparator.IsEqual(list1, list2);
        /// Assert.IsTrue(result.Result, result.Message);
        /// </summary>
        public CompareResult IsEqual(ICollection<T> firstList, ICollection<T> secondList)
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
                var itemInSecond = secondList.ElementAt(i);
                if (typeof(T) == typeof(double) || typeof(T) == typeof(float))
                {
                    double double1 = (double)(object)itemInFirst;
                    double double2 = (double)(object)itemInSecond;
                    if (Math.Abs(double1 - double2) > Precision) 
                    {
                        return new CompareResult() { Result = false, Message = "Values at index " + i + " differ. First collection: " + itemInFirst.ToString() + ", second: " + secondList.ElementAt(i) };
                    }
                }
                else if (!itemInFirst.Equals(itemInSecond)) 
                {
                    return new CompareResult() { Result = false, Message = "Values at index " + i + " differ. First collection: " + itemInFirst.ToString() + ", second: " + secondList.ElementAt(i) };
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
