using MLAPI;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public ItemDatabase database;
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
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.GetComponent<PlayerStat>();
        PlayerActor data = SaveSystem.LoadPlayer();

        Debug.Log(data.equipment);
        foreach (int id in data.equipment) 
        {
            if (id != -1) 
            {
                GameItem item = database.getItem[id];
                FindObjectOfType<EquipmentManager>().equipItem((EquipItem)item);
            }
        }
        Debug.Log(data.inventory);
        foreach (int id in data.inventory) 
        {
            if (id != -1) 
            {
                GameItem item = database.getItem[id];
                Debug.Log("Lol This is the item ID " + id);
                FindObjectOfType<Inventory>().addItem(item);
            }
        }
    }
}
