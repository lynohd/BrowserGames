using BrowserGames.TicTacToe;

using System.Text;
using System.Xml.Linq;
//a very messy implementation of tictactoe in a console application

public class Program
{
    static Game _game = new Game();
    static CpuPlayer _cpu = new CpuPlayer(_game);


    static void Main(string[] args)
    {
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

        PromptInput();

    }
    static void PromptInput()
    {
        Console.WriteLine("Please enter a tile: ");
        var input = Console.ReadLine();

        if(input == "print")
        {
            PrintBoard();
            PromptInput();
        }
        var split = input.Split(',');

        if(split.Length <= 1)
        {
            Console.WriteLine("Invalid input..");
            PromptInput();
        }

        var row = int.Parse(split[0]);
        var col = int.Parse(split[1]);

        if(!_game.SetTile(row, col, Game.Player))
        {
            PromptInput();
        }

        PrintBoard();
        PromptInput();
    }


    static void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine(_game.ToPrettyString());
    }
}

