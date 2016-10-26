using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenList
{
    public interface IGenericList<X> : IEnumerable<X>
    {
        /// <summary >
        /// Adds an item to the collection .
        /// </ summary >
        void Add(X item);

        /// <summary >
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing .
        /// </ summary >
        bool Remove(X item);

        /// <summary >
        /// Removes the item at the given index in the collection .
        /// </ summary >
        bool RemoveAt(int index);

        /// <summary >
        /// Returns the item at the given index in the collection .
        /// </ summary >
        X GetElement(int index);

        /// <summary >
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </ summary >
        int IndexOf(X item);

        /// <summary >
        /// Readonly property . Gets the number of items contained in the collection.
        /// </ summary >
        int Count { get; }

        /// <summary >
        /// Removes all items from the collection .
        /// </ summary >
        void Clear();

        /// <summary >
        /// Determines whether the collection contains a specific value .
        /// </ summary >
        bool Contains(X item);
    }


    public class GenericList<X> : IGenericList<X>
    {
        private X[] _internalStorage;
        private int size { get; set; }
        private int totalSize { get; set; }


        public GenericList()
        {
            _internalStorage = new X[4];
            size = 0;
        }

        public GenericList(int initialSize)
        {
            if (initialSize > 0)
            {
                _internalStorage = new X[initialSize];
                size = 0;
            }
            else
            {
                Console.WriteLine("initialSize is equal or less than 0.");
                // return nešto 
            }
        }


        public void Add(X item)
        {
            if (size >= _internalStorage.Length)
            {
                totalSize = _internalStorage.Length;
                X[] internalStorage = new X[totalSize];

                for (int i = 0; i < size; i++)
                {
                    internalStorage[i] = _internalStorage[i];
                }

                totalSize *= 2;
                _internalStorage = new X[totalSize];

                for (int i = 0; i < size; i++)
                {
                    _internalStorage[i] = internalStorage[i];
                }
            }

            _internalStorage[size] = item;
            size++;
        }


        public bool RemoveAt(int index)
        {
            if (index >= size || index < 0)
            {
                return false;
            }

            for (int i = index; i < size - 1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }

            size--;
            return true;
        }


        public bool Remove(X item)
        {
            int idx = -1;
            for (int i = 0; i < size; i++)
            {
                if (AreEqual<X>(_internalStorage[i], item))
                {
                    idx = i;
                    break;
                }
            }

            return RemoveAt(idx);
        }


        public X GetElement(int index)
        {
            if (index >= 0 && index < size)
            {
                return _internalStorage[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        public int Count
        {
            get
            {
                return size;
            }
        }


        public int IndexOf(X item)
        {
            for (int i = 0; i < size; i++)
            {
                if (AreEqual<X>(_internalStorage[i], item))
                {
                    return i;
                }
            }

            return -1;
        }


        public void Clear()
        {
            size = 0;
        }


        public bool Contains(X item)
        {
            for (int i = 0; i < size; i++)
            {
                if (AreEqual<X>(_internalStorage[i], item))
                {
                    return true;
                }
            }

            return false;
        }


        private bool AreEqual<T>(T param1, T param2)
        {
            return EqualityComparer<T>.Default.Equals(param1, param2);
        }


        // IEnumerable <X> implementation
        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void test()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write(_internalStorage[i] + " ");
            }
        }
    }


    public class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> _genList;
        private int _index;
        private X _current;
        /*
        // Default constructor
        public GenericListEnumerator()
        {
            //nothing
        }
        */
        public GenericListEnumerator(GenericList<X> genList)
        {
            _genList = genList;
            _index = -1;
            _current = default(X);
        }

        public void Reset()
        {
            _index = -1;
            //_current = default(X);
        }


        public X Current
        {
            get
            {
                //return _genList.ElementAt(_index);
                return _current;
            }
        }


        object IEnumerator.Current
        {
            get
            {
                //return _genList.ElementAt(_index);
                return _current;
            }
        }

        /*
        public void Dispose()
        {
            _genList = null;
            //_current = default(X);
            _index = -1;
        }
        */

        void IDisposable.Dispose() { }

        public bool MoveNext()
        {
            if (++_index >= _genList.Count)
                return false;
            else
                _current = _genList.GetElement(_index);
            return true;
        }
    }
}
