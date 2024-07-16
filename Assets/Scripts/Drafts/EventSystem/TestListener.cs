using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestListener : MonoBehaviour
{
    private UnityAction onTest;

    private void Awake()
    {
        onTest = new UnityAction(OnTest);
        EventManager.StartListening("Test", onTest);
    }

    private void OnTest()
    {
        print("yoooooooo");
    }
}
