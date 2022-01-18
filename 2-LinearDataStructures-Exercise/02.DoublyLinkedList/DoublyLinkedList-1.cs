//namespace Problem02.DoublyLinkedList
//{
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;

//    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
//    {
//        private Node<T> head;
//        private Node<T> tail;

//        public int Count { get; private set; }

//        public void AddFirst(T item)
//        {
//            Node<T> newHead = new Node<T>(item)
//            {
//                Item = item,
//                Next = this.head
//            };
//            if (this.head == null)
//            {
//                this.head = newHead;
//                this.tail = newHead;
//            }
//            else
//            {
//                this.head.Previous = newHead;
//                this.head = newHead;
//            }
//            Count++;
//        }

//        public void AddLast(T item)
//        {
//            Node<T> newLast = new Node<T>(item)
//            {
//                Item = item,
//                Next = null
//            };

//            if (this.head == null)
//            {
//                this.head = newLast;
//                this.tail = newLast;
//            }
//            else
//            {
//                this.tail.Next = newLast;
//                this.tail = newLast;
//            }

//            Count++;
//        }

//        public T GetFirst()
//        {
//            if (Count == 0)
//            {
//                throw new InvalidOperationException();
//            }

//            return this.head.Item;
//        }

//        public T GetLast()
//        {
//            if (Count == 0)
//            {
//                throw new InvalidOperationException();
//            }

//            return this.tail.Item;
//        }

//        public T RemoveFirst()
//        {
//            if (Count == 0)
//            {
//                throw new InvalidOperationException();
//            }

//            var newHead = this.head.Next;
//            var result = this.head.Item;
//            this.head = newHead;
//            Count--;
//            return result;
//        }

//        public T RemoveLast()
//        {
//            if (Count == 0)
//            {
//                throw new InvalidOperationException();
//            }

//            var currentHead = this.head;
//            var result = this.tail.Item;
//            while (currentHead.Next != null)
//            {
//                if (currentHead.Next.Next == null)
//                {
//                    currentHead.Next = null;
//                    break;
//                }
//                currentHead = currentHead.Next;
//            }

//            Count--;
//            this.tail = currentHead;
//            return result;
//        }

//        public IEnumerator<T> GetEnumerator()
//        {
//            var currentHead = this.head;
//            while (currentHead != null)
//            {
//                yield return currentHead.Item;
//                currentHead = currentHead.Next;
//            }
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }
//    }
//}