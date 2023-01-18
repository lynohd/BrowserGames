using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TicTacToe;
public class WinnerEventArgs : EventArgs
{
    public GamePiece Player { get; }
    public WinnerEventArgs(GamePiece player)
    {
        this.Player = player;
    }
}