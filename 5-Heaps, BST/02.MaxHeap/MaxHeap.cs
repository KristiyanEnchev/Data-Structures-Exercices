namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private readonly List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size { get { return this.elements.Count; } }

        public void Add(T element)
        {
            this.elements.Add(element);
            this.Hipify();
        }

        private void Hipify()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = this.GetParentIndex(currentIndex);
            while (parentIndex >= 0 && this.elements[currentIndex].CompareTo(this.elements[parentIndex]) > 0)
            {
                SwapElemenets(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = this.GetParentIndex(currentIndex);
            }
        }

        public T Peek()
        {
            this.CheckIfEmpty();
            return this.elements[0];
        }

        private void CheckIfEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private void SwapElemenets(int current, int parent)
        {
            T temp = this.elements[current];
            elements[current] = elements[parent];
            elements[parent] = temp;
        }
    }
}
