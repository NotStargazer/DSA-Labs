using System;

namespace DSA.Lab01
{
    public static class SortAlgorithms
    {
        public static void InsertionSort(int[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            for (var i = 1; i < values.Length; i++)
            {
                if (values[i - 1] > values[i])
                {
                    for (var j = i; j >= 1; j--)
                    {
                        if (values[j - 1] < values[j])
                        {
                            break;
                        }
                        (values[j - 1], values[j]) = (values[j], values[j - 1]);
                    }
                }
            }
        }
    }
}
