using System;
using System.Collections.Generic;

namespace DSA.Lab04B
{
    /// <summary>
    /// Priority queue implemented with a binary min-heap.
    ///
    /// Invariant:
    /// each parent's Priority is <= the Priority of each of its children.
    ///
    /// The heap is NOT fully sorted.
    /// </summary>
    public sealed class BinaryHeapDispatchQueue<T>
    {
        private readonly List<PriorityEntry<T>> _heap = new List<PriorityEntry<T>>();

        public int Count => _heap.Count;

        public void Enqueue(T item, int priority)
        {
            var i = Count;
            _heap.Add(new PriorityEntry<T>(item, priority));
            if (Count > 1)
            {
                SiftUp(i);
            }
        }

        public PriorityEntry<T> PeekNext()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }

            return _heap[0];
        }

        public PriorityEntry<T> DequeueNext()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }

            var item = _heap[0];
            var endIndex = Count - 1;
            (_heap[0], _heap[endIndex]) = (_heap[endIndex], _heap[0]);
            _heap.RemoveAt(endIndex);
            if (Count > 1)
            {
                SiftDown(0);
            }
            return item;
        }

        private void SiftUp(int index)
        {
            var parentIndex = ParentIndex(index);
            var priority = _heap[index].Priority;
            while (priority < _heap[parentIndex].Priority)
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = ParentIndex(index);
            }
        }

        private void SiftDown(int index)
        {
            var priority = _heap[index].Priority;
            var leftIndex = LeftChildIndex(index);
            var rightIndex = RightChildIndex(index);
            while (rightIndex < Count)
            {
                var swap = _heap[rightIndex].Priority < _heap[leftIndex].Priority ? rightIndex : leftIndex;
                if (priority <= _heap[swap].Priority)
                {
                    return;
                }
                Swap(index, swap);
                index = swap;
                leftIndex = LeftChildIndex(index);
                rightIndex = RightChildIndex(index);

            }
            if (leftIndex < Count && priority > _heap[leftIndex].Priority)
            {
                Swap(index, leftIndex);
            }
        }

        private static int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private static int LeftChildIndex(int index)
        {
            return 2 * index + 1;
        }

        private static int RightChildIndex(int index)
        {
            return 2 * index + 2;
        }

        private void Swap(int a, int b)
        {
            (_heap[a], _heap[b]) = (_heap[b], _heap[a]);
        }
    }
}
