using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    class Day2
    {
        static string input = @"LURLLLLLDUULRDDDRLRDDDUDDUULLRLULRURLRRDULUUURDUURLRDRRURUURUDDRDLRRLDDDDLLDURLDUUUDRDDDLULLDDLRLRRRLDLDDDDDLUUUDLUULRDUDLDRRRUDUDDRULURULDRUDLDUUUDLUDURUURRUUDRLDURRULURRURUUDDLRLDDDDRDRLDDLURLRDDLUDRLLRURRURRRURURRLLRLDRDLULLUDLUDRURDLRDUUDDUUDRLUDDLRLUDLLURDRUDDLRURDULLLUDDURULDRLUDLUDLULRRUUDDLDRLLUULDDURLURRRRUUDRUDLLDRUDLRRDUDUUURRULLDLDDRLUURLDUDDRLDRLDULDDURDLUUDRRLDRLLLRRRDLLLLURDLLLUDRUULUULLRLRDLULRLURLURRRDRLLDLDRLLRLULRDDDLUDDLLLRRLLLUURLDRULLDURDLULUDLRLDLUDURLLLURUUUDRRRULRDURLLURRLDLRLDLDRRUUDRDDDDDRDUUDULUL
RRURLURRULLUDUULUUURURULLDLRLRRULRUDUDDLLLRRRRLRUDUUUUDULUDRULDDUDLURLRRLLDLURLRDLDUULRDLLLDLLULLURLLURURULUDLDUDLUULDDLDRLRRUURRRLLRRLRULRRLDLDLRDULDLLDRRULRDRDUDUUUDUUDDRUUUDDLRDULLULDULUUUDDUULRLDLRLUUUUURDLULDLUUUULLLLRRRLDLLDLUDDULRULLRDURDRDRRRDDDLRDDULDLURLDLUDRRLDDDLULLRULDRULRURDURRUDUUULDRLRRUDDLULDLUULULRDRDULLLDULULDUDLDRLLLRLRURUDLUDDDURDUDDDULDRLUDRDRDRLRDDDDRLDRULLURUDRLLUDRLDDDLRLRDLDDUULRUDRLUULRULRLDLRLLULLUDULRLDRURDD
UUUUUURRDLLRUDUDURLRDDDURRRRULRLRUURLLLUULRUDLLRUUDURURUDRDLDLDRDUDUDRLUUDUUUDDURRRDRUDDUURDLRDRLDRRULULLLUDRDLLUULURULRULDRDRRLURULLDURUURDDRDLLDDDDULDULUULLRULRLDURLDDLULRLRRRLLURRLDLLULLDULRULLDLRULDDLUDDDLDDURUUUURDLLRURDURDUUDRULDUULLUUULLULLURLRDRLLRULLLLRRRRULDRULLUURLDRLRRDLDDRLRDURDRRDDDRRUDRLUULLLULRDDLDRRLRUDLRRLDULULRRDDURULLRULDUDRLRUUUULURLRLRDDDUUDDULLULLDDUDRLRDDRDRLDUURLRUULUULDUDDURDDLLLURUULLRDLRRDRDDDUDDRDLRRDDUURDUULUDDDDUUDDLULLDRDDLULLUDLDDURRULDUDRRUURRDLRLLDDRRLUUUDDUUDUDDDDDDDLULURRUULURLLUURUDUDDULURDDLRDDRRULLLDRRDLURURLRRRDDLDUUDR
URLLRULULULULDUULDLLRDUDDRRLRLLLULUDDUDLLLRURLLLLURRLRRDLULRUDDRLRRLLRDLRRULDLULRRRRUUDDRURLRUUDLRRULDDDLRULDURLDURLRLDDULURDDDDULDRLLUDRULRDDLUUUDUDUDDRRUDUURUURLUUULRLULUURURRLRUUULDDLURULRRRRDULUDLDRLLUURRRLLURDLDLLDUDRDRLLUDLDDLRLDLRUDUULDRRLLULDRRULLULURRLDLUUDLUDDRLURDDUDRDUDDDULLDRUDLRDLRDURUULRRDRUUULRUURDURLDUDRDLLRUULUULRDDUDLRDUUUUULDDDDDRRULRURLLRLLUUDLUDDUULDRULDLDUURUDUDLRULULUULLLLRLULUDDDRRLLDRUUDRLDDDRDDURRDDDULURDLDLUDDUULUUURDULDLLULRRUURDDUDRUULDLRLURUDLRDLLLDRLDUURUDUDRLLLDDDULLUDUUULLUUUDLRRRURRRRRDUULLUURRDUU
UDULUUDLDURRUDDUDRDDRRUULRRULULURRDDRUULDRLDUDDRRRRDLRURLLLRLRRLLLULDURRDLLDUDDULDLURLURUURLLLDUURRUUDLLLUDRUDLDDRLRRDLRLDDDULLRUURUUUDRRDLLLRRULDRURLRDLLUDRLLULRDLDDLLRRUDURULRLRLDRUDDLUUDRLDDRUDULLLURLRDLRUUDRRUUDUDRDDRDRDDLRULULURLRULDRURLURLRDRDUUDUDUULDDRLUUURULRDUDRUDRULUDDULLRDDRRUULRLDDLUUUUDUDLLLDULRRLRDDDLULRDUDRLDLURRUUDULUDRURUDDLUUUDDRLRLRLURDLDDRLRURRLLLRDRLRUUDRRRLUDLDLDDDLDULDRLURDURULURUDDDUDUULRLLDRLDDDDRULRDRLUUURD";

        public string Part1()
        {
            var answer = "";
            var Position = 5;
            var instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string instruction in instructions)
            {
                foreach (char c in instruction)
                {
                    Position = Move(c, Position);
                }
                answer += Position.ToString();
            }
            return "Bathroom code: " + answer;
        }

        public string Part2()
        {
            var answer = "";
            var Position = '5';
            var instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string instruction in instructions)
            {
                foreach (char c in instruction)
                {
                    Position = MovePart2(c, Position);
                }
                answer += Position.ToString();
            }
            return "Bathroom code: " + answer;
        }

        private char MovePart2(char direction, char position)
        {
            switch (position)
            {
                case '1':
                    switch (direction)
                    {
                        case 'D':
                            return '3';
                    }
                    break;
                case '2':
                    switch (direction)
                    {
                        case 'D':
                            return '6';
                        case 'R':
                            return '3';
                    }
                    break;
                case '3':
                    switch (direction)
                    {
                        case 'U':
                            return '1';
                        case 'D':
                            return '7';
                        case 'L':
                            return '2';
                        case 'R':
                            return '4';
                    }
                    break;
                case '4':
                    switch (direction)
                    {
                        case 'D':
                            return '8';
                        case 'L':
                            return '3';
                    }
                    break;
                case '5':
                    switch (direction)
                    {
                        case 'R':
                            return '6';
                    }
                    break;
                case '6':
                    switch (direction)
                    {
                        case 'U':
                            return '2';
                        case 'D':
                            return 'A';
                        case 'L':
                            return '5';
                        case 'R':
                            return '7';
                    }
                    break;
                case '7':
                    switch (direction)
                    {
                        case 'U':
                            return '3';
                        case 'D':
                            return 'B';
                        case 'L':
                            return '6';
                        case 'R':
                            return '8';
                    }
                    break;
                case '8':
                    switch (direction)
                    {
                        case 'U':
                            return '4';
                        case 'D':
                            return 'C';
                        case 'L':
                            return '7';
                        case 'R':
                            return '9';
                    }
                    break;
                case '9':
                    switch (direction)
                    {
                        case 'L':
                            return '8';
                    }
                    break;
                case 'A':
                    switch (direction)
                    {
                        case 'U':
                            return '6';
                        case 'R':
                            return 'B';
                    }
                    break;
                case 'B':
                    switch (direction)
                    {
                        case 'U':
                            return '7';
                        case 'D':
                            return 'D';
                        case 'L':
                            return 'A';
                        case 'R':
                            return 'C';
                    }
                    break;
                case 'C':
                    switch (direction)
                    {
                        case 'U':
                            return '8';
                        case 'L':
                            return 'B';
                    }
                    break;
                case 'D':
                    switch (direction)
                    {
                        case 'U':
                            return 'B';
                    }
                    break;
            }
            return position;
        }

        private int Move(char direction, int position)
        {
            switch (direction)
            {
                case 'U':
                    if (position <= 4)
                    {
                        return position;
                    }
                    else
                    {
                        return position - 3;
                    }
                case 'D':
                    if (position >= 7)
                    {
                        return position;
                    }
                    else
                    {
                        return position + 3;
                    }
                case 'L':
                    if (position == 1 || position == 4 || position == 7)
                    {
                        return position;
                    }
                    else
                    {
                        return position - 1;
                    }
                case 'R':
                    if (position == 3 || position == 6 || position == 9)
                    {
                        return position;
                    }
                    else
                    {
                        return position + 1;
                    }
            }
            return position;
        }
    }
}
