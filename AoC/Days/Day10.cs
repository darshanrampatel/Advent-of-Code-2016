using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day10
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day10.txt");
        class Instruction
        {
            public string InstructionText { get; set; }
            public bool Processed { get; set; } = false;
        }

        class Bot
        {
            public int? Chip1 { get; set; }
            public int? Chip2 { get; set; }
        }

        class OutputBin
        {
            public int ID { get; set; }
            public int? Value { get; set; }
        }

        List<OutputBin> Outputs = new List<OutputBin>();

        SortedList<int, Bot> Bots = new SortedList<int, Bot>();

        string Part1_Answer = string.Empty;
        public string Part1()
        {
            Outputs = new List<OutputBin>();
            Bots = new SortedList<int, Bot>();
            var instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var instructionsList = new List<Instruction>();
            foreach (string instruction in instructions)
            {
                instructionsList.Add(new Instruction()
                {
                    InstructionText = instruction
                });
            }

            while (instructionsList.Any(i => !i.Processed))
            {
                foreach (var instruction in instructionsList)
                {
                    if (!instruction.Processed)
                    {
                        ProcessInstruction(instruction);
                    }
                }
            }

            return Part1_Answer;
        }

        void AddChipToBot(int botID, int chipID)
        {
            Bot bot;
            if (Bots.TryGetValue(botID, out bot))
            {
                if (bot.Chip1 != null)
                {
                    if (bot.Chip2 == null)
                    {
                        bot.Chip2 = chipID;
                    }
                }
                else
                {
                    bot.Chip1 = chipID;
                }
            }
            else
            {
                Bots.Add(botID, new Bot()
                {
                    Chip1 = chipID
                });
            }
        }

        void ProcessInstruction(Instruction instruction)
        {
            var parts = instruction.InstructionText.Split(' ');
            if (parts[0].Equals("value"))
            {
                AddChipToBot(Int32.Parse(parts[5]), Int32.Parse(parts[1]));
                instruction.Processed = true;
            }
            else if (parts[0].Equals("bot"))
            {
                var botID = Int32.Parse(parts[1]);
                Bot bot;
                if (Bots.TryGetValue(botID, out bot))
                {
                    if (bot.Chip1 != null && bot.Chip2 != null)
                    {
                        if ((bot.Chip1 == 17 && bot.Chip2 == 61) || (bot.Chip1 == 61 && bot.Chip2 == 17))
                        {
                            Part1_Answer = botID.ToString();
                        }
                        if (parts[5].Equals("bot"))
                        {
                            var firstBotID = Int32.Parse(parts[6]);
                            Bot firstBot;
                            if (Bots.TryGetValue(firstBotID, out firstBot))
                            {
                                if (firstBot.Chip1 == null)
                                {
                                    firstBot.Chip1 = bot.Chip1 < bot.Chip2 ? (int)bot.Chip1 : (int)bot.Chip2;
                                }
                                else if (firstBot.Chip2 == null)
                                {
                                    firstBot.Chip2 = bot.Chip1 < bot.Chip2 ? (int)bot.Chip1 : (int)bot.Chip2;
                                }
                            }
                            else
                            {
                                Bots.Add(firstBotID, new Bot()
                                {
                                    Chip1 = bot.Chip1 < bot.Chip2 ? (int)bot.Chip1 : (int)bot.Chip2
                                });
                            }
                        }
                        else
                        {
                            var index = Outputs.FindIndex(o => o.ID == (Int32.Parse(parts[6])));
                            if (index >= 0)
                            {
                                Outputs[index].Value = bot.Chip1 < bot.Chip2 ? (int)bot.Chip1 : (int)bot.Chip2;
                            }
                            else
                            {
                                Outputs.Add(new OutputBin()
                                {
                                    ID = Int32.Parse(parts[6]),
                                    Value = bot.Chip1 < bot.Chip2 ? (int)bot.Chip1 : (int)bot.Chip2
                                });
                            }
                        }

                        if (parts[10].Equals("bot"))
                        {
                            var firstBotID = Int32.Parse(parts[11]);
                            Bot firstBot;
                            if (Bots.TryGetValue(firstBotID, out firstBot))
                            {
                                if (firstBot.Chip1 == null)
                                {
                                    firstBot.Chip1 = bot.Chip1 < bot.Chip2 ? (int)bot.Chip2 : (int)bot.Chip1;
                                }
                                else if (firstBot.Chip2 == null)
                                {
                                    firstBot.Chip2 = bot.Chip1 < bot.Chip2 ? (int)bot.Chip2 : (int)bot.Chip1;
                                }
                            }
                            else
                            {
                                Bots.Add(firstBotID, new Bot()
                                {
                                    Chip1 = bot.Chip1 < bot.Chip2 ? (int)bot.Chip2 : (int)bot.Chip1
                                });
                            }
                        }
                        else
                        {
                            var index = Outputs.FindIndex(o => o.ID == (Int32.Parse(parts[11])));
                            if (index >= 0)
                            {
                                Outputs[index].Value = bot.Chip1 < bot.Chip2 ? (int)bot.Chip2 : (int)bot.Chip1;
                            }
                            else
                            {
                                Outputs.Add(new OutputBin()
                                {
                                    ID = Int32.Parse(parts[11]),
                                    Value = bot.Chip1 < bot.Chip2 ? (int)bot.Chip2 : (int)bot.Chip1
                                });
                            }
                        }

                        bot.Chip1 = null;
                        bot.Chip2 = null;
                        instruction.Processed = true;
                    }
                }
            }
        }

        public string Part2()
        {
            Outputs = new List<OutputBin>();
            Bots = new SortedList<int, Bot>();
            var instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var instructionsList = new List<Instruction>();
            foreach (string instruction in instructions)
            {
                instructionsList.Add(new Instruction()
                {
                    InstructionText = instruction
                });
            }

            while (instructionsList.Any(i => !i.Processed))
            {
                foreach (var instruction in instructionsList)
                {
                    if (!instruction.Processed)
                    {
                        ProcessInstruction(instruction);
                    }
                }
            }
            return (Outputs[Outputs.FindIndex(o => o.ID == 0)].Value * Outputs[Outputs.FindIndex(o => o.ID == 1)].Value * Outputs[Outputs.FindIndex(o => o.ID == 2)].Value).ToString();
        }
    }
}
