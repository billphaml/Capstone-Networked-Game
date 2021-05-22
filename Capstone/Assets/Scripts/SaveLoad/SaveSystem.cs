/******************************************************************************
 * This Class contains the logic for our saving and loading, it calls the save
 * methods from the respective classes that have them and loads the save.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[System.Serializable]
public static class SaveSystem
{
    public static void SavePlayer(PlayerStat player)
    {
        Debug.Log("This is the player" + player.ToString());

        GameObject theInventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager");
        GameObject theLocalGameManager = GameObject.FindGameObjectWithTag("Local Game Manager");
        player.thePlayer.inventory = theInventoryManager.GetComponent<Inventory>().SaveInventory();
        
        player.thePlayer.equipment = theInventoryManager.GetComponent<EquipmentManager>().SaveEquipped();

        player.thePlayer.currentQuests = theLocalGameManager.GetComponent<LocalGameManager>().saveQuests();
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

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/world.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        LocalGameManager quests = GameObject.FindGameObjectWithTag("Networked Game Manager").GetComponent<LocalGameManager>();
       
        WorldSave data = new WorldSave();
        data.CurrentQuests = quests.saveQuests();
        var Npcs = GameObject.FindGameObjectsWithTag("NPC");
        List<bool> introYN =  new List<bool>();

        foreach (GameObject Npc in Npcs) 
        {
            introYN.Add(Npc.GetComponent<NPCBehavior>().isIntro);
        }

        data.interactedWith = introYN;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Replace void with world file
    public static WorldSave LoadWorld()
    {
        string path = Application.persistentDataPath + "/world.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldSave data = formatter.Deserialize(stream) as WorldSave;
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
