namespace Recently_Used_List;

public interface IStringStackSet
{
    public int Capacity { get; }

    public int Length { get; }

    string ElementAt(int i);

    void Push(string item);

    string Pop();
}

