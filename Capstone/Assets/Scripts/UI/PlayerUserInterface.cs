using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class PlayerUserInterface : MonoBehaviour
{

    public InventoryItem theInventory;
    public LocalPlayerMovement thePlayer;


    public Dictionary<GameObject, InventorySlot> displayItem = new Dictionary<GameObject, InventorySlot>();



    // Start is called before the first frame update
    void Start()
    {
        setInventoryParent();
        startEvent();
        createDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
        updateDisplay();
    }

    public abstract void createDisplay();

    public void updateDisplay()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in displayItem)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = theInventory.database.getItem[_slot.Value.ID].itemImage;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                if (_slot.Key.GetComponentInChildren<TextMeshProUGUI>() != null)
                {
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.itemAmount == 1 ? " " : _slot.Value.itemAmount.ToString("n0");
                }
                
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                if (_slot.Key.GetComponentInChildren<TextMeshProUGUI>() != null)
                {
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
                    
            }
        }
    }

    public void setInventoryParent()
    {
        for(int i = 0; i < theInventory.storage.inventory.Length; i++)
        {
            theInventory.storage.inventory[i].interfaceParent = this;
        }
    }

    public void startEvent()
    {
        addEvent(gameObject, EventTriggerType.PointerEnter, delegate { onEnterInterface(gameObject); });
        addEvent(gameObject, EventTriggerType.PointerExit, delegate { onExitInterface(gameObject); });
    }

    /*
     * 
     **/
     protected void addEvent(GameObject iObject, EventTriggerType iType, UnityAction<BaseEventData> iAction)
    {
        EventTrigger trigger = iObject.GetComponent<EventTrigger>();
        var triggerEvent = new EventTrigger.Entry();
        triggerEvent.eventID = iType;
        triggerEvent.callback.AddListener(iAction);
        trigger.triggers.Add(triggerEvent);
    }

    public void mouseEnter(GameObject iObject)
    {
        thePlayer.theMouseItem.hoverObject = iObject;
        if (displayItem.ContainsKey(iObject))
        {
            thePlayer.theMouseItem.hoverItem = displayItem[iObject];
        }

    }

    public void mouseExit(GameObject iObject)
    {
        thePlayer.theMouseItem.hoverObject = null;
        thePlayer.theMouseItem.hoverItem = null;
    }

    public void onEnterInterface(GameObject iObject)
    {
        thePlayer.theMouseItem.userInterface = iObject.GetComponent<PlayerUserInterface>();
    }
    
    public void onExitInterface(GameObject iObject)
    {
        thePlayer.theMouseItem.userInterface = null;
    }


    public void mouseBeginDrag(GameObject iObject)
    {
        var mouseObject = new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);

        if (displayItem[iObject].ID >= 0)
        {
            var objectImage = mouseObject.AddComponent<Image>();
            objectImage.sprite = theInventory.database.getItem[displayItem[iObject].ID].itemImage;
            objectImage.raycastTarget = false;
        }
        thePlayer.theMouseItem.mouseObject = mouseObject;
        thePlayer.theMouseItem.item = displayItem[iObject];
    }


    public void mouseEndDrag(GameObject iObject)
    {
        var mouseItem = thePlayer.theMouseItem;
        var mouseHoverItem = mouseItem.hoverItem;
        var mouseHoverObject = mouseItem.hoverObject;
        var getItemObject = theInventory.database.getItem;

        if(mouseItem.userInterface != null)
        {
            if (mouseHoverObject)
            {

                if (mouseHoverItem.canPlace(getItemObject[displayItem[iObject].ID]) && (mouseHoverItem.ID <= -1 || (mouseHoverItem.ID >= 0 && displayItem[iObject].canPlace(getItemObject[mouseHoverItem.theItem.itemID]))))
                {

                    theInventory.switchItem(displayItem[iObject], mouseHoverItem.interfaceParent.displayItem[mouseItem.hoverObject]);
                }

            }
        }   
        else
        {
         theInventory.removeItem(displayItem[iObject].theItem);
        }

        Destroy(mouseItem.mouseObject);
        mouseItem.item = null;
    }




    public void mouseDrag(GameObject iObject)
    {
        if (thePlayer.theMouseItem.mouseObject != null)
        {
            thePlayer.theMouseItem.mouseObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

}

