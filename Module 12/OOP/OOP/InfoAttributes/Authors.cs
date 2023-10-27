using OOP.Interfaces;

namespace OOP.InfoAttributes;

internal class Authors : IInfo
{
    public string InfoName { get; }
    public object InfoValue { get; }
    string IInfo.InfoStringValue => string.Join(", ", (List<string>)InfoValue);

    public Authors(List<string> authors)
    {
        if (authors.Count == 0)
            throw new ArgumentException("Must contain at least one Author");

        InfoValue = authors;
        InfoName = GetType().Name;
    }
}
