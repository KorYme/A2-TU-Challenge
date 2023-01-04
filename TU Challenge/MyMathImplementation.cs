using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge
{
    public class MyMathImplementation
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static List<int> GetAllPrimary(int a)
        {
            List<int> list = new List<int>();
            for (int i = 2; i <= a; i++)
            {
                if (IsPrimary(i))
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public static bool IsDivisible(int a, int b)
        {
            return a % b == 0;
        }
        public static bool IsPrimary(int a)
        {
            if (a<2)
            {
                return false;
            }
            for (int i = 2; i <= a/2; i++)
            {
                if (IsDivisible(a,i))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsEven(int a)
        {
            return (a % 2 == 0);
        }

        public static bool IsMajeur(int age)
        {
            if (age < 0 || age >= 150)
            {
                throw new ArgumentException();
            }
            return age >= 18;
        }

        public static int Power2(int a)
        {
            return a * a;
        }

        public static int Power(int a, int b)
        {
            int result = 1;
            for (int i = 0; i < b; i++)
            {
                result *= a;
            }
            return result;
        }

        public static int IsInOrder(int a, int b)
        {
            return a < b ? 1 : a > b ? -1 : 0;
        }

        public static int IsInOrderDesc(int a, int b)
        {
            return -IsInOrder(a, b);
        }

        public static bool IsListInOrder(List<int> list)
        {
            for (int i = 0; i < list.Count-1; i++)
            {
                if (IsInOrder(list[i],list[i+1]) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsListInOrder(List<int> list, Func<int, int, int> func)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (func(list[i], list[i+1]) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<int> Sort(List<int> toSort)
        {
            while (!IsListInOrder(toSort))
            {
                for (int i = 0; i < toSort.Count-1; i++)
                {
                    if (IsInOrder(toSort[i], toSort[i+1]) == -1)
                    {
                        int tmp = toSort[i];
                        toSort[i] = toSort[i+1];
                        toSort[i+1] = tmp;
                    }
                }
            } 
            return toSort;
        }

        public static List<int> GenericSort(List<int> toSort, Func<int, int, int> func)
        {
            while (!IsListInOrder(toSort, func))
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    if (func(toSort[i], toSort[i + 1]) == -1)
                    {
                        int tmp = toSort[i];
                        toSort[i] = toSort[i + 1];
                        toSort[i + 1] = tmp;
                    }
                }
            }
            return toSort;
        }
    }
}