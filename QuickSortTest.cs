using NUnit.Framework;
using NUnit.Framework.Legacy;

[TestFixture]
public class QuickSortTests
{
    [Test]
    public void TestEmptyArray()
    {
        int[] input = new int[0];
        QuickSort.Sort(input);
        CollectionAssert.AreEqual(new int[0], input);
    }

    [Test]
    public void TestSingleElementArray()
    {
        int[] input = { 5 };
        QuickSort.Sort(input);
        CollectionAssert.AreEqual(new int[] { 5 }, input);
    }

    [Test]
    public void TestAlreadySortedArray()
    {
        int[] input = { 1, 2, 3, 4, 5 };
        QuickSort.Sort(input);
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, input);
    }

    [Test]
    public void TestReverseSortedArray()
    {
        int[] input = { 5, 4, 3, 2, 1 };
        QuickSort.Sort(input);
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, input);
    }

    [Test]
    public void TestDuplicateElements()
    {
        int[] input = { 5, 2, 8, 1, 9, 5, 2, 4, 7, 6, 3 };
        QuickSort.Sort(input);
        CollectionAssert.AreEqual(new int[] { 1, 2, 2, 3, 4, 5, 5, 6, 7, 8, 9 }, input);
    }

    [Test]
    public void TestLargeArray()
    {
        Random rand = new Random();
        int[] input = Enumerable.Range(0, 10000).Select(i => rand.Next(100000)).ToArray();
        int[] copy = (int[])input.Clone();
        QuickSort.Sort(input);
        Array.Sort(copy);
        Assert.That(input, Is.EqualTo(copy));
    }

    [Test]
    public void TestArrayWithNegativeNumbers()
    {
        // Arrange
        int[] input = { -3, -1, -2, 0, 2, 1 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { -3, -2, -1, 0, 1, 2 }, input);
    }

    [Test]
    public void TestAllSameElements()
    {
        // Arrange
        int[] input = { 7, 7, 7, 7, 7 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 7, 7, 7, 7, 7 }, input);
    }

    [Test]
    public void TestArrayWithMinMaxValues()
    {
        // Arrange
        int[] input = { int.MaxValue, 0, int.MinValue };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { int.MinValue, 0, int.MaxValue }, input);
    }

    [Test]
    public void TestDescendingWithDuplicates()
    {
        // Arrange
        int[] input = { 9, 8, 7, 7, 6, 5, 5, 4 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 4, 5, 5, 6, 7, 7, 8, 9 }, input);
    }

    [Test]
    public void TestTwoElementArray()
    {
        // Arrange
        int[] input = { 2, 1 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 1, 2 }, input);
    }
}
