namespace Problem04.SinglyLinkedList
{
    public class Node<T>
    {
        public Node(T element)
        {
            this.Element = element;
            this.Next = null;
        }

        public T Element { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }
    }
}