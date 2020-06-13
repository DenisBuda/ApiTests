using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class RandomValuesInt
    {
        public static string RandomString(int length)
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static string RandomInt(int length)
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            const string chars = "0123456789";
            //const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
