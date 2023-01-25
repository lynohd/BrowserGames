using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TicTacToe;
public class PiecePlacedEventArgs : EventArgs
{
    public int Row { get; }
    public int Col { get; }
    public GamePiece Piece { get; }

    public PiecePlacedEventArgs(int row, int col, GamePiece piece)
    {
        this.Row = row; 
        this.Col = col;
        this.Piece =  piece;
    }
}
