using BrowserGames.TicTacToe;

using System.Text;
//a very messy implementation of tictactoe in a console application



var _game = new Game();
var _cpu = new CpuPlayer(_game);
_game.OnWin += (obj, winner) =>
{
    PrintBoard();
    Console.WriteLine("Player: " + winner.Player + " won");
    Console.WriteLine("would you like to play again? Y/N");
    var again = Console.ReadKey(true).Key;

    if(again == ConsoleKey.Y)
    {
        _game.ResetBoard();
    }
    else
    {
        Environment.Exit(0);
    }
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
    Console.WriteLine(_game.ToPrettyString());
}