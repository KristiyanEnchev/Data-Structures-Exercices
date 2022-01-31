namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

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
            Copy(root.LeftChild);
            Copy(root.RightChild);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count { get; private set; }

        public bool Contains(T element)
        {
            var current = this.Root;
            while (current != null)
            {
                if (NewElementIsLessThanCurrentElement(current.Value, element))
                {
                    current = current.LeftChild;
                }
                else if (NewElementIsBiggerThanCurrentElement(current.Value, element))
                {
                    current = current.RightChild;
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
                var current = this.Root;
                Node<T> parent = null;

                while (current != null)
                {
                    parent = current;
                    if (NewElementIsLessThanCurrentElement(current.Value, element))
                    {
                        current = current.LeftChild;
                    }
                    else if (NewElementIsBiggerThanCurrentElement(current.Value, element))
                    {
                        current = current.RightChild;
                    }
                }

                if (NewElementIsLessThanCurrentElement(parent.Value, element))
                {
                    parent.LeftChild = newElement;
                    if (this.LeftChild == null)
                    {
                        this.LeftChild = newElement;
                    }
                }
                else
                {
                    parent.RightChild = newElement;
                    if (this.RightChild == null)
                    {
                        this.RightChild = newElement;
                    }
                }
            }

            Count++;
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            while (current != null && !NewElementIsEqualToCurrentElement(current.Value, element))
            {
                if (NewElementIsLessThanCurrentElement(current.Value, element))
                {
                    current = current.LeftChild;
                }
                else if (NewElementIsBiggerThanCurrentElement(current.Value, element))
                {
                    current = current.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            this.InOrderDfs(this.Root, action);
        }

        private void InOrderDfs(Node<T> currentElement, Action<T> action)
        {
            if (currentElement != null)
            {
                this.InOrderDfs(currentElement.LeftChild, action);
                action.Invoke(currentElement.Value);
                this.InOrderDfs(currentElement.RightChild, action);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            List<T> result = new List<T>();
            Queue<Node<T>> queue = new Queue<Node<T>>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var cuurentElement = queue.Dequeue();
                if (NewElementIsLessThanCurrentElement(cuurentElement.Value, lower) && NewElementIsBiggerThanCurrentElement(cuurentElement.Value, upper))
                {
                    result.Add(cuurentElement.Value);
                }
                else if (NewElementIsEqualToCurrentElement(cuurentElement.Value, lower) || NewElementIsEqualToCurrentElement(cuurentElement.Value, upper))
                {
                    result.Add(cuurentElement.Value);
                }

                if (cuurentElement.LeftChild != null)
                {
                    queue.Enqueue(cuurentElement.LeftChild);
                }

                if (cuurentElement.RightChild != null)
                {
                    queue.Enqueue(cuurentElement.RightChild);
                }
            }

            return result;

        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }

            var current = this.Root;
            Node<T> parent = null;

            while (current.LeftChild != null)
            {
                parent = current;
                current = current.LeftChild;
            }

            parent.LeftChild = current.RightChild;

            Count--;
        }

        public void DeleteMax()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }

            var current = this.Root;
            Node<T> parent = null;
            while (current.RightChild != null)
            {
                parent = current;
                current = current.RightChild;
            }

            parent.RightChild = current.LeftChild;
            Count--;
        }

        public int GetRank(T element)
        {
            return DfsRank(this.Root, element);
        }

        private int DfsRank(Node<T> root, T element)
        {
            if (root == null)
            {
                return 0;
            }

            if (NewElementIsLessThanCurrentElement(root.Value, element))
            {
                return this.DfsRank(root.LeftChild, element);
            }

            if (NewElementIsBiggerThanCurrentElement(root.Value, element))
            {
                return 1 + this.DfsRank(root.LeftChild, element) + this.DfsRank(root.RightChild, element);
            }

            return 1;
        }


        private bool NewElementIsLessThanCurrentElement(T current, T element)
        {
            if (element.CompareTo(current) < 0)
            {
                return true;
            }

            return false;
        }

        private bool NewElementIsBiggerThanCurrentElement(T current, T element)
        {
            if (element.CompareTo(current) > 0)
            {
                return true;
            }

            return false;
        }

        private bool NewElementIsEqualToCurrentElement(T current, T element)
        {
            if (element.CompareTo(current) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
