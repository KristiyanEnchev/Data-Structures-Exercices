namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> newHead = new Node<T>(item);
            if (_head == null)
            {
                _head = newHead;
                _tail = newHead;
            }
            else
            {
                newHead.Next = _head;
                _head = newHead;
            }
            Count++;
        }

        public void AddLast(T item)
        {
            Node<T> newLast = new Node<T>(item);
            newLast.Next = null;

            if (_tail == null)
            {
                _head = newLast;
                _tail = newLast;
            }
            else
            {
                _tail.Next = newLast;
                _tail = newLast;
            }
            Count++;
        }

        public T GetFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._head.Element;
        }

        public T GetLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._tail.Element;
        }

        public T RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var oldHead = this._head;
            _head = _head.Next;
            Count--;
            return oldHead.Element;
        }

        public T RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var current = this._head;
            var result = this._tail.Element;
            while (current.Next != null)
            {
                if (current.Next.Next == null)
                {
                    current.Next = null;
                    break;
                }
                current = current.Next;
            }

            this._tail = current;
            Count--;
            return result;
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