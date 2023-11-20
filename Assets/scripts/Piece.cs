using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representation of 'Piece' in binary is that the first two
/// bits will be it's color, and the last two bits the type
/// </summary>
public static class Piece
{
    public const short None = 0; //0b_0000
    public const short King = 1; //0b_0001
    public const short Pawn = 2; //0b_0010

    public const short White = 4; //0b_0100
    public const short Black = 8; //0b_1000


    // compares piece a and b's color
    public static bool SameColor(int a, int b)
    {
        return GetColor(a) == GetColor(b);
    }

    public static int GetColor(int piece)
    {
        return piece & 0b1100;
    }
}
