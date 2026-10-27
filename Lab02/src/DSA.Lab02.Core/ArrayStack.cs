using System;

namespace DSA.Lab02
{
    public sealed class ArrayStack<T>
    {
        private readonly T[] _items;
        private int _count;

        public ArrayStack(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Push(T item)
        {
            if (_count == Capacity)
            {
                throw new InvalidOperationException("Added item exceeds capacity of ArrayStack");
            }

            _items[_count++] = item;
        }

        public T Peek()
        {
            return _count == 0 
                ? throw new InvalidOperationException("ArrayStack is empty") 
                : _items[_count - 1];
        }

        public T Pop()
        {
            return _count == 0 
                ? throw new InvalidOperationException("ArrayStack is empty") 
                : _items[--_count];
        }
    }
}
