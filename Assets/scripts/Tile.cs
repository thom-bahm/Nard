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
        this.TryMove(intCoords); //probably change this to a "can make move" function
    }

    // Move this code to the UnitManager probably ?
    // Or have a script that is responsible for Movement, etc.
    private void TryMove(Vector2Int intCoords)
    {
        // If there is a selected unit, update it's position and 'de-activate' it
        Unit activeRef = UnitManager.activeUnit;
        if (activeRef)
        {
            // check if it is the correct players turn & that it is a legal move
            if (activeRef.GameStateAgrees() && MoveChecker.CheckLegalMove(activeRef.GetPosition(), intCoords))
            {
                activeRef.MoveUnit(intCoords);
                BoardStateChecker.CheckBoardState(activeRef);
                UnitManager.activeUnit = null;

                // WhiteTurn stored as 2, BlackTurn stored as -2
                // so change the game state by flipping it's sign
                int flippedState = -(int)GameManager.Instance.State;
                GameManager.Instance.State = (GameState)flippedState;
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

    }
}
