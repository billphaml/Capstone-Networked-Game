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


    void Awake()
    {
        storage = new Inventory();
    }

    /*
    private void OnEnable()
    {
        database = Resources.Load<ItemDatabase>("/Database/ItemDatabase");
    }
    **/

    public void addItem(GameItem _theItem, int _itemAmount)
    {
        if(_theItem.gameItemType != itemType.DEFAULT && _theItem.gameItemType != itemType.CONSUME)
        {
            setEmptySlot(_theItem, _itemAmount);
            return;
        }

        for (int i = 0; i < storage.inventory.Length; i++)
        {
            if(storage.inventory[i].ID == _theItem.itemID)
            {
                storage.inventory[i].addAmount(_itemAmount);
                return;
            }
        }
        setEmptySlot(_theItem, _itemAmount);

    }

    public InventorySlot setEmptySlot(GameItem iItem, int iAmount)
    {
        for(int i = 0; i < storage.inventory.Length; i++)
        {
            if(storage.inventory[i].ID <= -1)
            {
                storage.inventory[i].updateSlot(iItem, iAmount);
                return storage.inventory[i];
            }
            
        }
        // Set up functionality for when the inventory is full
        return null;
    }


    public void OnAfterDeserialize()
    {
        /*
        for(int i = 0; i < storage.inventory.Length; i++)
        {
            if(storage.inventory[i].theItem != null)
            {
                storage.inventory[i].theItem = database.getItem[storage.inventory[i].ID];
            }
        }
       **/
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void switchItem(InventorySlot firstItem, InventorySlot secondItem)
    {
        InventorySlot tempItem = new InventorySlot(secondItem.ID, secondItem.theItem, secondItem.itemAmount);
        secondItem.updateSlot(firstItem.ID, firstItem.theItem, firstItem.itemAmount);
        firstItem.updateSlot(tempItem.ID,tempItem.theItem, tempItem.itemAmount);
    }

    public void removeItem(GameItem iItem)
    {
        for(int i = 0; i < storage.inventory.Length; i++)
        {
            if(storage.inventory[i].theItem == iItem)
            {
                storage.inventory[i].updateSlot(-1, null, 0);
                return;
            }
        }
    }


    [ContextMenu("Save Inventory")]
    public void saveInventory()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Create(string.Concat(Application.persistentDataPath,savePath));
        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    [ContextMenu("Load Inventory")]
    public void loadInventory()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(saveFile).ToString(), this);
            saveFile.Close();
        }
    }

    [ContextMenu("Clear Inventory")]
    public void clearInventory()
    {
        storage.Clear();
    }

}



