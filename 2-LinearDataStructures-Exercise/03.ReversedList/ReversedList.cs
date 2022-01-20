namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;
        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity = DefaultCapacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(Count - index - 1);
                return this._items[Count - index - 1];
            }
            set
            {
                ValidateIndex(index);
                this._items[index] = value;
            }
        }


        public void Add(T item)
        {
            if (this.Count == this.Capacity)
            {
                ResizeList();
            }

            this._items[this.Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            if (this.IndexOf(item) >= 0)
            {
                return true;
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this._items.Length; i++)
            {
                if (_items[i].Equals(item))
                {
                    return this.Count - i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (this.Count == this.Capacity)
            {
                ResizeList();
            }

            ValidateIndex(index);

            for (int i = Count + 1; i > Count - index; i--)
            {
                this._items[i] = this._items[i - 1];
            }
            this._items[this.Count - index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            int index = 0;
            for (int i = 0; i < this._items.Length; i++)
            {
                if (this._items[i].Equals(item))
                {
                    index = i;
                }
            }
            if (index == 0)
            {
                return false;
            }

            for (int i = index; i < this._items.Length - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            Count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            for (int i = Count - index; i < Count - 1; i++)
            {
                this._items[i] = this._items[ i + 1];
            }
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void ResizeList()
        {
            var newArrCapacity = this._items.Length * 2;
            var newArray = new T[newArrCapacity];
            for (int i = 0; i < this.Count; i++)
            {
                newArray[i] = _items[i];
            }
            
            this._items = newArray;
            this.Capacity = newArrCapacity;

        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
}