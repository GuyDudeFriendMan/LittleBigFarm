using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
* Contains all static information for the crops.
* Everything that could change at runtime is handled by another script.
*/

[CreateAssetMenu]
public class Crop : ScriptableObject
{

    public string title;
    public int growthPeriod;

    public TileBase tileBase;

    [Header("Pollination")]
    [Header("Bees")]
    public float minBeePollination;
    public float maxBeePollination;
    public bool isBeePollinated;

    [Header("Birds")]
    public float minBirdPollination;
    public float maxBirdPollination;
    public bool isBirdPollinated;

    [Header("Bats")]
    public float minBatPollination;
    public float maxBatPollination;
    public bool isBatPollinated;

    [Header("Butterflies")]
    public float minButterflyPollination;
    public float maxButterflyPollination;
    public bool isButterflyPollinated;

    [Header("LilGuys")]
    public float minLilGuyPollination;
    public float maxLilGuyPollination;
    public bool isLilGuyPollinated;



    public float getMinPollination(PollinatorType pollinatorType)
    {
        switch (pollinatorType)
        {
            case PollinatorType.Bees:
                return minBeePollination;
            case PollinatorType.Birds:
                return minBirdPollination;
            case PollinatorType.Bats:
                return minBatPollination;
            case PollinatorType.Butterflies:
                return minButterflyPollination;
            case PollinatorType.LilGuys:
                return minLilGuyPollination;
            default:
                return 0f;
        }
    }
}
