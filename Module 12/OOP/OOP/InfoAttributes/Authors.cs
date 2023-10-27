using OOP.Interfaces;

namespace OOP.InfoAttributes;

internal class Authors : IInfo
{
    public string InfoName { get; set; }
    public object InfoValue { get; set; }
    public string InfoStringValue { get; set; }

    public Authors() {}

    public Authors(List<string> authors)
    {
        if (authors.Count == 0)
            throw new ArgumentException("Must contain at least one Author");

        InfoValue = authors;
        InfoName = GetType().Name;
        InfoStringValue = string.Join(", ", (List<string>)InfoValue);
    }
}
