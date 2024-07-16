using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Note that this is for any ground tiles - probably should edit that at some point

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public string title;
    public TileBase[] tiles;

    public bool isPlantable;
    
}
