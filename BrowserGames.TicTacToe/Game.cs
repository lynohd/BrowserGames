namespace BrowserGames.TicTacToe;
public class Game : IGame
{
    //Should always be 3x3 for tictactoe
    const int COLUMNS = 3;
    const int ROWS = 3;

    public const GamePiece Player = GamePiece.O;
    public const GamePiece Enemy = GamePiece.X;
    public const GamePiece Empty = GamePiece.Empty;



    ///////////////////////////////////////////////////////////EVENTS///////////////////////////////////////////////////

    public event Action? OnRestart;
    public event Action<GamePiece>? OnWin;


    /// <summary>
    /// Game should be over when all the tiles are filled
    /// </summary>
    public bool IsGameOver => (Turns >= (COLUMNS * ROWS));

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
        OnRestart?.Invoke();
    }


    /// <summary>
    /// Used for resetting the board.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="piece"></param>
    private void SetTileWithoutChecks(int row, int column, GamePiece piece)
    {
        this[row, column] = piece;
    }

    /// <summary>
    /// Used for placing tiles with alternating turns
    /// (Hot seat mode)
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void PerformPlay(int row, int column)
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

        if(IsGameOver)
        {
            EndGame();
        }
        
    }
    /// <summary>
    /// Perform win checks here
    /// </summary>
    public void EndGame()
    {
        OnWin?.Invoke(Player);

        ResetBoard();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns true if successfull, false if not</returns>
    /// <param name="x">Row</param>
    /// <param name="y">Column</param>
    /// <param name="piece"></param>
    /// <exception cref="IndexOutOfRangeException">Will be thrown if you're trying to place a piece out of the board bounds.</exception>
    public bool SetTile(int row, int column, GamePiece piece)
    {
        if(IsWithinBounds(row, column))
        {
            if(IsOccupied(row, column))
            {
                Console.WriteLine("Tile is occupied");
                return false;
            }

            this[row, column] = piece;
            Turns++;
            return true;
        }

        Console.WriteLine("your selection is out of bound something went wrong.");
        throw new IndexOutOfRangeException("your selection is out of bound something went wrong.");
    }

}
