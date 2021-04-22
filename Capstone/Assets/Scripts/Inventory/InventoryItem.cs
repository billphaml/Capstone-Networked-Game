using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName ="New Inventory", menuName = "GameItem/Inventory/Item")]
public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    [SerializeField]
    public ItemDatabase database;
    public Inventory storage;
    /*
    private void OnEnable()
    {
        database = Resources.Load<ItemDatabase>("/Database/ItemDatabase");
    }
    **/

    public void addItem(GameItem _theItem, int _itemAmount)
    {

        for(int i = 0; i < storage.inventory.Count; i++)
        {
            if(storage.inventory[i].theItem == _theItem)
            {
                storage.inventory[i].addAmount(_itemAmount);
                return;
            }
        }
        storage.inventory.Add(new InventorySlot(database.GetID[_theItem], _theItem, _itemAmount));
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < storage.inventory.Count; i++)
        {
            storage.inventory[i].theItem = database.getItem[storage.inventory[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
    }

    public void saveInventory()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Create(string.Concat(Application.persistentDataPath,savePath));
        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    public void loadInventory()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(saveFile).ToString(), this);
            saveFile.Close();
        }
    }

}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> inventory = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public GameItem theItem;
    public int itemAmount;
    public InventorySlot(int iID, GameItem _theItem, int _itemAmount)
    {
        theItem = _theItem;
        itemAmount = _itemAmount;
    }

    public void addAmount(int iAmount)
    {
        itemAmount += iAmount;
    }
}
