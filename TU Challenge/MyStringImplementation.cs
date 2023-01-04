using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge
{
    public class MyStringImplementation
    {
        public static bool IsNullEmptyOrWhiteSpace(string input)
        {
            if (input == null)
            {
                return true;
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public static string MixString(string a, string b)
        {
            if (IsNullEmptyOrWhiteSpace(a) || IsNullEmptyOrWhiteSpace(b))
            {
                throw new ArgumentException();
            }
            string result = "";
            for (int i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                if (i < a.Length)
                {
                    result += a[i];
                }
                if (i < b.Length)
                {
                    result += b[i];
                }
            }
            return result;
        }

        public static string ToLowerCase(string a)
        {
            string result = "";
            for (int i = 0; i < a.Length; i++)
            {
                if ((a[i] >= 'A' && a[i] <= 'Z') || (a[i] >= 'À' && a[i] <= 'Þ'))
                {
                    result += (char)(a[i] + 32);
                }
                else
                {
                    result += a[i];
                }
            }
            return result;
        }

        public static string Voyelles(string a)
        {
            string result = "";
            List<string> voyelles = new List<string>() { "a", "e", "i", "o", "u", "y" };
            for (int i = 0; i < a.Length; i++)
            {
                if (!result.Contains(a[i]) && voyelles.Contains(ToLowerCase(a[i].ToString())))
                {
                    result += ToLowerCase(a[i].ToString());
                }
            }
            return result;
        }

        public static string Reverse(string a)
        {
            string result = "";
            for (int i = a.Length-1; i >= 0; i--)
            {
                result += a[i];
            }
            return result;
        }

        public static string BazardString(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i+=2)
            {
                result += input[i];
            }
            for (int i = 1; i < input.Length; i+=2)
            {
                result += input[i];
            }
            return result;
        }

        public static string UnBazardString(string input)
        {
            string startInput = "";
            string endInput = "";
            for (int i = 0; i < input.Length/2; i++)
            {
                startInput+= input[i];
            }
            for (int i = input.Length/2; i < input.Length; i++)
            {
                endInput+= input[i];
            }
            return MixString(startInput, endInput);
        }

        public static string ToCesarCode(string input, int offset)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (IsNullEmptyOrWhiteSpace(ToLowerCase(input[i].ToString())))
                {
                    result += input[i];
                }
                else
                {
                    result += (char)((((input[i] + (offset % 26)) - 97) % 26) + 97);
                }
            }
            return result;
        }
    }
}
