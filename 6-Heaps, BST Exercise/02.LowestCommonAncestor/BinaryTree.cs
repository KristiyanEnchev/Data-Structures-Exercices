namespace _02.LowestCommonAncestor
{
    using System;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }
            this.RightChild = rightChild;
            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var one = Search(first);
            var two = Search(second);

            var parent = two.Parent;
            while (!parent.Equals(one.Parent) || !parent.Equals(two.Parent))
            {
                if (!parent.Equals(one.Value))
                {
                    one = one.Parent;
                }

                if (!parent.Equals(two.Value))
                {
                    two = two.Parent;
                }
            }

            return two.Value;
        }

        public IAbstractBinaryTree<T> Search(T element)
        {
            var current = this;
            while (current != null && !NewElementIsEqualToCurrentElement(current.Value, element))
            {
                if (NewElementIsLessThanCurrentElement(current.Value, element))
                {
                    if (current.LeftChild == null)
                    {
                        break;
                    }
                    current = current.LeftChild;
                }
                else if (NewElementIsBiggerThanCurrentElement(current.Value, element))
                {
                    if (current.RightChild == null)
                    {
                        break;
                    }
                    current = current.RightChild;
                }
            }

            //return new BinaryTree<T>(current.Value, current.LeftChild, current.RightChild);
            return current;
        }

        //public IAbstractBinaryTree<T> Search(T element)
        //{
        //   var current = this;

        //    while (current != null || element.CompareTo(current.Value) == 0)
        //    {
        //        if (element.CompareTo(current.Value) < 0)
        //        {
        //            current = current.LeftChild;
        //        }
        //        else if (element.CompareTo(current.Value) > 0)
        //        {
        //            current = current.RightChild;
        //        }
        //        else
        //        {
        //            return current;
        //        }
        //    }

        //    return null;
        //}

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
