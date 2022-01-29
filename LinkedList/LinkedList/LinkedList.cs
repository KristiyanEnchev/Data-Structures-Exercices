using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Last { get; set; }

        public void Add(T element)
        {
            Node<T> newHead = new Node<T>(element);
            newHead.Next = Head;

            if (Head == null)
            {
                Last = newHead;
            }

            Head = newHead;
        }

        public void AddLast(T element)
        {
            Node<T> newLast = new Node<T>(element);

            if (Last == null)
            {
                Last = newLast;
                Head = newLast;
            }
            else
            {
                Last.Next = newLast;
                Last = newLast; 
            }
        }

        public Node<T> RemoveHead()
        {
            var oldHead = Head;
            Head = Head.Next;

            if (Head == null)
            {
                Last = null;
            }

            return oldHead;
        }
    }
}
