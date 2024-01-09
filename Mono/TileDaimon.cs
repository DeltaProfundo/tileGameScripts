using System.Collections.Generic;
using UnityEngine;

public class TileDaimon : MonoBehaviour
{
    [SerializeField] private Tile[,] _tiles;
    public Tile[,] Tiles { get { return _tiles; } }
    [SerializeField] private Vector2Int[] _neighbouringDirections;

    public void GenerateTiles(Vector2Int gridSize, TileType[] defaultTileTypes)
    {
        _tiles = new Tile[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                TileType tileType = defaultTileTypes[Random.Range(0, defaultTileTypes.Length)];
                _tiles[x, y] = new Tile(gridPos, tileType);
            }
        }
    }    

    public void Tick()
    {
        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                Tile selTile = _tiles[x, y];
                selTile.Tick();
            }
        }
    }

    public Tile[] Neighbours(Tile tile)
    {
        Vector2Int gridPos = tile.GridPos;
        List<Tile> output = new List<Tile>();
        for (int i = 0; i < _neighbouringDirections.Length; i++)
        {
            Vector2Int selGridPos = gridPos + _neighbouringDirections[i];
            if (selGridPos.x < 0 || selGridPos.y < 0) { continue; }
            if (selGridPos.x > _tiles.GetLength(0) || selGridPos.y > _tiles.GetLength(1)) { continue; }
            output.Add(_tiles[selGridPos.x, selGridPos.y]);
        }
        return output.ToArray();
    }

}
