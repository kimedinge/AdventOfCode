using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomsDeclarationForm
{
    public static class CustomDeclarationController
    {
        public static int CountAnswers(string declarationFormAnswers)
        {
            var sumAnswered = 0;
            var groups = declarationFormAnswers.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var group in groups)
            {
                sumAnswered += group.Replace("\r\n", string.Empty).Distinct().Count();
            }
            return sumAnswered;
        }

        public static int CountEveryoneAnswers(string declarationFormAnswers)
        {
            var sumAnswered = 0;
            var groups = declarationFormAnswers.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var group in groups)
            {
                var persons = group.Split(Environment.NewLine, StringSplitOptions.None);
                var answers = new Dictionary<char, int>();
                foreach(var answer in persons)
                {
                    answer.ToList().ForEach(a => {
                        if (answers.ContainsKey(a))
                        {
                            answers[a]++;
                        }
                        else
                        {
                            answers.Add(a, 1);
                        }
                    });
                    
                }
                sumAnswered += answers.Where(k => k.Value == persons.Count()).Count();
            }
            return sumAnswered;
        }
    }
}
