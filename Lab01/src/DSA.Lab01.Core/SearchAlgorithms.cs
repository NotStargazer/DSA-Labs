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

            //return BinarySearchStandard(sortedValues, target);
            return BinarySearchRecursive(sortedValues, target, 0, sortedValues.Length - 1);
        }

        private static int BinarySearchStandard(int[] sortedValues, int target)
        {
            var min = 0;
            var max = sortedValues.Length - 1;
            while (min <= max)
            {
                var centre = min + ((max - min) >> 1);
                
                if (sortedValues[centre] == target)
                {
                    return centre;
                }
                if (sortedValues[centre] < target)
                {
                    min = centre + 1;
                }
                else
                {
                    max = centre - 1;
                }
            }

            return -1;
        }

        private static int BinarySearchRecursive(int[] sortedValues, int target, int min, int max)
        {
            if (min > max)
            {
                return -1;
            }
            
            var center = min + ((max - min) >> 1);
            if (sortedValues[center] == target)
            {
                return center;
            }

            if (sortedValues[center] > target)
            {
                return BinarySearchRecursive(sortedValues, target, min, center - 1);
            }

            return BinarySearchRecursive(sortedValues, target, center + 1, max);
        }
    }
}
