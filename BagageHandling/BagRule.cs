using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BagageHandling
{
    public class BagRule
    {
        public Dictionary<BagRule, int> BagsPossibleInside = new Dictionary<BagRule, int>();
        public string Name { get; set; }

        public bool CanHaveBagInside(string bagName)
        {
            foreach(var key in BagsPossibleInside.Keys)
            {
                if (key.Name.TrimStart().TrimEnd() == bagName)
                    return true;
            }
            return false;
        }

        public int SumRule
        {
            get
            {
                if (BagsPossibleInside.Count == 0)
                    return 1;
                return BagsPossibleInside.Sum(kp=>kp.Value * kp.Key.SumRule) + 1;
            }
        }
    }
}
