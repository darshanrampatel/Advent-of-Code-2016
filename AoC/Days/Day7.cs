using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    class Day7
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day7.txt");

        public string Part1()
        {
            int IPCount = 0;
            var IPs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string ip in IPs)
            {
                var splits = ip.Split('[', ']');
                var insideSquareBrackets = splits.Where((item, index) => index % 2 != 0).ToList();
                var outsideSquareBrackets = splits.Where((item, index) => index % 2 == 0).ToList();
                bool inside = false;
                bool outside = false;
                foreach (var insidePart in insideSquareBrackets)
                {
                    if (IsABBA(insidePart))
                    {
                        inside = true;
                        break;
                    }
                }
                foreach (var outsidePart in outsideSquareBrackets)
                {
                    if (IsABBA(outsidePart))
                    {
                        outside = true;
                        break;
                    }
                }
                if (!inside && outside)
                {
                    IPCount++;
                }
            }
            return "TLS IP Count: " + IPCount.ToString();
        }

        private bool IsABBA(string part)
        {
            bool result = false;
            if (part.Length >= 4)
            {
                var fourCharParts = new List<string>();
                for (int i = 0; i <= (part.Length - 4); i++)
                {
                    fourCharParts.Add(part.Substring(i, 4));
                }
                foreach (string fourCharPart in fourCharParts)
                {
                    if (fourCharPart == new string(fourCharPart.Reverse().ToArray()))
                    {
                        if (fourCharPart.Distinct().Count() > 1)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        private List<string> GetABAs(string part)
        {
            List<string> result = new List<string>();
            if (part.Length >= 3)
            {
                var threeCharParts = new List<string>();
                for (int i = 0; i <= (part.Length - 3); i++)
                {
                    threeCharParts.Add(part.Substring(i, 3));
                }
                foreach (string threeCharPart in threeCharParts)
                {
                    if (threeCharPart == new string(threeCharPart.Reverse().ToArray()))
                    {
                        if (threeCharPart.Distinct().Count() > 1)
                        {
                            result.Add(threeCharPart);
                        }
                    }
                }
            }
            return result;
        }

        public string Part2()
        {
            int IPCount = 0;
            var IPs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string ip in IPs)
            {
                var splits = ip.Split('[', ']');
                var insideSquareBrackets = splits.Where((item, index) => index % 2 != 0).ToList();
                var outsideSquareBrackets = splits.Where((item, index) => index % 2 == 0).ToList();
                bool ssl = false;
                var ABAs = new List<string>();
                foreach (var outsidePart in outsideSquareBrackets)
                {
                    ABAs.AddRange(GetABAs(outsidePart));
                }
                foreach (var ABA in ABAs)
                {
                    var BABtoFind = string.Concat(ABA[1],ABA[0],ABA[1]);
                    foreach (var insidePart in insideSquareBrackets)
                    {
                        if (insidePart.Contains(BABtoFind))
                        {
                            ssl = true;
                            break;
                        }
                    }
                }
                if (ssl)
                {
                    IPCount++;
                }
            }
            return "SSL IP Count: " + IPCount.ToString();
        }
    }
}
