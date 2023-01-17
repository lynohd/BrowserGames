using System.Collections.Generic;

namespace BrowserGames.TicTacToe.Tests;

public class TicTacToeTests
{
    //using a proxy of game to make testing easier
    public GameProxy game;
    
    public TicTacToeTests()
    {
        game = new GameProxy(new Game());
    }


    [Fact]
    public void GAME_BOARD_IS_EMPTY()
    {
        Assert.True(AllTilesAreEmpty);
    }

    [Fact]
    public void GAME_BOARD_IS_NOT_EMPTY()
    {
        game.SetTile(0, 0, GamePiece.O);
        Assert.False(AllTilesAreEmpty);
    }

    [Fact]
    public void GAME_BOARD_GETS_RESET_ON_GAME_END()
    {

        for(int row = game.Board.GetLowerBound(0); row <= game.Board.GetUpperBound(0); row++)
            for(int col = game.Board.GetLowerBound(0); col <= game.Board.GetUpperBound(0); col++)
                game.PerformPlay(row,col);


        Assert.True(AllTilesAreEmpty);
    }

    [Fact]
    public void GAME_THROWS_WHEN_PLACING_TILE_OUT_OF_BOUNDS()
    {
        Assert.Throws<IndexOutOfRangeException>(() => game.SetTile(10, 10, GamePiece.X));
    }

    [Fact]
    public void GAME_TILE_IS_OCCUPIED()
    {
        game.SetTile(0,0,GamePiece.X);
        Assert.False(game.SetTile(0,0, GamePiece.O));
    }

    [Fact]
    public void CORRECT_TILE_AT_ROW_AND_COLUMN()
    {
        game.Board[2,2] = GamePiece.O;
        Assert.True(game.GetTile(2,2) == GamePiece.O);
    }

    [Fact]
    public void ALL_TILES_ARE_Os()
    {
        List<(int, int, GamePiece)> list = new List<(int, int, GamePiece)>() 
        {
            (0, 0, GamePiece.O),
            (0, 1, GamePiece.O),
            (0, 2, GamePiece.O),
            (1, 0, GamePiece.O),
            (1, 1, GamePiece.O),
            (1, 2, GamePiece.O),
            (2, 0, GamePiece.O),
            (2, 1, GamePiece.O),
            (2, 2, GamePiece.O),
        };



        game.SetTiles(list);

        Assert.True(AllTilesAreOs);

    }


    [Fact]
    public void ALL_TILES_ARE_Xs()
    {
        List<(int, int, GamePiece)> list = new List<(int, int, GamePiece)>()
        {
            (0, 0, GamePiece.X),
            (0, 1, GamePiece.X),
            (0, 2, GamePiece.X),
            (1, 0, GamePiece.X),
            (1, 1, GamePiece.X),
            (1, 2, GamePiece.X),
            (2, 0, GamePiece.X),
            (2, 1, GamePiece.X),
            (2, 2, GamePiece.X),
        };

        game.SetTiles(list);

        Assert.True(AllTilesAreXs);

    }

    public bool AllTilesAreXs => game.Board.OfType<GamePiece>().ToList().All(x => x == GamePiece.X);
    public bool AllTilesAreOs => game.Board.OfType<GamePiece>().ToList().All(x => x == GamePiece.O);
    public bool AllTilesAreEmpty => game.Board.OfType<GamePiece>().ToList().All(x => x == GamePiece.Empty);
}