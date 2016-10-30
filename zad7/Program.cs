using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad7_8
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other . NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            Task<int> res1 = IKnowWhoKnowsThis(10);
            Task<int> res2 = IKnowWhoKnowsThis(5);
            int result1 = await res1;
            int result2 = await res2;
            return result1 + result2;
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            Task<int> fac = FactorialDigitSum(n);
            int result = await fac;
            return result;
        }

        private async static Task<int> FactorialDigitSum(int n)
        {
            var res = await Task.Run(() =>
            {
                long fac = 1;

                for (int i = 2; i <= n; i++)
                {
                    fac *= i;
                }

                int result = fac.ToString().Sum(c => c - '0');
                return result;
            });

            return res;
        }
    }
}
