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
            TodoItem res = _inMemoryTodoDatabase.Where(r => r.Id == todoId).FirstOrDefault();
            return res;
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
                IGenericList<TodoItem> inMemoryTodoDatabase = new IGenericList<TodoItem>;
                inMemoryTodoDatabase = _inMemoryTodoDatabase.Where(r => r.Id != todoId).ToList();
                return true;
            }
        }
    }
}
