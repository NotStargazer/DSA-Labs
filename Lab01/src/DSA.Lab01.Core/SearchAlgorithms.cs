using System;

namespace DSA.Lab01
{
    public static class SearchAlgorithms
    {
        public static int LinearSearch(int[] values, int target)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int BinarySearch(int[] sortedValues, int target)
        {
            if (sortedValues == null)
            {
                throw new ArgumentNullException(nameof(sortedValues));
            }

            return BinarySearchRecursive(new Span<int>(sortedValues), target);
        }

        private static int BinarySearchRecursive(Span<int> sortedValues, int target, int minIndex = 0)
        {
            var upper = sortedValues.Length >> 1;
            var lower = upper - 1;
            if (sortedValues[upper] == target)
            {
                return upper + minIndex;
            }
            if (sortedValues.Length == 1)
            {
                return -1;
            }
            if (sortedValues[lower] == target)
            {
                return lower + minIndex;
            }
            if (sortedValues.Length == 2)
            {
                return -1;
            }

            if (sortedValues[upper] > target)
            {
                return BinarySearchRecursive(
                    sortedValues[..lower],
                    target,
                    minIndex);
            }

            return BinarySearchRecursive(
                sortedValues[upper..],
                target,
                upper + minIndex);
        }
    }
}
