using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to keep track of Array rep of Game Board
/// </summary>
public static class Board
{
    /// <summary>
    /// Elements are stored 1-Dimensionally, such that the row is accessed
    /// by dividing the index by 8 (integer division discards remainder), and
    /// the column is accesed by the remainder of index divided by 8 (aka index mod 8)
    /// </summary>
    public static int[] Elements;
    public static bool activePieceState;

    static Board()
    {
        Elements = new int[64];
        activePieceState = false;
        InitBoard();
    }

    //Initial positions for black & white pieces
    private static void InitBoard()
    {
        // None Pieces
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Elements[r + c * 8] = Piece.None;
            }
        }

        //White
        for (int c = 0; c < 8; c++)
        {
            Elements[0 + c * 8] = Piece.White | Piece.Pawn;
        }
        Elements[1 + 3 * 8] = Piece.White | Piece.King; // White King piece

        //Black
        for (int c = 0; c < 8; c++)
        {
            Elements[7 + c * 8] = Piece.Black | Piece.Pawn;
        }
        Elements[6 + 4 * 8] = Piece.Black | Piece.King; // Black King piece

    }

    public static int GetPieceAtPos(Vector2Int pos)
    {
        if (pos.x > 7 || pos.y > 7 || pos.x < 0 || pos.y < 0) return -1;
        return Elements[pos.y + 8 * pos.x];
    }

    public static int GetPieceAtPos(int x, int y)
    {
        if (x > 7 || y > 7 || x < 0 || y < 0) return -1;
        return Elements[y + 8 * x];
    }


    public static Vector2Int GetPosAtBoard(int i)
    {
        return new Vector2Int(i / 8, i % 8);
    }

    public static int GetColorAtPos(Vector2Int pos)
    {
        return Elements[pos.y + pos.x * 8] & Piece.Black;
    }
}
