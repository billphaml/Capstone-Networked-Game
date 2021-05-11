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
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.GetComponent<PlayerStat>();
    }
    public void SaveData() 
    {
        SaveSystem.SavePlayer(player);
        SaveSystem.SaveWorld();
    }

    public void LoadData() 
    {
        SaveSystem.LoadPlayer();
    }
}
