using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stud;

namespace zad4
{
    class Program
    {
        static void Example1()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567")
            };
            var ivan = new Student("Ivan", jmbag: "001234567");
            // false :(
            bool anyIvanExists = list.Any(s => s == ivan);
            Console.WriteLine(anyIvanExists);
        }

        static void Example2()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567"),
                new Student ("Ivan", jmbag :"001234567")
            };
            // 2 :(
            var distinctStudents = list.Distinct().Count();
            Console.WriteLine(distinctStudents);
        }

        static void Main(string[] args)
        {
            Example1();
            Example2();
            Console.ReadKey();
        }
    }
}
