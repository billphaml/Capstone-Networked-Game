using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public MouseInventory theMouseItem = new MouseInventory();
    public GameObject inventoryPrefab;
    public InventoryItem theInventory;

    public int xStart;
    public int yStart;
    public int xBetweenItem;
    public int yBetweenItem;
    public int columnNum;

    Dictionary<GameObject, InventorySlot> displayItem = new Dictionary<GameObject, InventorySlot>();



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
        displayItem = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < theInventory.storage.inventory.Length; i++)
        {
            var itemObject = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            itemObject.GetComponent<RectTransform>().localPosition = getPosition(i);

            if(theInventory.storage.inventory[i].theItem != null)
            {
                itemObject.transform.GetChild(0).GetComponent<Image>().sprite = theInventory.database.getItem[theInventory.storage.inventory[i].ID].itemImage;
            }

            addEvent(itemObject, EventTriggerType.PointerEnter, delegate { mouseEnter(itemObject); });
            addEvent(itemObject, EventTriggerType.PointerExit, delegate { mouseExit(itemObject); });
            addEvent(itemObject, EventTriggerType.BeginDrag, delegate { mouseBeginDrag(itemObject); });
            addEvent(itemObject, EventTriggerType.EndDrag, delegate { mouseEndDrag(itemObject); });
            addEvent(itemObject, EventTriggerType.Drag, delegate { mouseDrag(itemObject); });

            displayItem.Add(itemObject, theInventory.storage.inventory[i]);
        }
    }

    public void updateDisplay()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in displayItem)
        {
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = theInventory.database.getItem[_slot.Value.ID].itemImage;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.itemAmount == 1 ? " " : _slot.Value.itemAmount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(xStart + (xBetweenItem * (i % columnNum)), yStart + (-yBetweenItem * (i / columnNum)), 0f);
    }


    /*
     * 
     **/
    private void addEvent(GameObject iObject, EventTriggerType iType, UnityAction<BaseEventData> iAction)
    {
        EventTrigger trigger = iObject.GetComponent<EventTrigger>();
        var triggerEvent = new EventTrigger.Entry();
        triggerEvent.eventID = iType;
        triggerEvent.callback.AddListener(iAction);
        trigger.triggers.Add(triggerEvent);
    }

    public void mouseEnter(GameObject iObject)
    {
        theMouseItem.hoverObject = iObject;
        if (displayItem.ContainsKey(iObject))
        {
            theMouseItem.hoverItem = displayItem[iObject];
        }

    }

    public void mouseExit(GameObject iObject)
    {
        theMouseItem.hoverObject = null;
         theMouseItem.hoverItem = null;
    }


    public void mouseBeginDrag(GameObject iObject)
    {
        var mouseObject = new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);

        if(displayItem[iObject].ID >= 0)
        {
            var objectImage = mouseObject.AddComponent<Image>();
            objectImage.sprite = theInventory.database.getItem[displayItem[iObject].ID].itemImage;
            objectImage.raycastTarget = false;
        }
        theMouseItem.mouseObject = mouseObject;
        theMouseItem.item = displayItem[iObject];
    }


    public void mouseEndDrag(GameObject iObject)
    {
        if (theMouseItem.hoverObject)
        {
            theInventory.switchItem(displayItem[iObject], displayItem[theMouseItem.hoverObject]);
        }
        else
        {
            theInventory.removeItem(displayItem[iObject].theItem);
        }
        Destroy(theMouseItem.mouseObject);
        theMouseItem.item = null;
    }


    

    public void mouseDrag(GameObject iObject)
    {
        if(theMouseItem.mouseObject != null)
        {
            theMouseItem.mouseObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

}

