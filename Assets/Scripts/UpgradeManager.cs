using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"More than one UpgradeManager exist in the scene! Destroying {this.gameObject.name}");
            Destroy(this.gameObject);
        }

        Instance = this;
    }

    public StatHolder GetSaveData()
    {
        // TODO: make sure that theres a way to get save here!!
        // Needs upgrades ID + upgrade level

        return new StatHolder();
    }

    
}