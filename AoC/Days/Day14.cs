using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Days
{
    class Day14
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day14.txt");

        public string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        List<int> Keys = new List<int>();

        Dictionary<int, string> computedHashes = new Dictionary<int, string>();

        void GetTriplet(string hash, int index, bool part1)
        {
            var match3 = Regex.Match(hash, "([a-z\\d])\\1\\1");

            if (match3 != null && match3.Success)
            {
                var c = match3.Value[0];

                var newHash = string.Empty;
                for (int i = index + 1; i <= index + 1000; i++)
                {
                    if (!computedHashes.ContainsKey(i))
                    {
                        newHash = CalculateMD5Hash(input + i.ToString());
                        if (!part1)
                        {
                            for (int j = 0; j < 2016; j++)
                            {
                                newHash = CalculateMD5Hash(newHash);
                            }
                        }
                        computedHashes[i] = newHash;
                    }
                    else
                    {
                        newHash = computedHashes[i];
                    }

                    var match5 = Regex.Match(newHash, "(" + c + ")\\1\\1\\1\\1");
                    if (match5 != null && match5.Success)
                    {
                        Keys.Add(index);
                        //System.Diagnostics.Debug.WriteLine($"Found Key: {index}");
                        break;
                    }
                }
            }
        }

        public string Part1()
        {
            var password = string.Empty;
            computedHashes = new Dictionary<int, string>();
            var count = 0;
            while (Keys.Count < 64)
            {
                if (!computedHashes.ContainsKey(count))
                {
                    password = CalculateMD5Hash(input + count.ToString());
                    computedHashes[count] = password;
                }
                else
                {
                    password = computedHashes[count];
                }

                GetTriplet(password, count, true);
                count++;
            }
            var answer = $"Index of 64th Key: {Keys.Last()}";
            return answer;
        }

        public string Part2()
        {
            var password = string.Empty;
            computedHashes = new Dictionary<int, string>();
            var count = 0;
            while (Keys.Count < 64)
            {
                if (!computedHashes.ContainsKey(count))
                {
                    password = CalculateMD5Hash(input + count.ToString());
                    for (int i = 0; i < 2016; i++)
                    {
                        password = CalculateMD5Hash(password);
                    }
                    computedHashes[count] = password;
                }
                else
                {
                    password = computedHashes[count];
                }

                GetTriplet(password, count, false);
                count++;
            }
            var answer = $"Index of 64th Key: {Keys.Last()}";
            return answer;
        }
    }
}
