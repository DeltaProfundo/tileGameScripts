using UnityEngine;

[System.Serializable]
public class Tile
{
    [SerializeField] private Vector2Int _gridPos;
    public Vector2Int GridPos { get { return _gridPos; } }
    [SerializeField] private TileType _tileType;
    public TileType TileType { get { return _tileType;} }

    public Tile(Vector2Int gridPos, TileType tileType)
    {
        _gridPos = gridPos;
        _tileType = tileType;
    }

    public void DiscreteUpdate()
    {
        
    }
    public void DiscreteUpdate(TileType tileType)
    {
        if (tileType == null)
        {
            Debug.LogWarning("Tile : DiscreteUpdate : TileType is null");
            return;
        }
        _tileType = tileType;
        DiscreteUpdate();
    }
    public void Tick()
    {
        Context context = new Context();
        context.Tiles = new Tile[] { this };
        Parsing.ExecuteInstruction(_tileType.TickInstruction, context);
    }

}
