using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Lab06
{
    public sealed class HashedPriorityMinFrontier<T>
    {
        private struct PriorityIndex
        {
            public PriorityIndex(int index, int priority)
            {
                Index = index;
                Priority = priority;
            }
            
            public int Index;
            public int Priority;
        }
        
        public readonly struct Entry
        {
            private readonly HashedPriorityMinFrontier<T> _heap;
            public Entry(HashedPriorityMinFrontier<T> heap, T value)
            {
                _heap = heap;
                Value = value;
            }

            public T Value { get; }

            public int Priority
            {
                get => _heap._priorities[Value].Priority;
                set
                {
                    var a = _heap._priorities[Value];
                    a.Priority = value;
                    _heap._priorities[Value] = a;
                }
            }
        }

        private readonly List<Entry> _heap = new();
        private readonly Dictionary<T, PriorityIndex> _priorities = new();

        public int Count => _heap.Count;

        public void Enqueue(T value, int priority)
        {
            var index = _heap.Count;
            _heap.Add(new Entry(this, value));
            _priorities.Add(value, new PriorityIndex(index, priority));
            SiftUp(_heap.Count - 1);
        }

        public Entry Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The frontier is empty.");
            }

            Entry result = _heap[0];
            int last = _heap.Count - 1;

            if (last == 0)
            {
                _priorities.Remove(result.Value);
                _heap.RemoveAt(0);
                return result;
            }

            _heap[0] = _heap[last];
            _priorities.Remove(result.Value);
            var pi = _priorities[_heap[0].Value];
            pi.Index = 0;
            _priorities[_heap[0].Value] = pi;
            _heap.RemoveAt(last);
            SiftDown(0);
            return result;
        }
        
        public bool ContainsKey(T key)
        {
            return _priorities.ContainsKey(key);
        }

        public int this[T entry]
        {
            get => _priorities[entry].Priority;
            set
            {
                var p = _priorities[entry];
                if (p.Priority == value)
                {
                    return;
                }

                _priorities[entry] = new PriorityIndex(p.Index, value);
                if (value > p.Priority)
                {
                    SiftDown(p.Index);
                }
                
                if (value < p.Priority)
                {
                    SiftUp(p.Index);
                }
            }
        }
        
        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_heap[parent].Priority <= _heap[index].Priority)
                {
                    return;
                }

                Swap(parent, index);
                index = parent;
            }
        }

        private void SiftDown(int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                if (left >= _heap.Count)
                {
                    return;
                }

                int right = left + 1;
                int smallerChild = left;

                if (right < _heap.Count && _heap[right].Priority < _heap[left].Priority)
                {
                    smallerChild = right;
                }

                if (_heap[index].Priority <= _heap[smallerChild].Priority)
                {
                    return;
                }

                Swap(index, smallerChild);
                index = smallerChild;
            }
        }

        private void Swap(int a, int b)
        {
            (_heap[a], _heap[b]) = (_heap[b], _heap[a]);
            var ap = _priorities[_heap[a].Value];
            ap.Index = a;
            _priorities[_heap[a].Value] = ap;
            var bp = _priorities[_heap[b].Value];
            bp.Index = b;
            _priorities[_heap[b].Value] = bp;
        }
    }
}
