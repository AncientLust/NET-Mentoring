namespace LCD_Digits;

public class LcdConverter
{
    private static readonly List<string> Digit = new()
    {
        "._.\n|.|\n|_|", // 0
        "...\n..|\n..|", // 1
        "._.\n._|\n|_.", // 2
        "._.\n._|\n._|", // 3
        "...\n|_|\n..|", // 4
        "._.\n|_.\n._|", // 5
        "._.\n|_.\n|_|", // 6
        "._.\n..|\n..|", // 7
        "._.\n|_|\n|_|", // 8
        "._.\n|_|\n..|", // 9
    };

    public string Convert(int digit)
    {
        if (digit is < 0 or > 9)
            throw new ArgumentOutOfRangeException();

        return Digit[digit];
    }
}