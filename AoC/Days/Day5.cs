using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    class Day5
    {
        static string input = "uqwqemis";

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
        public string Part1()
        {
            var password = "";
            var answer = CalculateMD5Hash(input);
            var count = 0;
            while (password.Length < 8)
            {
                while (!answer.StartsWith("00000"))
                {
                    answer = CalculateMD5Hash(input + count.ToString());
                    count++;
                }
                password += answer[5];
                answer = CalculateMD5Hash(input + (count++).ToString());
            }
            return password;
        }

        public string Part2()
        {
            var password = new char[8];
            password[0] = '-';
            password[1] = '-';
            password[2] = '-';
            password[3] = '-';
            password[4] = '-';
            password[5] = '-';
            password[6] = '-';
            password[7] = '-';
            var count = 0;
            var answer = CalculateMD5Hash(input);
            while (password.Contains('-'))
            {
                while (!answer.StartsWith("00000"))
                {
                    answer = CalculateMD5Hash(input + count.ToString());
                    count++;
                }
                if (char.IsNumber(answer[5]) && char.GetNumericValue(answer[5]) < 8 && char.GetNumericValue(answer[5]) >= 0)
                {
                    if (password[Int32.Parse(answer[5].ToString())].Equals('-'))
                    {
                        password[Int32.Parse(answer[5].ToString())] = answer[6];
                    }
                }
                answer = CalculateMD5Hash(input + (count++).ToString());
            }
            return string.Join("", password);
        }
    }
}
