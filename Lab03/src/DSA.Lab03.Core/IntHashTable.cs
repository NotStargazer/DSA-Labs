using System;
using System.Collections.Generic;

namespace DSA.Lab03
{
    public sealed class IntHashTable<TValue>
    {
        private readonly List<Entry<TValue>>[] _buckets;
        private int _count;

        public IntHashTable(int bucketCount)
        {
            if (bucketCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bucketCount));
            }

            _buckets = new List<Entry<TValue>>[bucketCount];
            for (int i = 0; i < _buckets.Length; ++i)
            {
                _buckets[i] = new List<Entry<TValue>>();
            }
        }

        public int Count => _count;
        public int BucketCount => _buckets.Length;

        public void Set(int key, TValue value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var bucketIndex = key % BucketCount;
            var bucket = _buckets[bucketIndex];
            var newEntry = new Entry<TValue>(key, value);
            for (var i = 0; i < bucket.Count; i++)
            {
                var entry = bucket[i];
                if (entry.Value == null)
                {
                    continue;
                }

                if (entry.Key == key)
                {
                    bucket[i] = newEntry;
                    return;
                }
            }

            bucket.Add(newEntry);
            _count++;
        }

        public bool TryGetValue(int key, out TValue value)
        {
            var bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];
            foreach (var entry in bucket)
            {
                if (entry.Key == key)
                {
                    value = entry.Value;
                    return true;
                }
            }

            value = default!;
            return false;
        }

        public bool ContainsKey(int key)
        {
            var bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];
            foreach (var entry in bucket)
            {
                if (entry.Key == key)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetBucketIndex(int key)
        {
            if (key < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            return key % _buckets.Length;
        }
    }
}
