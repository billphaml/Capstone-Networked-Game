using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerStat player)
    {
        //player.thePlayer.inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        //player.thePlayer.equipment = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<EquipmentManager>();
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
        string path = Application.persistentDataPath + "/player/data";
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
}
