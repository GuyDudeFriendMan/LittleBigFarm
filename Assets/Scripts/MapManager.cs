using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private List<TileData> tileDatas;

    [SerializeField]
    private Tilemap cropMap;

    [SerializeField]
    private TileData defaultTile;

    private Dictionary<TileBase, TileData> dataFromTiles;
    private Dictionary<Vector3Int, GroundTile> groundTileDictionary;
    private Dictionary<Vector3Int, CropObject> cropData;


    public Crop toAdd;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        groundTileDictionary = new Dictionary<Vector3Int, GroundTile>();
        cropData = new Dictionary<Vector3Int, CropObject>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }

        foreach (var tile in tilemap.GetTilesBlock(tilemap.cellBounds))
        {
            if (tile != null)
            {
                if (!dataFromTiles.ContainsKey(tile))
                {
                    dataFromTiles.Add(tile, defaultTile);
                }

            }
        }

        //This uses a lot of memory and might not be the best idea
        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.xMin; y < tilemap.cellBounds.xMax; y++)
            {
                if (tilemap.GetTile(tilemap.WorldToCell(new Vector3Int(x, y))))
                {
                    GroundTile tile = new GroundTile(dataFromTiles[tilemap.GetTile(tilemap.WorldToCell(new Vector3Int(x, y)))]);

                    groundTileDictionary.Add(new Vector3Int(x, y), tile);
                }
            }
        }
    }

    //This can probably be generalized
    public void SpreadBeePollination(Vector3Int position, int radius, float beePollinationAmount, float fallOff)
    {
        position = tilemap.WorldToCell(position);
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                if(distanceFromCenter <= radius)
                {
                    Vector3Int nextTilePosition = new Vector3Int(position.x + x, position.y + y);

                    if (groundTileDictionary.ContainsKey(nextTilePosition))
                    {
                        GroundTile nextTile = groundTileDictionary[nextTilePosition];

                        float pollination = nextTile.beePollinationLevel + beePollinationAmount - (distanceFromCenter * fallOff * beePollinationAmount);
                        if (pollination <= 0)
                        {
                            pollination = 0;
                        }

                        nextTile.SetBeePollinationLevel(pollination);
                        groundTileDictionary[nextTilePosition] = nextTile;

                        print(groundTileDictionary[nextTilePosition].beePollinationLevel);
                    }
                }

            }
        }

        groundTileDictionary[position].SetBeePollinationLevel(groundTileDictionary[position].beePollinationLevel + beePollinationAmount);
        
    }

    //I should really refactor everything, this script is doing a little too much
    public void PlantCrop(Vector3Int position, CropObject crop)
    {
        if (groundTileDictionary[position].isPlantable && groundTileDictionary[position].beePollinationLevel >= crop.crop.minBeePollination)
        {
            if(cropData.TryAdd(position, crop))
                cropMap.SetTile(position, crop.crop.tileBase);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            TileBase clickedTile = tilemap.GetTile(gridPosition);
            GroundTile clickedTileData = groundTileDictionary[gridPosition];

            print("At position " + gridPosition + " there is a " + clickedTileData.title + " with bee pollination level " + clickedTileData.beePollinationLevel);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            
            PlantCrop(gridPosition, new CropObject(toAdd));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            SpreadBeePollination(gridPosition, 5, 10, 0.2f);
        }
    }
}
