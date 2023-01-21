﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TicTacToe.Tests;


/// <summary>
/// Proxy of Game easier testing
/// </summary>
public  class GameProxy 
{
    private readonly IGame _game;
    public GameProxy(IGame game)
    {
        _game = game;
    }

    public bool IsGameOver => _game.IsGameOver;

    public int Turns => _game.Turns;

    public GamePiece CurrentTurn => _game.CurrentTurn;

    public GamePiece[,] Board {
        get => _game.Board; 
        init => throw new Exception(); 
    }



    public event EventHandler<WinnerEventArgs> OnWin {
        add {
            _game.OnWin += value;
        }

        remove {
            _game.OnWin -= value;
        }
    }





    /// <summary>
    /// Used for unit testing
    /// </summary>
    /// <param name="tiles"></param>
    internal void SetTiles(IEnumerable<(int row, int column, GamePiece piece)> tiles, bool ignoreChecks = false)
    {
        foreach(var t in tiles)
        {

            if(ignoreChecks)
            {
                Game g = _game as Game;

                g.SetTileWithoutChecks(t.row, t.column, t.piece);
            }else
            SetTile(t.row, t.column, t.piece);
        }
    }


    public bool SetTile(int row, int column, GamePiece piece)
    {
        return _game.SetTile(row, column, piece);
    }

    public GamePiece GetTile(int row, int column)
    {
        return _game.GetTile(row, column);
    }

    public void PerformHotSeatPlay(int row, int column)
    {
        _game.PerformHotSeatPlay(row, column);
    }

    public bool IsOccupied(int row, int column)
    {
        return _game.IsOccupied(row, column);
    }

    public bool IsWithinBounds(int row, int col)
    {
        return _game.IsWithinBounds(row, col);
    }

    public void ResetBoard()
    {
        _game.ResetBoard();
    }
}
