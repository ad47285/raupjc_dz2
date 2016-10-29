using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };

            string[] strings = integers.GroupBy(x => x)
                .OrderBy(group => group.Key)
                .Select(group => string.Format("Broj {0} ponavlja se {1} puta.", group.Key, group.Count()))
                .ToArray();

            //foreach (var item in strings)
            //{
            //    Console.WriteLine(item);
            //}

            Console.ReadKey();
        }
    }
}
