using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TicTacToe;

public  class CpuPlayer : IDisposable
{
    private readonly Game _game;

    public CpuPlayer(Game game)
    {
        _game = game;
        _game.OnTurn += OnTurn;
    }

    private void OnTurn(object? sender, PiecePlacedEventArgs e)
    {
        if(e.Piece == Game.Player && !_game.IsGameOver)
        {
            PerformCpuPlay();
        }
    }


    List<(int row, int col)> freeTiles = new(); 
    public void PerformCpuPlay()
    {
        freeTiles.Clear();
        for(int row = _game.Board.GetLowerBound(0); row <= _game.Board.GetUpperBound(0); row++)
        {
            for(int col = _game.Board.GetLowerBound(1); col <= _game.Board.GetUpperBound(1); col++)
            {
                if(_game.Board[row,col] == GamePiece.Empty)
                {
                    freeTiles.Add((row, col));
                }
            }
        }

        Console.WriteLine("free tiles:" + freeTiles.Count);

        if(freeTiles.Count > 0)
        {
            var random = freeTiles[Random.Shared.Next(0, freeTiles.Count)];
            _game.SetTile(random.row, random.col, Game.Enemy);
        }
    
    }

    public void Dispose()
    {
        _game.OnTurn -= OnTurn;
    }
}
