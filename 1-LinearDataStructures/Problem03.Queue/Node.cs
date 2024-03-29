﻿namespace Problem03.Queue
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Next { get; set; }

        public Node(T item)
        {
            this.Element = item;
            this.Next = null;
        }
    }
}