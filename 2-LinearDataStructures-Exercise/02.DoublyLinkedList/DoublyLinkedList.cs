namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new Node<T>(item, null, null);
                Count++;
                return;
            }

            var newNode = new Node<T>(item, this.head, null);
            this.head.Previous = newNode;
            this.head = newNode;
            Count++;
        }

        public void AddLast(T item)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new Node<T>(item, null, null);
                Count++;
                return;
            }
            var newNode = new Node<T>(item, null, this.tail);
            this.tail.Next = newNode;
            this.tail = newNode;
            Count++;
        }

        public T GetFirst()
        {
            ValidateSize();
            return this.head.Item;
        }

        public T GetLast()
        {
            ValidateSize();
            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            ValidateSize();
            var oldHead = this.head;
            var newHead = this.head.Next;
            this.head.Previous = null;
            this.head = newHead;
            Count--;
            return oldHead.Item;
        }

        public T RemoveLast()
        {
            ValidateSize();
            var oldTail = this.tail;
            var newTail = this.tail.Previous;
            this.tail.Next = null;
            this.tail = newTail;
            Count--;
            return oldTail.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentHead = this.head;
            while (currentHead != null)
            {
                yield return currentHead.Item;
                currentHead = currentHead.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateSize()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}