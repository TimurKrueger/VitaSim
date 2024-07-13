using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, Interactable
{
    public void Interact(bool isMeleeInteraction = true)
    {
        Debug.Log("Test");    
    }
}
