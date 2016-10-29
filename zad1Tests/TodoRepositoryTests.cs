using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1Tests
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        public void GetExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsNotNull(repository.Get(todoItem.Id));
        }

        [TestMethod]
        public void GetNonExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsNull(repository.Get(Guid.NewGuid()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void RemoveExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsTrue(repository.Remove(todoItem.Id));
            Assert.AreEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void RemoveNonExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsFalse(repository.Remove(Guid.NewGuid()));
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void UpdateNonExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Update(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void UpdateExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            var testItem = new TodoItem("Eggs");
            testItem.IsCompleted = true;
            testItem.Id = todoItem.Id;
            repository.Update(testItem);
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdatingWithNullArgThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Update(null);
        }

        [TestMethod]
        public void MarkAsCompletedNonExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsFalse(repository.MarkAsCompleted(Guid.NewGuid()));
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void MarkAsCompletedExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id));
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void GetAllTest()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Eggs");
            repository.Add(todoItem);
            todoItem = new TodoItem("Soda");
            repository.Add(todoItem);
            todoItem = new TodoItem("Apples");
            repository.Add(todoItem);
            Assert.AreEqual(3, repository.GetAll().Count);
        }

        [TestMethod]
        public void GetActiveTest()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Eggs");
            repository.Add(todoItem);
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id));
            todoItem = new TodoItem("Soda");
            repository.Add(todoItem);
            todoItem = new TodoItem("Apples");
            repository.Add(todoItem);
            Assert.AreEqual(2, repository.GetActive().Count);
        }

        [TestMethod]
        public void GetCompletedTest()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Eggs");
            repository.Add(todoItem);
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id));
            todoItem = new TodoItem("Soda");
            repository.Add(todoItem);
            todoItem = new TodoItem("Apples");
            repository.Add(todoItem);
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id));
            Assert.AreEqual(2, repository.GetCompleted().Count);
        }

        [TestMethod]
        public void GetFilteredTest()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Eggs");
            repository.Add(todoItem);
            todoItem = new TodoItem("Soda");
            repository.Add(todoItem);
            todoItem = new TodoItem("Apples");
            repository.Add(todoItem);

            Func<TodoItem, bool> testFunc = r =>
            {
                if (r.Text.Length == 4)
                    return true;
                else
                    return false;
            };
            Assert.AreEqual(2, repository.GetFiltered(testFunc).Count);
        }
    }
}