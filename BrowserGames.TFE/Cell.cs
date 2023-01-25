namespace BrowserGames.TFE;

public class Cell
{
    public int Row { get; }
    public int Column { get; }

    public int Value { get; set; }


    public Cell(int row, int column, int value)
    {
        Row = row;
        Column = column;
        Value = value;
    }


    public void Set(int value) => Value = value;

    public static explicit operator int(Cell cell)
    {
        return cell.Value;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
