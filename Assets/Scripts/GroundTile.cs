using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Okay so, I think the way this is gonna work is that the ScriptableObject TileData will be the base, and this will contain all the information for the actual game
 struct GroundTile
{
    
    public string title;
    public bool isPlantable;
    public float moistureLevel;
    public float beePollinationLevel;

    public GroundTile(TileData tileData)
    {
        title = tileData.title;
        this.isPlantable = tileData.isPlantable;
        moistureLevel = 0f;
        beePollinationLevel = 0f;
    }

    public void SetBeePollinationLevel(float beePollinationLevel)
    {
        this.beePollinationLevel = beePollinationLevel;
    }
}
