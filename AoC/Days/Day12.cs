using System;
using System.Collections.Generic;
using System.IO;

namespace AoC.Days
{
    class Day12
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day12.txt");
        List<RegisterValue> registers = new List<RegisterValue>();

        class RegisterValue
        {
            public string Register { get; set; }
            public int Value { get; set; }

            public RegisterValue(string register, int value)
            {
                Register = register;
                Value = value;
            }
        }
        public string Part1()
        {
            var instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            registers.Add(new RegisterValue("a", 0));
            registers.Add(new RegisterValue("b", 0));
            registers.Add(new RegisterValue("c", 0));
            registers.Add(new RegisterValue("d", 0));

            for (int i = 0; i < instructions.Length;)
            {
                i = i + ExecuteInstruction(instructions[i]);
            }

            var answer = string.Empty;
            foreach (var r in registers)
            {
                answer += r.Register + ": " + r.Value.ToString() + Environment.NewLine;
            }
            return answer;
        }

        int ExecuteInstruction(string instruction)
        {
            var parts = instruction.Split(' ');
            if (instruction.StartsWith("cpy"))
            {
                string registerToChange = parts[2];
                string newValue = parts[1];
                int newValueInt;
                if (Int32.TryParse(newValue, out newValueInt))
                {
                    registers.Find(r => r.Register == registerToChange).Value = newValueInt;
                }
                else
                {
                    registers.Find(r => r.Register == registerToChange).Value = registers.Find(r => r.Register == newValue).Value;
                }
                return 1;
            }
            else if (instruction.StartsWith("inc"))
            {
                string registerToChange = parts[1];
                registers.Find(r => r.Register == registerToChange).Value++;
                return 1;
            }
            else if (instruction.StartsWith("dec"))
            {
                string registerToChange = parts[1];
                registers.Find(r => r.Register == registerToChange).Value--;
                return 1;
            }
            else if (instruction.StartsWith("jnz"))
            {
                string registerToCheck = parts[1];
                int newValueInt;
                if (Int32.TryParse(registerToCheck, out newValueInt))
                {
                    if (newValueInt == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        var skipAmount = Int32.Parse(parts[2]);
                        return skipAmount;
                    }
                }
                else
                {
                    var register = registers.Find(r => r.Register == registerToCheck);
                    if (register.Value == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        var skipAmount = Int32.Parse(parts[2]);
                        return skipAmount;
                    }
                }
            }
            return 1;
        }

        public string Part2()
        {
            var instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            registers.Add(new RegisterValue("a", 0));
            registers.Add(new RegisterValue("b", 0));
            registers.Add(new RegisterValue("c", 1));
            registers.Add(new RegisterValue("d", 0));

            for (int i = 0; i < instructions.Length;)
            {
                i = i + ExecuteInstruction(instructions[i]);
            }

            var answer = string.Empty;
            foreach (var r in registers)
            {
                answer += r.Register + ": " + r.Value.ToString() + Environment.NewLine;
            }
            return answer;
        }
    }
}
