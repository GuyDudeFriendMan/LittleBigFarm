using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pollinator : MonoBehaviour
{
    public Vector3Int position;
    public MapManager mapManager;
    public int radius;
    public float pollinationAmount;
    public float fallOff;

    private UnityAction onPollinationTick;

    private void Awake()
    {
        transform.position = position;
        onPollinationTick = new UnityAction(OnPollinationTick);
        EventManager.StartListening("POLLINATION_TICK", onPollinationTick);
    }

    private void OnPollinationTick()
    {
        mapManager.SpreadBeePollination(position, radius, pollinationAmount, fallOff);
    }
}
