using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC.Days
{
    class Day16
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day16.txt");

        string DragonCurve(string a)
        {
            var b = new string(a.Reverse().ToArray());
            b = b.Replace("0", "x").Replace("1", "0").Replace("x", "1").ToString();
            return string.Concat(a, "0", b);
        }

        Tuple<bool, string> IsChecksum(string data)
        {
            int stringLength = data.Length;
            StringBuilder checksum = new StringBuilder(stringLength / 2 + 1);
            if (stringLength % 2 == 0)
            {
                for (int i = 0; i < stringLength; i += 2)
                {
                    if (data[i] == data[i + 1])
                    {
                        checksum.Append("1");
                    }
                    else
                    {
                        checksum.Append("0");
                    }
                }
            }
            return new Tuple<bool, string>(checksum.Length > 0 ? (checksum.Length % 2 == 0 ? false : true) : false, checksum.ToString());
        }

        string CalculateCorrectChecksum(string data, int diskLength)
        {
            while (data.Length < diskLength)
            {
                data = DragonCurve(data);
            }

            data = data.Substring(0, diskLength);

            Tuple<bool, string> isChecksum = new Tuple<bool, string>(false, data);
            while (!isChecksum.Item1)
            {
                isChecksum = IsChecksum(isChecksum.Item2);
            }

            return isChecksum.Item2;
        }

        public string Part1()
        {
            return CalculateCorrectChecksum(input, 272);
        }

        public string Part2()
        {
            return CalculateCorrectChecksum(input, 35651584);
        }
    }
}
