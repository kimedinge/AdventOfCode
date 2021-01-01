using System;
using System.Collections.Generic;
using System.Linq;

namespace BagageHandling
{
    public class BagageValidation
    {
        private static List<BagRule> bagRules;
        public static int CountValidBags(string bagageRules)
        {
            string carryBag = "shiny gold";
            bagRules = GetRules(bagageRules);
            var possibleBags = bagRules.Where(bag => bag.CanHaveBagInside(carryBag)).Select(s => s).ToList();
            var newPossibleBags = new List<BagRule>();
            while (true)
            {
                foreach (var bag in possibleBags)
                {
                    newPossibleBags.AddRange(bagRules.Where(rule => rule.CanHaveBagInside(bag.Name.TrimStart().TrimEnd())).Select(s => s).ToList());
                }

                if (newPossibleBags.Any() && !newPossibleBags.All(bag => possibleBags.Contains(bag)))
                {
                    possibleBags.AddRange(newPossibleBags.Where(bag => !possibleBags.Contains(bag)));
                }
                else
                {
                    break;
                }
            }

            return possibleBags.Count();
        }

        public static int CountNeededContainedBags(string bagageRules)
        {
            string carryBag = "shiny gold";
            bagRules = GetRules(bagageRules);
            var bags = bagRules.Where(bag => bag.Name.Equals(carryBag)).ToList();
            var bag = bagRules.Where(bag => bag.Name.Equals(carryBag)).FirstOrDefault();
            PopulateContainedBags(bag);
            var sum = bag.SumRule-1;
            return sum;
        }

        private static void PopulateContainedBags(BagRule bag)
        {
            bag.BagsPossibleInside.Keys.ToList()
                .ForEach(k => k.BagsPossibleInside = bagRules.Where(br => br.Name == k.Name).FirstOrDefault().BagsPossibleInside);
            foreach (var bagRule in bag.BagsPossibleInside.Keys)
            {
                PopulateContainedBags(bagRule);
            }
        }

        private static List<BagRule> GetRules(string bagageRules)
        {
            return bagageRules.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(rule => new BagRule
                                        {
                                            Name = rule.Split("contain").FirstOrDefault().Replace("bags", string.Empty).Trim(),
                                            BagsPossibleInside = rule.Split("contain").LastOrDefault().Split(", ", StringSplitOptions.RemoveEmptyEntries)
                                                                .Where(rule => !rule.Equals(" no other bags."))
                                                                .Select(bag => new
                                                                {
                                                                    Key = new BagRule { Name = bag.Replace("bags", string.Empty).Replace("bag", string.Empty).Replace(".", string.Empty)[(bag.TrimStart().IndexOf(' ') + 1)..].Trim() },
                                                                    Value = int.Parse(bag.TrimStart().Substring(0, bag.TrimStart().IndexOf(' ')))
                                                                })
                                                                .ToDictionary(b => b.Key, b => b.Value)
                                        }).ToList();
        }
    }
}
