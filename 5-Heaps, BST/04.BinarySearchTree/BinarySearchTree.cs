namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {

        }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        private void Copy(Node<T> root)
        {
            if (root == null)
            {
                return;
            }

            this.Insert(root.Value);
            this.Copy(root.LeftChild);
            this.Copy(root.RightChild);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            Node<T> currentElement = this.Root;
            while (currentElement != null)
            {
                if (element.CompareTo(currentElement.Value) < 0)
                {
                    currentElement = currentElement.LeftChild;
                }
                else if (element.CompareTo(currentElement.Value) > 0)
                {
                    currentElement = currentElement.RightChild;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(T element)
        {
            Node<T> newElement = new Node<T>(element, null, null);
            if (this.Root == null)
            {
                this.Root = newElement;
            }
            else
            {
                Node<T> currentElement = this.Root;
                Node<T> parentElement = null;

                while (currentElement != null)
                {
                    parentElement = currentElement;
                    if (element.CompareTo(currentElement.Value) < 0)
                    {
                        currentElement = currentElement.LeftChild;
                    }
                    else if (element.CompareTo(currentElement.Value) > 0)
                    {
                        currentElement = currentElement.RightChild;
                    }
                    else
                    {
                        return;
                    }
                }

                if (element.CompareTo(parentElement.Value) < 0)
                {
                    parentElement.LeftChild = newElement;
                    if (this.LeftChild == null)
                    {
                        this.LeftChild = newElement;
                    }
                }
                else
                {
                    parentElement.RightChild = newElement;
                    if (this.RightChild == null)
                    {
                        this.RightChild = newElement;
                    }
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            Node<T> currentElement = this.Root;

            while (currentElement != null && element.CompareTo(currentElement.Value) != 0)
            {
                if (element.CompareTo(currentElement.Value) < 0)
                {
                    currentElement = currentElement.LeftChild;
                }
                else if (element.CompareTo(currentElement.Value) > 0)
                {
                    currentElement = currentElement.RightChild;
                }
            }

            return new BinarySearchTree<T>(currentElement);
        }
    }
}
