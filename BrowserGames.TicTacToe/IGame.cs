using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TicTacToe;
public interface IGame
{
    GamePiece[,] Board { get; init; }
    bool IsGameOver { get; }
    int Turns { get; }
    GamePiece CurrentTurn { get; }

    event EventHandler<WinnerEventArgs> OnWin;

    GamePiece GetTile(int row, int column);
    bool IsOccupied(int row, int column);
    bool IsWithinBounds(int row, int col);
    void PerformPlay(int row, int column);
    void ResetBoard();
    bool SetTile(int row, int column, GamePiece piece);
}
