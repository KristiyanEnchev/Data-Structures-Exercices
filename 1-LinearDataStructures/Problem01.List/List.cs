namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY)
        {
        }

        public List(int capacity = DEFAULT_CAPACITY)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return this._items[index];
            }
            set
            {
                ValidateIndex(index);
                _items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (_items.Length == Count)
            {
                T[] newArr = new T[Count * 2];
                for (int i = 0; i < _items.Length; i++)
                {
                    newArr[i] = _items[i];
                }

                _items = newArr;
            }

            _items[Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }


        public int IndexOf(T item)
        {
            int index = -1;
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    index = i;
                }
            }

            return index;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);

            if (_items.Length == Count)
            {
                T[] newArr = new T[Count * 2];
                for (int i = 0; i < _items.Length; i++)
                {
                    newArr[i] = _items[i];
                }

                _items = newArr;
            }

            for (int i = Count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }

            _items[index] = item;
            Count++;

        }

        public bool Remove(T item)
        {
            int index = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    index = i;
                }
            }

            if (index == 0)
            {
                return false;
            }

            for (int i = index; i < Count; i++)
            {
                _items[i] = _items[i + 1];
            }
            Count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            for (int i = index; i < Count; i++)
            {
                _items[i] = _items[i + 1];
            }
            Count--;
            _items[Count] = default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();


        public void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }

}