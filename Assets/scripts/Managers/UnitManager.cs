using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages Units (Pawns, Kings); Spawns pieces, Keeps track of
/// changing data, etc.
/// </summary>

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private King _kingPrefab;

    public static Unit activeUnit;
    public static Dictionary<Vector2Int, Unit> units;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Instantiates 'Units' on the board according to their location,
    /// color, and type, as represented in Board.Elements array
    /// </summary>
    public void SpawnPieces()
    {
        units = new Dictionary<Vector2Int, Unit>();

        for (int i = 0; i < Board.Elements.Length; i++)
        {
            // get x and y position
            int x = i / 8; int y = i % 8;

            Unit spawnedUnit = null;
            // If if the piece is King or Pawn, create the corresp. prefab on board.
            if ((Board.Elements[i] & 3) == Piece.Pawn)
            {
                spawnedUnit = Instantiate(_pawnPrefab, new Vector3(x + 0.5f, y + 0.5f, -1), Quaternion.identity);
            } else if ( (Board.Elements[i] & 3) == Piece.King)
            {
                spawnedUnit = Instantiate(_kingPrefab, new Vector3(x + 0.5f, y + 0.5f, -1), Quaternion.identity);
            }

            // If the type was King or Pawn (not NONE),
            // call initUnit which colors the piece
            if (spawnedUnit)
            {
                spawnedUnit.name = $"{spawnedUnit.GetType()} {x}, {y}";
                spawnedUnit.InitUnit(Board.Elements[i], x, y);
                units[new Vector2Int(x, y)] = spawnedUnit;
            }
        }

        Debug.Log("Spawn Pieces called");
        GameManager.Instance.ChangeState(GameState.WhiteTurn);
    }

    public void RemoveUnit(Vector2Int pos)
    {
        units[pos].ToggleRenderer();
        units[pos] = null;
    }

    public void RemoveUnit(int x, int y)
    {
        Vector2Int pos = new Vector2Int(x, y);
        //Destroy(units[pos]);
        units[pos].ToggleRenderer();
        units[pos] = null;
    }

}
