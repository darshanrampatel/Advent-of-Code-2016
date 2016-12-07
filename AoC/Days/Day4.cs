using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    class Day4
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day4.txt");

        public string Part1()
        {
            var validRooms = 0;
            var rooms = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string room in rooms)
            {
                var parts = room.Split('[');
                var sectorID = Int32.Parse(parts[0].Split('-').LastOrDefault());
                var letters = parts[0].Replace("-", "");
                var checksum = parts[1].Replace("]", "");
                var groupedLetters = letters
                    .Where(c => Char.IsLetter(c))
                    .GroupBy(c => c)
                    .OrderByDescending(c => c.Count())
                    .ThenBy(c => c.Key)
                    .Select(c => char.ToString(c.Key))
                    .Take(5)
                    .ToArray()
                    .Aggregate((prev, current) => prev + current);
                if (groupedLetters.Equals(checksum))
                {
                    validRooms += sectorID;
                }
            }
            return "Sum: " + validRooms.ToString(); ;
        }

        public char GetNextChar(char c)
        {
            if (c.Equals('z'))
            {
                return 'a';
            }
            char newChar = (Char)(Convert.ToUInt16(c) + 1); ;
            return newChar;
        }

        public string Part2()
        {
            var decryptedRooms = new List<string>();
            var rooms = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string room in rooms)
            {
                var decryptedRoomName = "";
                var parts = room.Split('[');
                var sectorID = Int32.Parse(parts[0].Split('-').LastOrDefault());
                var letters = parts[0].Replace("-", " ").Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c));
                foreach (char l in letters)
                {
                    char s = l;
                    if (!l.Equals(' '))
                    {
                        for (int i = 0; i < sectorID % 26; i++)
                        {
                            s = GetNextChar(s);
                        }
                    }
                    decryptedRoomName += s;
                }
                decryptedRoomName += sectorID.ToString();
                decryptedRooms.Add(decryptedRoomName);
            }
            decryptedRooms = decryptedRooms.Where(room => room.Contains("north")).ToList();
            return string.Join(Environment.NewLine, decryptedRooms);
        }
    }
}
