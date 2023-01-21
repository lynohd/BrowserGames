using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGames.TicTacToe.Extensions;
public static class Extensions
{
    public static IEnumerable<T> GetColumn<T>(this T[,] arr, int column)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            yield return arr[i, column];
        }
    }

    public static IEnumerable<IEnumerable<T>> GetColumns<T>(this T[,] arr)
    {
        for (int i = 0; i < arr.GetLength(1); i++)
        {
            yield return arr.GetColumn(i);
        }
    }
    public static IEnumerable<T> GetRow<T>(this T[,] arr, int row)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            yield return arr[row, i];
        }
    }

    public static IEnumerable<IEnumerable<T>> GetRows<T>(this T[,] arr)
    {
        for (int i = 0; i < arr.GetLength(1); i++)
        {
            yield return arr.GetRow(i);
        }
    }
}
