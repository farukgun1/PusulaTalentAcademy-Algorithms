using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Algorithms
{
    public static class Q1MaxIncreasingSubarray
    {

        public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
        {
           
            if (numbers == null || numbers.Count == 0)
                return "[]";

           
            int curStart = 0;
            int curLen = 1;
            int curSum = numbers[0];

            
            int bestStart = 0;
            int bestLen = 1;
            int bestSum = numbers[0];

            
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    
                    curLen++;
                    curSum += numbers[i];
                }
                else
                {
                    
                    if (curSum > bestSum )
                    {
                        bestSum = curSum;
                        bestLen = curLen;
                        bestStart = i - curLen;
                    }

                 
                    curStart = i;
                    curLen = 1;
                    curSum = numbers[i];
                }
            }

      
            if (curSum > bestSum)
            {
                bestSum = curSum;
                bestLen = curLen;
                bestStart = numbers.Count - curLen;
            }

          
            if (bestLen <= 0)
                return "[]";

            var result = new List<int>(bestLen);
            for (int k = 0; k < bestLen; k++)
                result.Add(numbers[bestStart + k]);

            return JsonSerializer.Serialize(result);
        }
    }
}
