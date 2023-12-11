using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardStateChecker : MonoBehaviour
{
    // if we find any enemy piece along a rank or file
    // in between the newly moved piece and another friendly piece we can
    // take the enemy piece
    public static void CheckBoardState(Unit unit)
    {
        Debug.Log(unit.ToString());
        //Search for enemy piece
        int enemyColor = unit.GetUnitColor() == Piece.White ? Piece.Black : Piece.White;
        int capturable = enemyColor | Piece.Pawn;
        Vector2Int pos = unit.GetPosition();

        // Enemy pieces
        // if a piece immediately next to it is capturable (enemy color, pawn)
        // and there is a friendly piece on the surrounding side, capture it.

        // check for capturable piece at row above moved piece
        if (Board.GetPieceAtPos(pos.x, pos.y + 1) == capturable)
        {
            // friendly piece on surrounding side
            if (Board.GetColorAtPos(pos.x, pos.y + 2) == unit.GetUnitColor())
            {
                // capture piece
                UnitManager.Instance.RemoveUnit(pos.x, pos.y + 1);
                Board.RemovePiece(pos.x, pos.y + 1);
            }
        }
        // row beneath moved piece
        if (Board.GetPieceAtPos(pos.x, pos.y - 1) == capturable)
        {
            if (Board.GetColorAtPos(pos.x, pos.y - 2) == unit.GetUnitColor())
            {
                UnitManager.Instance.RemoveUnit(pos.x, pos.y - 1);
                Board.RemovePiece(pos.x, pos.y - 1);
            }
        }
        // column right of moved piece
        if (Board.GetPieceAtPos(pos.x + 1, pos.y) == capturable)
        {
            if (Board.GetColorAtPos(pos.x + 2, pos.y) == unit.GetUnitColor())
            {
                UnitManager.Instance.RemoveUnit(pos.x + 1, pos.y);
                Board.RemovePiece(pos.x + 1, pos.y);
            }
        }
        // column left of moved piece
        if (Board.GetPieceAtPos(pos.x - 1, pos.y) == capturable)
        {
            if (Board.GetColorAtPos(pos.x - 2, pos.y) == unit.GetUnitColor())
            {
                UnitManager.Instance.RemoveUnit(pos.x - 1, pos.y);
                Board.RemovePiece(pos.x - 1, pos.y);
            }
        }

        // Check if moved piece should be taken IFF it's a pawn
        if (unit.GetUnitType() == Piece.Pawn)
        {
            if (Board.GetColorAtPos(pos.x, pos.y + 1) == enemyColor
            && Board.GetColorAtPos(pos.x, pos.y - 1) == enemyColor)
            {
                UnitManager.Instance.RemoveUnit(pos);
                Board.RemovePiece(pos);
            }
            else if (Board.GetColorAtPos(pos.x - 1, pos.y) == enemyColor
            && Board.GetColorAtPos(pos.x + 1, pos.y) == enemyColor)
            {
                UnitManager.Instance.RemoveUnit(pos);
                Board.RemovePiece(pos);
            }
        }

    }

    private void CheckWin()
    {
        // if only the king is left, Game over.
        if (Board.blackCount == 1)
            GameManager.Instance.ChangeState(GameState.WhiteWin);
        else if (Board.whiteCount == 1)
            GameManager.Instance.ChangeState(GameState.BlackWin);
    }
}

