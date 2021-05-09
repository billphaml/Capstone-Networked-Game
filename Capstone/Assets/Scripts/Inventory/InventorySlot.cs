using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* This is the InventorySlot class
 * The purpose of this class is to represent each individual "box" in a player's inventory
 * Each one of the InventorySlot is a button; when clicked, it called the UseItem method to differentiate each individual item's function based on type
 * It contact with the EquipmentManager in order to allows for equipment communication
 * 
 * Stores the Item value and maybe the item amount if we do item stacking
 * 
 * */

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;

    public TextMeshProUGUI itemAmountText;

    public Button dropButton;

    GameItem theItem;

    public GameObject itemImagePrefab;

    int itemAmount = 0;

    public Canvas theCanvas;
    private CanvasGroup theSlotCanvas;
    public GameObject dragItem;
    public bool isDragSucess;
    private RectTransform dragRectTransform;

    /// <summary>
    /// Background image for empty item slot.
    /// </summary>
    [SerializeField] private Sprite emptyImage;

    private void Awake()
    {
        theSlotCanvas = GetComponent<CanvasGroup>();
    }

    public void addItem(GameItem iItem)
    {
        theItem = iItem;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = iItem.itemImage;
        //itemAmount++;
        //itemAmountText.text = itemAmount.ToString();
        dropButton.interactable = true;
    }

    public void clearSlot()
    {
        theItem = null;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptyImage;
        itemAmount = 0;
        itemAmountText.text = "";
        dropButton.interactable = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("You begin dragging something");
        if(theItem != null)
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

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("You are currently dragging" );
        if(theItem != null)
        {
            dragRectTransform.anchoredPosition += eventData.delta / theCanvas.scaleFactor;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("You just stopped dragging something");
        if(theItem != null)
        {
            Object.Destroy(dragItem);
            theSlotCanvas.alpha = 1f;
            theSlotCanvas.blocksRaycasts = true;
            if(isDragSucess == true)
            {
                theInventory.removeItem(theItem);
                isDragSucess = false;
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
                theInventory.addItem(theDragItem.theItem);
                isDragEventHandler(eventData);
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked onto the inventory slot");
    }

    public void onRemoveButton()
    {
        theInventory.removeItem(theItem);
    }

    public void UseItem()
    {
        if(theItem != null)
        {
            //theItem.useItem();
            switch (theItem.gameItemType)
            {
                case itemType.CONSUME:
                    break;
                case itemType.DEFAULT:
                    break;
                default:
                    EquipItem dummyEquip = (EquipItem)theItem;
                    GameItem returnItem = theEquipmentManager.equipItem(dummyEquip);

                    if(returnItem == null)
                    {
                        theInventory.removeItem(theItem);
                    }
                    else
                    {
                        theInventory.removeItem(theItem);
                        theInventory.addItem(returnItem);
                    }
                    break;
            }
        }
    }

    private DragItem dragItemHandler(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.GetComponent<EquipmentSlot>() != null)
        {
            return eventData.pointerDrag.gameObject.GetComponent<EquipmentSlot>().dragItem.GetComponent<DragItem>();
        }
        else if (eventData.pointerDrag.gameObject.GetComponent<InventorySlot>() != null)
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
