using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad1Tests;

namespace zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            TodoRepositoryTests test = new TodoRepositoryTests();
            //test.AddingItemWillAddToDatabase();
            //test.GetItem();
            //test.GetNonExistingItemAssertFailed();
            //test.RemoveExistingItem();
            //test.RemoveNonExistingItem();
            //test.UpdateNonExistingItem();
            //test.UpdateExistingItem();
            //test.UpdatingWithNullArgThrowsException();
            //test.MarkAsCompletedNonExistingItem();
            //test.MarkAsCompletedExistingItem();
            //test.GetAllTest();
            //test.GetActiveTest();
            //test.GetCompletedTest();
            test.GetFilteredTest();
            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
