using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public static Inventory Instance;

    public bool hasSprayCan = false;
    public bool hasMedicalKit = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        // Set instance of items in inventory to true/false by pressing or interacting
    }
}
