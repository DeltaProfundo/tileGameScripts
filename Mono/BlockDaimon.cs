using System.Collections.Generic;
using UnityEngine;

public class BlockDaimon : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks = new List<Block>();
    public List<Block> Blocks { get { return _blocks; } }

    public void GenerateBlocks(Tile[,] tiles)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                Tile selTile = tiles[x, y];
                Vector3 worldPos = new Vector3(x, 0, y);
                GameObject blockObj = Instantiate(Ubik.Instance.BlockPrefab, worldPos, Quaternion.identity, transform);
                Block block = blockObj.GetComponent<Block>();
                block.Setup(selTile);
                _blocks.Add(block);
            }
        }
    }

    public Block Block(Vector2Int gridPos)
    {
        Block output = null;
        for (int i = 0; i< _blocks.Count; i++)
        {
            Block selBlock = _blocks[i];
            if (selBlock.Tile.GridPos == gridPos)
            {
                return selBlock;
            }
        }
        return output;
    }

}
