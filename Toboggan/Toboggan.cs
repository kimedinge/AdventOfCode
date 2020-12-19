using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Toboggan
{
    public class TobogganRide
    {
        public List<Tuple<int,int>> Slopes {get; set;}
        private List<int> TreeCount = new List<int>();

        public List<int> Ride()
        {
            TextReader tx = File.OpenText("C:\\Repos\\AdventOfCode\\AdventOfCode\\Toboggan\\map.txt");
            string[] map = tx.ReadToEnd().Split("\r\n" );
            var s=0;
            var treeCount = 0;
            foreach(var slope in Slopes)
            {
                for(var i = 0; i<map.Count(); i+=slope.Item2)
                {
                    var m = map[i];
                    if(m[s].Equals('#'))
                    {
                        treeCount++;
                    }
                    s+=slope.Item1;
                    if(s>=m.Length)
                    {
                        s -= m.Length;
                    }
                }
                TreeCount.Add(treeCount);
                treeCount = 0;
                s=0;
            }
            return TreeCount;
        }
    }
}
