namespace Recently_Used_List;

public class StringStackSet : IStringStackSet
{
    private readonly FromPreviousTask.DoublyLinkedList<string> _storage = new();
    public int Capacity { get; }
    public int Length => _storage.Length;

    public StringStackSet(int capacity)
    {
        Capacity = capacity;
    }

    public string ElementAt(int i)
    {
        return _storage.ElementAt(i);
    }

    public string Pop()
    {
        var storageLength = _storage.Length;
        if (storageLength == 0) throw new InvalidOperationException();

        return _storage.RemoveAt(--storageLength);
    }

    public void Push(string item)
    {
        if (string.IsNullOrEmpty(item)) throw new InvalidDataException();

        if (_storage.Contains(item))
        {
            if (Length == 1) return;

            _storage.Remove(item);
        }

        _storage.Add(item);

        if (Length > Capacity)
        {
            _storage.RemoveAt(0);
        }
    }
}