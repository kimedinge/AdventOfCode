using System;
using System.Collections.Generic;
using System.Linq;

namespace ElvesAccountingSum2020
{
    public static class Accounting
    {
        #region Day 1
        public static int GetProduct2020ForTwo(IEnumerable<int> list)
        {
            var sortedList = list.OrderBy(i=>i).ToArray();
            var sum = 0;
            var expectedSum = 2020;
            var i = 0;
            var j = sortedList.Count() - 1;
            while (i<j && sum != expectedSum)
            {
                var a = sortedList[i];
                var b = sortedList[j];
                sum = a + b;
                if(sum > expectedSum)
                {
                    j--;
                }
                if(sum < expectedSum)
                {
                    i++;
                }
            }
            var product = sortedList[i]*sortedList[j];
            return product;
        }

        public static int GetProduct2020ForThree(IEnumerable<int> list)
        {
            var sortedList = list.OrderBy(i => i).ToArray();
            var sortedListHash = sortedList.ToHashSet();
            var sum = 0;
            var expectedSum = 2020;
            var i = 0;
            var j = i + 1;
            var product = 0;
            while(i < j && j < sortedList.Length && product == 0)
            {
                var a = sortedList[i];
                var b = sortedList[j];
                var c = expectedSum - a - b;
                sum = a + b + c;
                var exists = sortedListHash.Contains(c);
                if (exists)
                {
                    product = a * b * c;
                }
                else
                {
                    if(sum > expectedSum || !exists)
                    {
                        i++;
                        j = i + 1;
                    }
                }
            }
            
            return product;
        }
        #endregion
    }
}
