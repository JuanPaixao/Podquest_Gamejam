using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomizeTile : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    private Sprite[] groundSprites;

    public int numberOfTiles;
    public TileBase[] allTiles;
    public TileData tileData;

    private void Start()
    {
        RandomizeGround();
    }
    public void RandomizeGround()
    {
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                numberOfTiles++;
                int randomTile = Random.Range(0, 4);
                tilemap.SetTile(position, tiles[randomTile]);
            }
        }
    }
}