using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* This is the EquipmentSlot class
 * The purpose of this class is to represent the player's equipment in the equipment UI as a button.
 * It connects with the EquipmentManager in order to tell the EquipmentManager to take the item off the player when they're "clicked"
 * It connects to the Inventory in order to add its EquipItem into the inventory list when "clicked"
 * */
public class EquipmentSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;
    public Button dropButton;
    public EquipItem theItem;
    public GameObject itemImagePrefab;
    int itemAmount = 0;

    public itemType theEquipmentType;
    /// <summary>
    /// Background image for empty item slot.
    /// </summary>
    [SerializeField] private Sprite emptyImage;

    public Canvas theCanvas;
    private CanvasGroup theSlotCanvas;
    public GameObject dragItem;
    public bool isDragSucess;
    private RectTransform dragRectTransform;

    private void Awake()
    {
        theSlotCanvas = GetComponent<CanvasGroup>();
    }

    public void addItem(EquipItem iItem)
    {
        theItem = iItem;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = iItem.itemImage;
        transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        //itemAmount++;
        //itemAmountText.text = itemAmount.ToString();
        dropButton.interactable = true;
    }

    public void clearSlot()
    {
        theItem = null;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptyImage;
        transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        //itemAmount = 0;
        //itemAmountText.text = "";
        //dropButton.interactable = false;
    }

    public void onRemoveButton()
    {
        theInventory.removeItem(theItem);
    }

    public void UseItem()
    {
        if (theItem != null)
        {
            if (theInventory.canAdd())
            {
                GameItem returnItem = theItem;
                theInventory.addItem(returnItem);
                theEquipmentManager.removeFromInventory(theItem);
                clearSlot();
            }
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Did you just drop something");
            DragItem theDragItem = dragItemHandler(eventData);

            if (theDragItem != null)
            {
                insertEquipmentHandler(theDragItem.theItem,eventData);
                
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("You are currently dragging");
        if (theItem != null)
        {
            dragRectTransform.anchoredPosition += eventData.delta / theCanvas.scaleFactor;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("You begin dragging something");
        if (theItem != null)
        {
            dragItem = Instantiate(itemImagePrefab, transform.parent.parent.parent, false);
            dragItem.GetComponent<DragItem>().theItem = theItem;
            dragItem.GetComponent<Image>().sprite = theItem.itemImage;
            dragItem.transform.position = Input.mousePosition;
            dragRectTransform = dragItem.GetComponent<RectTransform>();
            theSlotCanvas.alpha = 0.4f;
            theSlotCanvas.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("You just stopped dragging something");
        if (theItem != null)
        {
            Object.Destroy(dragItem);
            theSlotCanvas.alpha = 1f;
            theSlotCanvas.blocksRaycasts = true;
            if (isDragSucess == true)
            {
                theEquipmentManager.removeFromInventory(theItem);
                isDragSucess = false;
            }
        }
    }

    private void insertEquipmentHandler(GameItem theEquipment, PointerEventData eventData)
    {
        switch (theEquipment.gameItemType)
        {
            case itemType.HEAD:
                if(theEquipmentType == itemType.HEAD)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.ARMOR:
                if (theEquipmentType == itemType.ARMOR)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.NECKLACE:
                if (theEquipmentType == itemType.NECKLACE)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.RING:
                if (theEquipmentType == itemType.RING)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.SWORD:
                if (theEquipmentType == itemType.SWORD)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.GREATSWORD:
                if (theEquipmentType == itemType.SWORD)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.DAGGER:
                if (theEquipmentType == itemType.SWORD)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.BOW:
                if (theEquipmentType == itemType.SWORD)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
            case itemType.MAGIC:
                if (theEquipmentType == itemType.SWORD)
                {
                    theEquipmentManager.equipItem((EquipItem)theEquipment);
                    isDragEventHandler(eventData);
                }
                break;
        }
    }
        private DragItem dragItemHandler(PointerEventData eventData)
    {
        if(eventData.pointerDrag.gameObject.GetComponent<EquipmentSlot>() != null)
        {
            return eventData.pointerDrag.gameObject.GetComponent<EquipmentSlot>().dragItem.GetComponent<DragItem>();
        }
        else if(eventData.pointerDrag.gameObject.GetComponent<InventorySlot>() != null)
        {
            return eventData.pointerDrag.gameObject.GetComponent<InventorySlot>().dragItem.GetComponent<DragItem>();
        }
        else
        {
            return eventData.pointerDrag.gameObject.GetComponent<ItemSlot>().dragItem.GetComponent<DragItem>();
        }
    }

    private void isDragEventHandler(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.GetComponent<EquipmentSlot>() != null)
        {
            eventData.pointerDrag.gameObject.GetComponent<EquipmentSlot>().isDragSucess = true;
        }
        else if (eventData.pointerDrag.gameObject.GetComponent<InventorySlot>() != null)
        {
            eventData.pointerDrag.gameObject.GetComponent<InventorySlot>().isDragSucess = true;
        }
        else if (eventData.pointerDrag.gameObject.GetComponent<ItemSlot>() != null)
        {
            eventData.pointerDrag.gameObject.GetComponent<ItemSlot>().isDragSucess = true;
        }
    }
    }
