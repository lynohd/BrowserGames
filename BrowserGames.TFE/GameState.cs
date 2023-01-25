using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TFE;
public class GameState
{
    public int Score { get; private set; }

    private GameBoard Board { get; set; }

    public Cell[,] Cells => Board.Cells;


    public Cell GetCell(int row, int column)
    {
        return Cells[row, column];
    }

    public GameState()
    {
        Board = new();
    }

}
