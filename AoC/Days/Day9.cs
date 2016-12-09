using System;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day9
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day9.txt");

        public string Part1()
        {
            var answer = string.Empty;
            var inputStrings = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var compressedString in inputStrings)
            {
                var decompressedString = DecompressString(compressedString);
                answer += decompressedString.Length;
                //System.Diagnostics.Debug.Write(compressedString + " => " + decompressedString + " (" + decompressedString.Length + ")" + Environment.NewLine);
            }
            return answer;
        }

        string DecompressString(string compressedString)
        {
            var decompressedString = string.Empty;
            bool withinMarker = false;
            bool foundX = false;
            var countOfCharactersToCopyString = string.Empty;
            int countOfCharactersToCopy = 0;
            var timesToRepeat = string.Empty;
            var stringToCopy = string.Empty;
            foreach (var c in compressedString)
            {
                if (!withinMarker && c.Equals('(') && countOfCharactersToCopy == 0)
                {
                    if (!string.IsNullOrEmpty(timesToRepeat))
                    {
                        for (int i = 0; i < Int32.Parse(timesToRepeat); i++)
                        {
                            decompressedString += stringToCopy;
                        }
                        timesToRepeat = string.Empty;
                    }

                    foundX = false;
                    countOfCharactersToCopyString = string.Empty;
                    countOfCharactersToCopy = 0;
                    stringToCopy = string.Empty;
                    withinMarker = true;
                }
                else
                {
                    if (withinMarker)
                    {
                        if (c.Equals(')'))
                        {
                            withinMarker = false;
                        }
                        else if (c.Equals('x'))
                        {
                            foundX = true;
                        }
                        else
                        {
                            if (foundX)
                            {
                                timesToRepeat += c;
                            }
                            else
                            {
                                countOfCharactersToCopyString += c;
                                countOfCharactersToCopy = Int32.Parse(countOfCharactersToCopyString);
                            }
                        }
                    }
                    else
                    {
                        if (countOfCharactersToCopy > 0)
                        {
                            stringToCopy += c;
                            countOfCharactersToCopy--;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(timesToRepeat))
                            {
                                for (int i = 0; i < Int32.Parse(timesToRepeat); i++)
                                {
                                    decompressedString += stringToCopy;
                                }
                                timesToRepeat = string.Empty;
                            }
                            decompressedString += c;
                        }
                    }
                }


            }
            if (!string.IsNullOrEmpty(timesToRepeat))
            {
                for (int i = 0; i < Int32.Parse(timesToRepeat); i++)
                {
                    decompressedString += stringToCopy;
                }
            }
            return decompressedString;
        }

        long DecompressStringCountOnly(string compressedString, int n)
        {
            long count = 0;
            for (int i = 0; i < compressedString.Length; i++)
            {
                if (compressedString[i].Equals('('))
                {
                    var marker = new String(compressedString.Skip(i + 1).TakeWhile(c => !c.Equals(')')).ToArray());
                    var markerSplit = marker.Split('x');
                    var countOfCharactersToCopy = Int32.Parse(markerSplit.FirstOrDefault());
                    var timesToRepeat = Int32.Parse(markerSplit.LastOrDefault());
                    var charactersToSkip = i + marker.Length + 2; // +brackets
                    count += DecompressStringCountOnly(new string(compressedString
                        .Skip(charactersToSkip)
                        .Take(countOfCharactersToCopy)
                        .ToArray()), timesToRepeat);
                    i = charactersToSkip + countOfCharactersToCopy - 1;
                }
                else
                {
                    count++;
                }
            }
            return count * n;
        }

        public string Part2()
        {
            var answer = string.Empty;
            var inputStrings = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var compressedString in inputStrings)
            {

                answer += DecompressStringCountOnly(compressedString, 1).ToString();
                //System.Diagnostics.Debug.Write(answer + Environment.NewLine);
            }
            return answer;
        }
    }
}
