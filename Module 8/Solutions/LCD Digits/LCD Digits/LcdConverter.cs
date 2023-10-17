namespace LCD_Digits;

public class LcdConverter
{
    private static readonly List<string[]> DigitMap = new()
    {
        new[]
        {
            "._.",
            "|.|",
            "|_|"   // 0
        },
        new[]
        {
            "...",
            "..|",
            "..|"   // 1
        },
        new[]
        {
            "._.",
            "._|",
            "|_."   // 2
        },
        new[]
        {
            "._.",
            "._|",
            "._|"   // 3
        },
        new[]
        {
            "...",
            "|_|",
            "..|"   // 4
        },
        new[]
        {
            "._.",
            "|_.",
            "._|"   // 5
        },
        new[]
        {
            "._.",
            "|_.",
            "|_|"   // 6
        },
        new[]
        {
            "._.",
            "..|",
            "..|"   // 7
        },
        new[]
        {
            "._.",
            "|_|",
            "|_|"   // 8
        },
        new[]
        {
            "._.",
            "|_|",
            "..|"   // 9
        }
    };

    public string Convert(int number)
    {
        if (number < 0)
            throw new ArgumentOutOfRangeException();

        if (number is >= 0 and <= 9)
            return string.Join('\n', DigitMap[number]);

        int[] numberDigits = number.ToString().Select(d => d - '0').ToArray();
        List<string[]> mappedDigits = numberDigits.Select(d => DigitMap[d]).ToList();

        string line1 = string.Join(' ', mappedDigits.Select(md => md[0]));
        string line2 = string.Join(' ', mappedDigits.Select(md => md[1]));
        string line3 = string.Join(' ', mappedDigits.Select(md => md[2]));

        return line1 + '\n' + line2 + '\n' + line3;
    }
}