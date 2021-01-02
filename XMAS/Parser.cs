using System;
using System.Collections.Generic;
using System.Linq;

namespace XMAS
{
    public static class Parser
    {

        public static ulong FindFirstNotSum(string numbers)
        {
            var preamble = 25;
            var numbersList = numbers.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(s=>UInt64.Parse(s)).ToList();
            var sumValid = false;
            for(int i = 0; i< numbersList.Count; i++)
            {
                var index = i + preamble;
                while(index>=numbersList.Count)
                {
                    index--;
                }
                var sum = numbersList[index];
                var take = preamble + i > numbersList.Count ? numbersList.Count - i : preamble;
                var possibleNumbers = numbersList.GetRange(i, take);
                sumValid = false;
                foreach (var number in possibleNumbers)
                {
                    for(int p = 0; p<possibleNumbers.Count; p++)
                    {
                        var testsum = possibleNumbers[p] + number;
                        if(testsum == sum)
                        {
                            sumValid = true;
                            break;
                        }
                    }
                    if(sumValid)
                    {
                        break;
                    }
                }
                if (!sumValid)
                {
                    return sum;
                }
            }
            return 0;
        }

        public static ulong GetContigousSumFirstLast(string numbers)
        {
            var invalidSum = FindFirstNotSum(numbers);
            var numbersList = numbers.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(s => UInt64.Parse(s)).ToList();
            ulong foundSum = 0;
            while(foundSum == 0)
            {
                for(int num = 2; num < numbersList.Count; num++)
                {
                    for(int i = 0; i < numbersList.Count; i++)
                    {
                        var take = num + i > numbersList.Count ? numbersList.Count - i : num;
                        if(take < 2)
                        {
                            break;
                        }
                        var possibleNums = numbersList.GetRange(i, take);
                        ulong possibleSum = 0;
                        foreach(var possNum in possibleNums)
                        {
                            possibleSum += possNum;
                        }
                        if(possibleSum == invalidSum)
                        {
                            var sortedPossible = possibleNums.OrderBy(o => o);
                            foundSum = sortedPossible.First() + sortedPossible.Last();
                            break;
                        }
                    }
                    if(foundSum != 0)
                    {
                        break;
                    }
                }
            }
            return foundSum;
        }
    }
}
