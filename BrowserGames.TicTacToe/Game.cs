using System;
using System.Text;
using System.Xml.Linq;

namespace BrowserGames.TicTacToe;


public class Game : IGame
{
    //Should always be 3x3 for tictactoe
    const int COLUMNS = 3;
    const int ROWS = 3;

    public const GamePiece Player = GamePiece.O;
    public const GamePiece Enemy = GamePiece.X;
    public const GamePiece Empty = GamePiece.Empty;



    public event Action? OnRestart;
    public event EventHandler<WinnerEventArgs> OnWin;
    public event EventHandler<PiecePlacedEventArgs> OnTurn;


    /// <summary>
    /// Game should be over when all the tiles are filled
    /// </summary>
    public bool IsGameOver => Turns >= (COLUMNS * ROWS) || _gameOver;
    bool _gameOver = false;

    /// <summary>
    /// Game should be over at 9 turns maximum
    /// </summary>
    public int Turns { get; internal set; }

    /// <summary>
    /// The current turn
    /// </summary>
    public GamePiece CurrentTurn { get; private set; }

    /// <summary>
    /// [Row,Column]
    /// </summary>
    public GamePiece[,] Board { get; init;}

    public GamePiece this[int row, int column] {
        get => Board[row, column];
        set => Board[row, column] = value;
    }

    public Game()
    {
        this.Board = new GamePiece[ROWS, COLUMNS];
        this.CurrentTurn = Player;
    }


    public bool IsWithinBounds(int row, int col) => ((col < COLUMNS && row < ROWS) && (col >= 0 && row >= 0));
    
    public bool IsOccupied(int row, int column) => this[row, column] != GamePiece.Empty;


    public GamePiece GetTile(int row, int column) => this[row, column];




    public void ResetBoard()
    {
        for(int row = Board.GetLowerBound(0); row <= Board.GetUpperBound(0); row++)
        {
            for(int col = Board.GetLowerBound(1); col <= Board.GetUpperBound(1); col++)
            {
                SetTileWithoutChecks(row, col, Empty);
            }
        }

        Turns = 0;
        CurrentTurn = Player;
        _gameOver= false;
        OnRestart?.Invoke();
    }


    /// <summary>
    /// Used for resetting the board.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="piece"></param>
    internal void SetTileWithoutChecks(int row, int column, GamePiece piece)
    {
        this[row, column] = piece;
        if(CheckWin(row, column, piece))
            OnWin?.Invoke(this, new(piece));
    }

    /// <summary>
    /// Used for placing tiles with alternating turns
    /// (Hot seat mode)
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void PerformHotSeatPlay(int row, int column)
    {
        if(this.CurrentTurn == Player)
        {
            if(SetTile(row, column, Player))
            {
                this.CurrentTurn = Enemy;
            }
        }
        else
        {
            if(SetTile(row, column, Enemy))
            {
                this.CurrentTurn = Player;
            }
        }

    }

    public bool CheckWin(int row, int col, GamePiece player)
    {
        List<(int, int)[]> conditions = new List<(int, int)[]>
        {
            new[] { (row, 0), (row, 1), (row, 2) },
            new[] {(0, col), (1, col), (2, col)},
            new[] {(0,0), (1,1), (2,2)},
            new[] {(0,2), (1,1), (2,0)}
        };

        //nested method
        bool __HasWin((int, int)[] tiles, GamePiece player)
        {
            foreach((int row, int col) in tiles)
            {
                if(Board[row, col] != player)
                {
                    return false;
                }
            }
            return true;
        }

        return conditions.Any(tiles => __HasWin(tiles, player));
    }


    public void EndGame()
    {
        ResetBoard();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns true if successfull, false if not</returns>
    /// <param name="x">Row</param>
    /// <param name="y">Column</param>
    /// <param name="player"></param>
    /// <exception cref="IndexOutOfRangeException">Will be thrown if you're trying to place a piece out of the board bounds.</exception>
    public bool SetTile(int row, int column, GamePiece player)
    {
        //This is in desperate need of cleaning up
        if(IsGameOver)
        { 
            return false; 
        }

        if(IsWithinBounds(row, column))
        {
            if(IsOccupied(row, column))
            {
                Console.WriteLine("Tile is occupied");
                return false;
            }

            this[row, column] = player;


            if(CheckWin(row, column, player))
            {
                _gameOver = true;
                OnWin?.Invoke(this, new(player));
                return false;
            }
            OnTurn?.Invoke(this, new (row, column,player));

            Turns++;
            return true;
        }

        throw new IndexOutOfRangeException($"your selection row: {row} col: {column}is out of bound something went wrong.");
    }

    public string ToPrettyString()
    {
        var sb = new StringBuilder();
        var b = Board;

        sb.AppendLine($"|{b[0, 0]}|{b[0, 1]}|{b[0, 2]}|");
        sb.AppendLine($"|{b[1, 0]}|{b[1, 1]}|{b[1, 2]}|");
        sb.AppendLine($"|{b[2, 0]}|{b[2, 1]}|{b[2, 2]}|");

        sb.Replace("Empty", " ");
        return sb.ToString();
    }

}
