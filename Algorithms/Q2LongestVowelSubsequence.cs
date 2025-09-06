using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Algorithms
{
    public static class Q2LongestVowelSubsequence
    {
        public static string LongestVowelSubsequenceAsJson(List<string> words)
        {
            if (words == null || words.Count == 0)
                return "[]";

            var output = new List<object>(words.Count);

            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word))
                {
                    output.Add(new { word = word ?? "", sequence = "", length = 0 });
                    continue;
                }

                string bestSeq = "";   
                string curSeq  = "";  

                foreach (char ch in word)
                {
                    char lo = char.ToLower(ch);

                    if (lo == 'a' || lo == 'e' || lo == 'i' || lo == 'o' || lo == 'u')
                    {
                        curSeq += ch; 
                    }
                    else
                    {
                        if (curSeq.Length > bestSeq.Length)
                            bestSeq = curSeq;

                        curSeq = "";
                    }
                }

                if (curSeq.Length > bestSeq.Length)
                    bestSeq = curSeq;

                output.Add(new
                {
                    word = word,
                    sequence = bestSeq,
                    length = bestSeq.Length
                });
            }

            return JsonSerializer.Serialize(output);
        }
    }
}
