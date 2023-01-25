using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TFE;
public class GameBoard
{
    const int ROWS = 4;
    const int COLUMNS = 4;

    private readonly Cell[,] _cells;
    public Cell[,] Cells => _cells;

    public GameBoard()
    {
        _cells = new Cell[ROWS, COLUMNS];
        Initialize();
    }

    private void Initialize()
    {
        for(int _row = Cells.GetLowerBound(0); _row < 4; _row++)
        {
            for(int _col = Cells.GetLowerBound(0); _col < 4; _col++)
            {
                _cells[_row, _col] = new Cell(_row, _col, 2);
            }
        }
    }
}
