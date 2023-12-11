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
    public static int blackCount;
    public static int whiteCount;

    static Board()
    {
        Elements = new int[64];
        blackCount = 9; whiteCount = 9;
        InitBoard();
    }

    //Initial positions for black & white pieces
    private static void InitBoard()
    {
        // Init None Pieces
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Elements[r + c * 8] = Piece.None;
            }
        }

        // Add White
        for (int c = 0; c < 8; c++)
        {
            Elements[0 + c * 8] = Piece.White | Piece.Pawn;
        }
        Elements[1 + 3 * 8] = Piece.White | Piece.King; // White King piece

        // Add Black
        for (int c = 0; c < 8; c++)
        {
            Elements[7 + c * 8] = Piece.Black | Piece.Pawn;
        }
        Elements[6 + 4 * 8] = Piece.Black | Piece.King; // Black King piece

    }

    public static void RemovePiece(Vector2Int pos)
    {
        if (GetColorAtPos(pos.x, pos.y) == Piece.White) whiteCount--;
        else blackCount--;
        Elements[pos.y + 8 * pos.x] = Piece.None;
    }
    
    public static void RemovePiece(int x, int y)
    {
        if (GetColorAtPos(x, y) == Piece.White) whiteCount--;
        else blackCount--;
        Elements[y + 8 * x] = Piece.None;
    }


    public static int GetPieceAtPos(Vector2Int pos)
    {
        if (pos.x > 7 || pos.y > 7 || pos.x < 0 || pos.y < 0) return 0;
        return Elements[pos.y + 8 * pos.x];
    }

    public static int GetPieceAtPos(int x, int y)
    {
        if (x > 7 || y > 7 || x < 0 || y < 0) return 0;
        return Elements[y + 8 * x];
    }


    public static Vector2Int GetPosAtBoard(int i)
    {
        return new Vector2Int(i / 8, i % 8);
    }

    public static int GetColorAtPos(Vector2Int pos)
    {
        if (pos.x > 7 || pos.y > 7 || pos.x < 0 || pos.y < 0) return 0;
        return Elements[pos.y + pos.x * 8] & 12;
    }

    public static int GetColorAtPos(int x, int y)
    {
        if (x > 7 || y > 7 || x < 0 || y < 0) return 0;
        return Elements[y + x * 8] & 12;
    }

}
