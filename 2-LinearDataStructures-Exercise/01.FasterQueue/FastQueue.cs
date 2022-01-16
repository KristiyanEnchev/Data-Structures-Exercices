namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        private Node<T> _tail;
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;
            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var returnValue = this._head.Item;
            this._head = this._head.Next;
            Count--;
            return returnValue;
        }

        public void Enqueue(T item)
        {
            Node<T> newElement = new Node<T>(item)
            {
                Item = item,
                Next = null
            };

            if (this._head == null)
            {
                this._head = newElement;
                this._tail = newElement;
            }
            else
            {
                this._tail.Next = newElement;
                this._tail = newElement;
            }
            Count++;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}