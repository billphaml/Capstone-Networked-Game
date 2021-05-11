using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;


[System.Serializable]
public static class SaveSystem
{
    public static void SavePlayer(PlayerStat player)
    {
        Debug.Log("This is the player" + player.ToString());

        GameObject theInventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager");
        player.thePlayer.inventory = theInventoryManager.GetComponent<Inventory>().saveInventory();
        
        player.thePlayer.equipment = theInventoryManager.GetComponent<EquipmentManager>().saveEquipped();
        SavePlayerHelper(player);
    }

    private static void SavePlayerHelper(PlayerStat player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerActor data = player.thePlayer;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerActor LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerActor data = formatter.Deserialize(stream) as PlayerActor;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveWorld()
    {

    }

    // Replace void with world file
    public static void LoadWorld()
    {

    }
}
