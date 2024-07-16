using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollinationManager : MonoBehaviour
{
    public float pollinationTickTime;
    private float pollinationTimer;
    private void Awake()
    {
        pollinationTimer = pollinationTickTime;
    }

    private void FixedUpdate()
    {
        pollinationTimer -= Time.deltaTime;
        
        if (pollinationTimer <= 0)
        {
            pollinationTimer = pollinationTickTime;
            EventManager.TriggerEvent("POLLINATION_TICK");
        }
    }
}
