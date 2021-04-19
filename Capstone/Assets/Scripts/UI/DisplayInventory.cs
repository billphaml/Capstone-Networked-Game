using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInventory : MonoBehaviour
{
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
        for(int i = 0; i < theInventory.Inventory.Count; i++)
        {
            var itemObject = Instantiate(theInventory.Inventory[i].theItem.itemGameObject, Vector3.zero, Quaternion.identity, transform);
            itemObject.GetComponent<RectTransform>().localPosition = getPosition(i);
            itemObject.GetComponentInChildren<TextMeshProUGUI>().text = theInventory.Inventory[i].itemAmount.ToString("n0");
        }
    }

    public void updateDisplay()
    {
        for(int i = 0; i < theInventory.Inventory.Count; i++)
        {
            if (displayItem.ContainsKey(theInventory.Inventory[i]))
            {
                displayItem[theInventory.Inventory[i]].GetComponentInChildren<TextMeshProUGUI>().text = theInventory.Inventory[i].itemAmount.ToString("n0");
            }
            else
            {
                var itemObject = Instantiate(theInventory.Inventory[i].theItem.itemGameObject, Vector3.zero, Quaternion.identity, transform);
                itemObject.GetComponent<RectTransform>().localPosition = getPosition(i);
                itemObject.GetComponentInChildren<TextMeshProUGUI>().text = theInventory.Inventory[i].itemAmount.ToString("n0");
                displayItem.Add(theInventory.Inventory[i], itemObject);
            }
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(xStart + (xBetweenItem * (i % columnNum)), yStart + (-yBetweenItem * (i / columnNum)), 0f);
    }
}
