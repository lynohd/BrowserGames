using BrowserGames.TicTacToe;
//a very messy implementation of tictactoe in a console application

var _game = new Game();
var _cpu = new CpuPlayer(_game);

_game.OnWin += (obj, winner) =>
{
    Console.Clear();
    PrintBoard();
    Console.WriteLine("Player: " + winner.Player + " won");
    Console.ReadLine();
};

start:
Console.WriteLine("Please enter a tile: ");
var input = Console.ReadLine();

if(input == "print")
{
    PrintBoard();
    goto start;
}


var split = input.Split(',');

if(split.Length <= 1)
{
    Console.WriteLine("Invalid input..");
    goto start;
}

var row = int.Parse(split[0]);
var col = int.Parse(split[1]);

if(!_game.SetTile(row, col, Game.Player))
{
    goto start;
}

PrintBoard();
goto start;

void PrintBoard()
{
    Console.Clear();
    for(int row = _game.Board.GetLowerBound(0); row <= _game.Board.GetUpperBound(0); row++)
    {
        for(int col = _game.Board.GetLowerBound(1); col <= _game.Board.GetUpperBound(1); col++)
        {
            Console.WriteLine($"[{row}, {col}] = {_game[row,col]}");
        }
    }
}