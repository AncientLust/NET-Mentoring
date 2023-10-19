using FluentAssertions;
using Xunit;

namespace Recently_Used_List.Tests;

public class StringStackSetTests
{
    [Fact]
    public void Should_Be_Initially_Empty()
    {
        var uniqueQueue = new StringStackSet(3);

        var actualLength = uniqueQueue.Length;

        actualLength.Should().Be(0);
    }

    [Fact]
    public void Should_Throw_InvalidOperationException_If_Popped_Empty()
    {
        var uniqueQueue = new StringStackSet(3);

        var action = () => uniqueQueue.Pop();

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Return_Capacity()
    {
        var uniqueQueue = new StringStackSet(3);

        var actualCapacity = uniqueQueue.Capacity;

        actualCapacity.Should().Be(3);
    }

    [Fact]
    public void Should_Throw_InvalidDataException_If_Empty_String_Inserted()
    {
        var uniqueQueue = new StringStackSet(3);
        
        var action = () => uniqueQueue.Push("");

        action.Should().Throw<InvalidDataException>();
    }

    [Fact]
    public void Last_Inserted_Item_Should_Be_First_Returned()
    {
        var uniqueQueue = new StringStackSet(3);
        
        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");
        var actualElement = uniqueQueue.Pop();

        actualElement.Should().Be("Task 2");
    }

    [Fact]
    public void First_Inserted_Item_Should_Be_Last_Returned()
    {
        var uniqueQueue = new StringStackSet(3);

        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");

        var firstExtractedElement = uniqueQueue.Pop();
        var lastExtractedElement = uniqueQueue.Pop();

        firstExtractedElement.Should().Be("Task 2");
        lastExtractedElement.Should().Be("Task 1");
    }

    [Fact]
    public void Should_Return_Item_By_Index()
    {
        var uniqueQueue = new StringStackSet(3);
        
        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");
        var actualElement = uniqueQueue.ElementAt(0);

        actualElement.Should().Be("Task 1");
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_Index_Out_Of_Range()
    {
        var uniqueQueue = new StringStackSet(3);

        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");

        var action = () => uniqueQueue.ElementAt(3);

        action.Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void Duplicate_Insertions_Does_Not_Change_Length()
    {
        var uniqueQueue = new StringStackSet(3);
        
        uniqueQueue.Push("Task 1");
        var lengthBeforePush = uniqueQueue.Length;
        uniqueQueue.Push("Task 1");
        var lengthAfterPush = uniqueQueue.Length;

        Assert.Equal(lengthBeforePush, lengthAfterPush);
    }

    [Fact]
    public void Duplicate_Insertions_Should_Be_Moved_Rather_Than_Added()
    {
        var uniqueQueue = new StringStackSet(3);
        
        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");
        uniqueQueue.Push("Task 1");
        var firstElement = uniqueQueue.Pop();
        var secondElement = uniqueQueue.Pop();
        var actualLength = uniqueQueue.Length;

        firstElement.Should().Be("Task 1");
        secondElement.Should().Be("Task 2");
        actualLength.Should().Be(0);
    }

    [Fact]
    public void Overflow_Insertion_Should_Drop_The_Oldest_Item()
    {
        var uniqueQueue = new StringStackSet(2);

        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");
        uniqueQueue.Push("Task 3");
        var firstElement = uniqueQueue.ElementAt(0);

        firstElement.Should().Be("Task 2");
    }

    [Fact]
    public void Overflow_Should_Keep_Specified_Capacity()
    {
        var uniqueQueue = new StringStackSet(2);

        uniqueQueue.Push("Task 1");
        uniqueQueue.Push("Task 2");
        uniqueQueue.Push("Task 3");
        var actualLength = uniqueQueue.Length;

        actualLength.Should().Be(2);
    }
}
