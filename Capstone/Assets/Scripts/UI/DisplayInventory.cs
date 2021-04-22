using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryItem;
    public InventoryItem theInventory;

    public int xStart;
    public int yStart;
    public int xBetweenItem;
    public int yBetweenItem;
    public int columnNum;

    Dictionary<InventorySlot, GameObject> displayItem = new Dictionary<InventorySlot, GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        createDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        updateDisplay();
    }

    public void createDisplay()
    {
        for(int i = 0; i < theInventory.storage.inventory.Count; i++)
        {
            var itemObject = Instantiate(inventoryItem, Vector3.zero, Quaternion.identity, transform);
            itemObject.GetComponent<RectTransform>().localPosition = getPosition(i);
            itemObject.GetComponentInChildren<TextMeshProUGUI>().text = theInventory.storage.inventory[i].itemAmount.ToString("n0");
        }
    }

    public void updateDisplay()
    {
        for(int i = 0; i < theInventory.storage.inventory.Count; i++)
        {
            if (displayItem.ContainsKey(theInventory.storage.inventory[i]))
            {
                displayItem[theInventory.storage.inventory[i]].GetComponentInChildren<TextMeshProUGUI>().text = theInventory.storage.inventory[i].itemAmount.ToString("n0");
            }
            else
            {
                var itemObject = Instantiate(inventoryItem, Vector3.zero, Quaternion.identity, transform);
                //itemObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite;
                itemObject.GetComponent<RectTransform>().localPosition = getPosition(i);
                itemObject.GetComponentInChildren<TextMeshProUGUI>().text = theInventory.storage.inventory[i].itemAmount.ToString("n0");
                displayItem.Add(theInventory.storage.inventory[i], itemObject);
            }
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(xStart + (xBetweenItem * (i % columnNum)), yStart + (-yBetweenItem * (i / columnNum)), 0f);
    }
}
