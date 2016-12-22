using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day21
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day21.txt");

        public string Part1()
        {
            var result = "abcdefgh";
            var operations = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var operation in operations)
            {
                result = ProcessOperation(result, operation);
                System.Diagnostics.Debug.WriteLine($"{operation}: {result}");
            }

            return result.ToString();
        }

        static string SwapCharacters(string value, int position1, int position2)
        {
            //
            // Swaps characters in a string.
            //
            char[] array = value.ToCharArray(); // Get characters
            char temp = array[position1]; // Get temporary copy of character
            array[position1] = array[position2]; // Assign element
            array[position2] = temp; // Assign element
            return new string(array); // Return string
        }

        static string RotateString(string value, bool left)
        {
            if (left)
            {
                var length = value.Length - 1;
                return value.Substring(1, length) + value[0];
            }
            else
            {
                var length = value.Length - 1;
                return value[length] + value.Substring(0, length);
            }
        }

        private string ProcessOperation(string result, string operation)
        {
            string newResult = result;

            var words = operation.Split(' ');

            switch (words[0])
            {
                case "swap":
                    if (words[1].Equals("position"))
                    {
                        var swap_position_X = Int32.Parse(words[2]);
                        var swap_position_Y = Int32.Parse(words[5]);
                        newResult = SwapCharacters(result, swap_position_X, swap_position_Y);
                    }
                    else //letter
                    {
                        var swap_letter_X = words[2];
                        var swap_letter_Y = words[5];
                        newResult = result.Replace(swap_letter_X, "-")
                            .Replace(swap_letter_Y, swap_letter_X)
                            .Replace("-", swap_letter_Y);
                    }
                    break;
                case "rotate":
                    if (words[1].Equals("based"))
                    {
                        var rotate_position_X = result.IndexOf(words[6]);
                        var rotationAmount = 1 + rotate_position_X + ( rotate_position_X >= 4 ? 1 : 0 );
                        //System.Diagnostics.Debug.WriteLine($"rotate_position_X: {rotate_position_X}, rotationAmount: {rotationAmount}");
                        for (int i = 0; i < rotationAmount; i++)
                        {
                            newResult = RotateString(newResult, false);
                        }
                    }
                    else //left/right
                    {
                        bool rotateLeft;
                        if (words[1].Equals("left"))
                        {
                            rotateLeft = true;
                        }
                        else
                        {
                            rotateLeft = false;
                        }
                        var rotationAmount = Int32.Parse(words[2]);
                        for (int i = 0; i < rotationAmount; i++)
                        {
                            newResult = RotateString(newResult, rotateLeft);
                        }
                    }
                    break;
                case "reverse":
                    var reverse_X = Int32.Parse(words[2]);
                    var reverse_Y = Int32.Parse(words[4]) + 1;
                    if (reverse_X == 0 && reverse_Y == result.Length)
                    {
                        newResult = new string(result.Reverse().ToArray());
                    }
                    else
                    {
                        newResult = result.Substring(0, reverse_X);
                        newResult += new string(result.Substring(reverse_X, reverse_Y - reverse_X).Reverse().ToArray());
                        newResult += result.Substring(reverse_Y, result.Length - reverse_Y);
                    }

                    break;
                case "move":
                    var move_X = Int32.Parse(words[2]);
                    var move_Y = Int32.Parse(words[5]);
                    var move_c = result[move_X].ToString();
                    newResult = result.Remove(move_X, 1).Insert(move_Y, move_c);
                    break;
            }
            return newResult;
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }

        private static List<string> GetPer(char[] list, int k, int m)
        {
            var results = new List<string>();
            if (k == m)
            {
                results.Add(new string(list));
            }
            else
            {
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    results.AddRange(GetPer(list, k + 1, m));
                    Swap(ref list[k], ref list[i]);
                }
            }
            return results;
        }

        public string Part2()
        {
            //we should really not brute force this...
            var operations = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var scrambledPassword = "fbgdceah";
            int size = scrambledPassword.Length;
            char[] arr = scrambledPassword.ToCharArray();
            var perms = GetPer(arr, 0, arr.Length - 1);

            foreach (var item in perms)
            {
                var result = item;
                foreach (var operation in operations)
                {
                    result = ProcessOperation(result, operation);
                }
                if (result.Equals(scrambledPassword))
                {
                    return item;
                }
            }

            return "Unknown";
        }
    }
}
