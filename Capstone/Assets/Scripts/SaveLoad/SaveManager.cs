using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    PlayerStat player;

    public void Start()
    {
        
    }

    public void SaveData() 
    {
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.GetComponent<PlayerStat>();
        SaveSystem.SavePlayer(player);
        SaveSystem.SaveWorld();
    }

    public void LoadData() 
    {
        SaveSystem.LoadPlayer();
    }
}
