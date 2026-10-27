using System;

namespace DSA.Lab02
{
    public sealed class RingQueue<T>
    {
        private readonly T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public RingQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Enqueue(T item)
        {
            if (_count == Capacity)
            {
                throw new InvalidOperationException("Added item exceeds capacity of RingQueue");
            }

            _count++;
            _items[_tail++] = item;
            _tail %= Capacity;
        }

        public T Peek()
        {
            return _count == 0 
                ? throw new InvalidOperationException("RingQueue is empty") 
                : _items[_tail - 1];
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("RingQueue is empty");
            }

            _count--;
            var item = _items[_head++];
            _head %= Capacity;
            return item;
        }
    }
}
