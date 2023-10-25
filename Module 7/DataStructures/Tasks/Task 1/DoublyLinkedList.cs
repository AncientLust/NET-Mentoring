using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks;

public partial class DoublyLinkedList<T> : IDoublyLinkedList<T>
{
    public Node HeadNode;

    public int Length => GetLength();

    private void AddToEnd(Node currentNode, Node previousNode, T data)
    {
        if (currentNode is null)
        {
            previousNode.Next = new Node(data, previousNode);
        }
        else
        {
            AddToEnd(currentNode.Next, currentNode, data);
        }
    }

    private int GetLength()
    {
        if (HeadNode is null) return 0;

        var currentNode = HeadNode;
        var count = 1;

        while (currentNode.Next is not null)
        {
            currentNode = currentNode.Next;
            count++;
        }

        return count;
    }

    public void Add(T data)
    {
        if (HeadNode is null)
        {
            HeadNode = new Node(data);
        }
        else
        {
            AddToEnd(HeadNode.Next, HeadNode,  data);
        }
    }

    public void AddAt(int index, T data)
    {
        if (HeadNode is null || index < 0 || index > Length)
            throw new IndexOutOfRangeException();

        var newNode = new Node(data);
        if (index == 0)
        {
            newNode.Next = HeadNode;
            HeadNode = newNode;
            return;
        }

        var nodeToShift = HeadNode;
        for (var i = 0; i < index; i++)
        {
            if (nodeToShift.Next is null)
            {
                nodeToShift.Next = new Node(data, nodeToShift);
                return;
            }

            nodeToShift = nodeToShift.Next;
        }

        var shiftedNodePrevious = nodeToShift.Previous;
        
        newNode = new(data, shiftedNodePrevious);
        newNode.Next = nodeToShift;

        shiftedNodePrevious.Next = newNode;
        nodeToShift.Previous = newNode;
    }

    public T ElementAt(int index)
    {
        if (HeadNode is null || index < 0|| index >= Length) 
            throw new IndexOutOfRangeException();

        var currentNode = HeadNode;
        for (var i = 0; i <= index; i++)
        {
            if (index == i) break;
            currentNode = currentNode.Next;
        }

        return currentNode.Data;
    }

    public void Remove(T item)
    {
        var currentNode = HeadNode;
        while (currentNode != null)
        {
            if (currentNode.Data.Equals(item))
            {
                if (currentNode == HeadNode)
                {
                    HeadNode = currentNode.Next;
                    HeadNode.Previous = null;
                    HeadNode.Next.Previous = HeadNode;
                }
                else
                {
                    if (currentNode.Next is null)
                    {
                        currentNode.Previous.Next = null;
                        return;
                    }

                    currentNode.Previous.Next = currentNode.Next;
                    if (currentNode.Next is not null)
                    {
                        currentNode.Next.Previous = currentNode.Previous;
                    }
                }

                return;
            }

            currentNode = currentNode.Next;
        }
    }

    public T RemoveAt(int index)
    {
        if (HeadNode is null || index < 0 || index >= Length)
            throw new IndexOutOfRangeException();

        var currentNode = HeadNode;
        for (var i = 0; i < index; i++)
        {
            currentNode = currentNode.Next;
        }

        if (currentNode.Previous is null)
        {
            HeadNode = currentNode.Next;
            if (HeadNode != null)
            {
                HeadNode.Previous = null;
            }
        }
        else
        {
            currentNode.Previous.Next = currentNode.Next;
            if (currentNode.Next is not null)
            {
                currentNode.Next.Previous = currentNode.Previous;
            }
        }

        return currentNode.Data;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new DoublyLinkedListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}