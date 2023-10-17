namespace Mine_Fields;

public class MineField
{
    private int _rowCount;
    private int _colCount;

    private readonly (int x, int y)[] _neighborDirections = new[]
    {
        (-1, 0),
        (1, 0),
        (0, -1),
        (0, 1),
        (1, 1),
        (1, -1),
        (-1, 1),
        (-1, -1)
    };

    public string[,] GetMineMap(string[,] field)
    {
        _rowCount = field.GetLength(0);
        _colCount = field.GetLength(1);
        for (var x = 0; x < _rowCount; x++)
        {
            for (var y = 0; y < _colCount; y++)
            {
                switch (field[x, y])
                {
                    case "*":
                        IncrementNeighbors(field, x, y);
                        break;
                    case ".":
                        field[x, y] = "0";
                        break;
                }
            }
        }

        return field;
    }

    private void IncrementNeighbors(string[,] field, int x, int y)
    {
        foreach (var direction in _neighborDirections)
        {
            var newX = x + direction.x;
            var newY = y + direction.y;

            if (IsWithinBounds(newX, newY))
            {
                field[newX, newY] = IncrementField(field[newX, newY]);
            }
        }
    }

    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < _rowCount && y < _colCount;
    }

    private string IncrementField(string field)
    {
        if (field == "*") return "*";
        if (field == ".") return "1";

        if (!int.TryParse(field, out var num)) throw new InvalidCastException();
        num++;

        return num.ToString();
    }
}