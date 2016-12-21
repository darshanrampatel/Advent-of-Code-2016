using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }

        public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
        {
            return current.Previous ?? current.List.Last;
        }
    }
    class Day19
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day19.txt");

        class Elf
        {
            public int ID { get; set; }
        }

        public string Part1()
        {
            var Elves = new LinkedList<Elf>();
            for (int i = 0; i < Int32.Parse(input); i++)
            {
                Elves.AddLast(new Elf() {
                   ID =  i + 1
                });
            }

            var elf = Elves.First;
            while (Elves.Count > 0)
            {
                var nextElf = elf.NextOrFirst();

                if (nextElf == null || nextElf == elf)
                {
                    return $"Elf {elf.Value.ID}";
                }
                else
                {
                    Elves.Remove(nextElf);
                    elf = elf.NextOrFirst();
                }
            }
            return "Unknown";
        }

        public string Part2()
        {
            var Elves = new List<int>();
            for (int i = 0; i < Int32.Parse(input); i++)
            {
                Elves.Add(i + 1);
            }
            var elf = Int32.Parse(input);
            while (Elves.Count > 1)
            {
                var acrossElves = (int)Math.Floor((double)Elves.Count / 2);
                var elfIndex = Elves.IndexOf(elf);
                elfIndex++;
                elfIndex = elfIndex % Elves.Count;
                elf = Elves[elfIndex];
                var oppositeElf = (acrossElves + elfIndex) % Elves.Count;
                Elves.RemoveAt(oppositeElf);
            }
            System.Diagnostics.Debug.WriteLine(Elves.FirstOrDefault());
            return $"Elf {Elves.FirstOrDefault()}";
        }
    }
}
