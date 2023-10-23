﻿namespace Recently_Used_List.FromPreviousTask;

public interface IDoublyLinkedList<T> : IEnumerable<T>
{
    public int Length { get; }

    void Add(T e);

    void AddAt(int index, T e);

    void Remove(T item);

    T RemoveAt(int index);

    T ElementAt(int index);
}
