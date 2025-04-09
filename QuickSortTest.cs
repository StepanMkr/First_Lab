using NUnit.Framework;
using NUnit.Framework.Legacy;

[TestFixture]
public class QuickSortTests
{
    [Test]
    public void TestEmptyArray()
    {
        // Arrange
        int[] input = new int[0];

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[0], input);
    }

    [Test]
    public void TestSingleElementArray()
    {
        // Arrange
        int[] input = { 5 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 5 }, input);
    }

    [Test]
    public void TestAlreadySortedArray()
    {
        // Arrange
        int[] input = { 1, 2, 3, 4, 5 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, input);
    }

    [Test]
    public void TestReverseSortedArray()
    {
        // Arrange
        int[] input = { 5, 4, 3, 2, 1 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, input);
    }

    [Test]
    public void TestDuplicateElements()
    {
        // Arrange
        int[] input = { 5, 2, 8, 1, 9, 4, 7, 6, 3 };

        // Act
        QuickSort.Sort(input);

        // Assert
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, input);
    }

    [Test]
    public void TestLargeArray()
    {
        // Arrange
        Random rand = new Random();
        int[] input = Enumerable.Range(0, 10000).Select(i => rand.Next(100000)).ToArray();

        // Act
        QuickSort.Sort(input);

        // Assert
        Array.Sort(input); // Используем стандартный метод для проверки корректности
    }
}