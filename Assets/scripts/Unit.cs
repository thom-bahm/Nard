using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private Color _whiteColor, _blackColor;
    [SerializeField] protected SpriteRenderer _renderer;

    public int unit;
    private Vector2Int _position;

    public bool isActive;

    // Initializes the unit by assigning a color to the renderer based on
    // the pieces color
    public virtual void InitUnit(int unit, int x, int y)
    {
        this.unit = unit;
        isActive = false;
        _position = new Vector2Int(x, y);
        _renderer.color = (GetUnitColor() == Piece.White) ? _whiteColor : _blackColor;
    }

    // Move's the unit's location on the array representation of the board
    // as well as the 
    public void MoveUnit(Vector2Int newPos)
    {
        //Debug.Log("Old position: " + _position + ", New position: " + newPos);

        //Make the current space on board empty
        UnitManager.units[_position] = null;
        int index = _position.y + (_position.x * 8);
        Board.Elements[index] = Piece.None;

        //update position and space on board
        _position = newPos;
        int newIndex = _position.y + _position.x * 8;
        Board.Elements[newIndex] = this.unit;

        this.transform.position = new Vector3(_position.x + 0.5f, _position.y + 0.5f, -1);
        UnitManager.units[_position] = this;
    }

    public void ToggleRenderer()
    {
        this._renderer.enabled = !this._renderer.enabled;
        if (!this._renderer.enabled) _renderer.color = Color.red;
    }

    // Will return int representing color (4 = white, 8 = black)
    public int GetUnitColor()
    {
        return unit & 12;
    }

    // Will return int representing type (0 = none, 1 = king, 2 = pawn)
    public int GetUnitType()
    {
        return unit & 3;
    }

    public Vector2Int GetPosition()
    {
        return _position;
    }

    // Will return true if the color of this unit is the same
    // as the current Game State indicates
    public bool GameStateAgrees()
    {
        if (GameManager.Instance.State == GameState.WhiteTurn && this.GetUnitColor() == Piece.White
            || GameManager.Instance.State == GameState.BlackTurn && this.GetUnitColor() == Piece.Black)
            return true;
        else
            return false;
    }

    override public string ToString()
    {
        string res;
        res = "Pos: " + this._position.ToString() + ", unit type: " + unit;
        return res;
    }
}