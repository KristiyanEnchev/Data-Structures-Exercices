namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._top;
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

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._top.Element;
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            T element = _top.Element;
            Node<T> newTop = _top.Next;
            _top.Next = null;
            _top = newTop;
            Count--;
            return element;
        }

        public void Push(T item)
        {
            Node<T> newNode = new Node<T>(item)
            {
                Element = item,
                Next = this._top
            };

            this._top = newNode;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._top;
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