using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    class Day6
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day6.txt");

        public string Part1()
        {
            string message = string.Empty;
            List<string>[] answer = new List<string>[8];
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = new List<string>();
            }

            var messages = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < messages.Length; i++)
            {
                var line = messages[i].ToArray();
                for (int j = 0; j < line.Length; j++)
                {
                    answer[j].Add(line[j].ToString());
                }
            }
            foreach (var col in answer)
            {
                message += col
                    .GroupBy(c => c)
                    .OrderByDescending(c => c.Count())
                    .Take(1)
                    .Select(c => c.Key)
                    .FirstOrDefault();
            }
            return message;
        }

        public string Part2()
        {
            string message = string.Empty;
            List<string>[] answer = new List<string>[8];
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = new List<string>();
            }

            var messages = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < messages.Length; i++)
            {
                var line = messages[i].ToArray();
                for (int j = 0; j < line.Length; j++)
                {
                    answer[j].Add(line[j].ToString());
                }
            }
            foreach (var col in answer)
            {
                message += col
                    .GroupBy(c => c)
                    .OrderBy(c => c.Count())
                    .Take(1)
                    .Select(c => c.Key)
                    .FirstOrDefault();
            }
            return message;
        }
    }
}
