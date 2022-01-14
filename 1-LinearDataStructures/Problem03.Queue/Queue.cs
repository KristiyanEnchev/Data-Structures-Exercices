namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public Queue()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;
            while (current != null)
            {
                if (current.Element.Equals(item))
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

            T element = this._head.Element;
            Node<T> newFirst = this._head.Next;
            this._head = newFirst;
            Count--;
            return element;

        }

        public void Enqueue(T item)
        {
            var newQueue = new Node<T>(item);
            if (this._head == null)
            {
                this._tail = newQueue;
                this._head = newQueue;
            }
            else
            {
                _tail.Next = newQueue;
                _tail = newQueue;
            }
            Count++;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._head.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}