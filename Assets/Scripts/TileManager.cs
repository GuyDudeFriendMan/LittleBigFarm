using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    //using Vector3 here so that we can maybe assign different properties to different z levels?
    private Dictionary<Vector3Int, float> tiles = new Dictionary<Vector3Int, float>();

    
}
