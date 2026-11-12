using System.Collections.Generic;
using NUnit.Framework;

namespace DSA.Lab06.Tests;

public sealed class HashedPriorityMinHeapTests
{
    [Test]
    public void HashedPriorityMinHeap_Dequeue_CorrectOrder()
    {
        var heap = GetMinHeap();
        var outList = new List<string>();
        while (heap.Count > 0)
        {
            outList.Add(heap.Dequeue().Value);
        }
        Assert.That(outList, Is.EqualTo(["very high", "high", "mid", "low", "very low"]));
    }
    
    [Test]
    public void HashedPriorityMinHeap_Dequeue_CorrectOrderAfterPrioritySwap()
    {
        var heap = GetMinHeap();
        (heap["very low"], heap["very high"]) = (heap["very high"], heap["very low"]);
        var outList = new List<string>();
        while (heap.Count > 0)
        {
            outList.Add(heap.Dequeue().Value);
        }
        Assert.That(outList, Is.EqualTo(["very low", "high", "mid", "low", "very high"]));
    }
    
    [Test]
    public void HashedPriorityMinHeap_Dequeue_MaintainsZeroIndexAfterDequeue()
    {
        var heap = GetMinHeap();
        var v1 = heap.Dequeue().Value;
        Assert.DoesNotThrow(() => (heap["low"], heap["high"]) = (heap["high"], heap["low"]));
        var v2 = heap.Dequeue().Value;

        Assert.That(new[]{v1, v2}, Is.EqualTo(["very high", "low"]));
    }

    private HashedPriorityMinFrontier<string> GetMinHeap()
    {
        var minHeap = new HashedPriorityMinFrontier<string>();
        minHeap.Enqueue("low", 10);
        minHeap.Enqueue("high", 2);
        minHeap.Enqueue("mid", 5);
        minHeap.Enqueue("very high", 1);
        minHeap.Enqueue("very low", 21);

        return minHeap;
    }
}