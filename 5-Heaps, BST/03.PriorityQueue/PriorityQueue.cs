namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {

        private List<T> elements;

        public PriorityQueue()
        {
            this.elements = new List<T>();
        }

        public int Size { get { return this.elements.Count; } }

        public T Dequeue()
        {
            var top = this.Peek();
            //SwapElemenets(0, Size - 1);
            elements[0] = elements[Size - 1];
            elements.RemoveAt(Size - 1);
            HeapifyDown();
            return top;
        }

        private void HeapifyDown()
        {
            int index = 0;
            int leftChildIndex = GetLeftChildIndex(index);

            while (leftChildIndex > 0 && leftChildIndex < Size && elements[index].CompareTo(elements[leftChildIndex]) < 0)
            {
                int swapingIndex = leftChildIndex;
                int rightChildIndex = GetRightChildIndex(index);

                if (rightChildIndex > 0 && rightChildIndex < Size && elements[swapingIndex].CompareTo(elements[rightChildIndex]) < 0)
                {
                    swapingIndex = rightChildIndex;
                }
                SwapElemenets(swapingIndex, index);
                index = swapingIndex;
                leftChildIndex = GetLeftChildIndex(index);

            }
        }

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HipifyUp();
        }

        private void HipifyUp()
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
        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }
        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }

        private void SwapElemenets(int current, int parent)
        {
            T temp = this.elements[current];
            elements[current] = elements[parent];
            elements[parent] = temp;
        }
    }
}
