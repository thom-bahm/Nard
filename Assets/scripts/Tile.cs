using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thanks to this video from Tarodev on youtube for setting up Tiles: https://www.youtube.com/watch?v=kkAjpQAM-jE

/// <summary>
/// Tile to be rendered on screen
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _whiteColor, _blackColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;


    public void initTile(bool white)
    {
        _renderer.color = white ? _whiteColor : _blackColor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int intCoords = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        bool moveMade = this.TryMove(intCoords); //probably change this to a "can make move" function
        if (moveMade)
        {
            BoardStateChecker.CheckBoardState(UnitManager.activeUnit);
            UnitManager.activeUnit = null;
        }
    }

    // Move this code to the UnitManager probably ?
    // For now it's ok, keeps it simple enough - refactor later
    private bool TryMove(Vector2Int intCoords)
    {
        // If there is a selected unit, update it's position and 'de-activate' it
        bool moveLegal = false;
        Unit activeRef = UnitManager.activeUnit;
        if (activeRef)
        {
            // check if it is the correct players turn & that it is a legal move
            if (activeRef.GameStateAgrees() && MoveChecker.CheckLegalMove(activeRef.GetPosition(), intCoords))
            {
                moveLegal = true;
                activeRef.MoveUnit(intCoords);

                // WhiteTurn stored as 2, BlackTurn stored as -2
                // so change the game state by flipping it's sign
                int flippedState = -(int)GameManager.Instance.State;
                GameManager.Instance.ChangeState((GameState) flippedState);
                //GameManager.Instance.State = (GameState)flippedState;
            }
            // a unit has been 'picked up' but they pressed on a unit of 
            else if (Piece.SameColor(Board.GetPieceAtPos(intCoords), activeRef.unit))
            {
                UnitManager.units.TryGetValue(intCoords, out Unit newActiveUnit);
                if (newActiveUnit) UnitManager.activeUnit = newActiveUnit;
            }
        }
        // if no active unit and a unit exists at position selected,
        // assign the active unit to the piece on the tile just pressed
        else
        {
            UnitManager.units.TryGetValue(intCoords, out Unit newActiveUnit);
            if (newActiveUnit)
            {
                if (newActiveUnit.GameStateAgrees())
                    UnitManager.activeUnit = newActiveUnit;
            }
        }
        return moveLegal;
    }
}
