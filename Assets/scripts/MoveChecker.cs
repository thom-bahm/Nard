using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChecker : MonoBehaviour
{
    // if the sourcePos is on the same row XOR col as destPos it is legal
    public static bool CheckLegalMove(Vector2Int sourcePos, Vector2Int destPos)
    {
        if (sourcePos == destPos) return false; //can't move to same spot

        if ( (Board.GetPieceAtPos(destPos) == Piece.None) &&
            ( (sourcePos.x == destPos.x && sourcePos.y != destPos.y)
            || (sourcePos.y == destPos.y && sourcePos.x != destPos.x)))
        {
            return OstructionFree(sourcePos, destPos);
        }
        // did not meet conditions for legal move, return false
        return false;
    }

    // This only gets called if the move is moving along a single rank OR file
    private static bool OstructionFree(Vector2Int sourcePos, Vector2Int destPos)
    {
        int fromPos = int.MaxValue;
        int toPos = int.MaxValue;
        bool checkAlongFile = false;
        // source and destPos are in same column, diff rows; calc dist between rows
        if (sourcePos.x == destPos.x)
        {
            fromPos = Mathf.Min(destPos.y, sourcePos.y);
            toPos = Mathf.Max(destPos.y, sourcePos.y);
            checkAlongFile = true;
        }
        // same rows, diff columns, calc dist between columns
        else if (sourcePos.y == destPos.y)
        {
            fromPos = Mathf.Min(destPos.x, sourcePos.x);
            toPos = Mathf.Max(destPos.x, sourcePos.x);
            checkAlongFile = false;
        }

        // check if there is a piece between source and destination
        while (++fromPos <= --toPos)
        {
            if (checkAlongFile)
            {
                int file = sourcePos.x;
                if (Board.GetPieceAtPos(file, fromPos) != Piece.None ||
                    Board.GetPieceAtPos(file, toPos) != Piece.None) return false;
            } else
            {
                int rank = sourcePos.y;
                if (Board.GetPieceAtPos(fromPos, rank) != Piece.None ||
                    Board.GetPieceAtPos(toPos, rank) != Piece.None) return false;
            }
        }

        return true;

    }
}