using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using GenList;
using Models;

namespace Repositories
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }

            // Shorter way to write this in C# using ?? operator :
            // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
            // x ?? y -> if x is not null , expression returns x. Else y.
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.Where(r => r.Id == todoId).FirstOrDefault();
        }

        public void Add(TodoItem todoItem)
        {
            if (Get(todoItem.Id) != null)
            {
                throw new DuplicateTodoItemException("duplicate id: {todoItem.Id}");
            }
            else
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
        }

        public bool Remove(Guid todoId)
        {
            if (Get(todoId) == null)
            {
                return false;
            }
            else
            {
                return _inMemoryTodoDatabase.Remove(Get(todoId));
            }
        }

        public void Update(TodoItem todoItem)
        {
            var item = Get(todoItem.Id);

            if (item == null)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                item.Text = todoItem.Text;
                item.IsCompleted = todoItem.IsCompleted;
                item.DateCompleted = todoItem.DateCompleted;
                item.DateCreated = todoItem.DateCreated;
            }
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            var item = Get(todoId);

            if (item == null)
            {
                return false;
            }
            else
            {
                item.MarkAsCompleted();
                return true;
            }
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(r => r.DateCreated).ToList();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(r => r.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(r => r.IsCompleted == true).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            //List<TodoItem> res = new List<TodoItem>;
            //foreach (TodoItem r in _inMemoryTodoDatabase)
            //{
            //    if (filterFunction(r))
            //        res.Add(r);
            //}
            //return res;
            return _inMemoryTodoDatabase.Where(r => filterFunction(r) == true).ToList();
        }
    }
}
