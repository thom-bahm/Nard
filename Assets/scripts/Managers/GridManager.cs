using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thanks to this video from Tarodev on youtube: https://www.youtube.com/watch?v=kkAjpQAM-jE

/// <summary>
/// Responsible for drawing the Grid to the screen
/// </summary>
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width = 8, _height = 8;
    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2Int, Tile> _tiles;

    private void Awake()
    {
        Instance = this;
    }

    // Chess board - White When rows even, columns odd OR rows odd, columns even
    // Black when rows odd, columns odd OR rows even, columns even
    // Rank corresponds to the column, File corresp to row
    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Tile tile = Instantiate(_tilePrefab, new Vector3(i + 0.5f, j + 0.5f), Quaternion.identity);
                tile.name = $"tile {i}, {j}";

                // "White" tiles 
                var white = (i % 2 == 0 && j % 2 != 0) || ((i % 2 != 0) && j % 2 == 0);
                tile.initTile(white);

                _tiles[new Vector2Int(i, j)] = tile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2, (float)_height / 2, -10);


        GameManager.Instance.ChangeState(GameState.SpawnPieces);
    }

    public Tile GetTileAtPos(Vector2Int pos)
    {
        if (_tiles.TryGetValue(pos, out Tile tile))
            return tile;
        else
            return null;
    }
}
