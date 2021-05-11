using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public CraftingManager theCraftingManager;
    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;
    public Button dropButton;
    public GameItem theItem;
    public GameObject itemImagePrefab;
    public bool isResult;
    int itemAmount = 0;

    public GameItem returnItem;

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
        dropButton.interactable = false;
    }

    public void clearSlot()
    {
        theItem = null;
        returnItem = null;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptyImage;
        itemAmount = 0;
        itemAmountText.text = "";
        dropButton.interactable = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("You begin dragging something");
        if (theItem != null)
        {
            dragItem = Instantiate(itemImagePrefab, transform.parent.parent, false);
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
        Debug.Log("You are currently dragging");
        if (theItem != null)
        {
            dragRectTransform.anchoredPosition += eventData.delta / theCanvas.scaleFactor;
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
                if(isResult == true)
                {
                    theCraftingManager.claimResult();
                }
                else
                {
                    clearSlot();
                    theCraftingManager.updateRecipeResult();
                }
                isDragSucess = false;
            }

        }
    }


    // This is the ondrop method of the itemslot. If the item slot is a result tab, then you cannot drop an item on it.
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Did you just drop into the crafting slot ");
            DragItem theDragItem = dragItemHandler(eventData);

            if (theDragItem != null && isResult == false)
            {
                hasItemEventHandler(eventData);
                theCraftingManager.updateRecipeResult();
            }
        }
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
                theInventory.addItem(theItem);
                clearSlot();
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

    private void hasItemEventHandler(PointerEventData eventData)
    {
        DragItem theDragItem = dragItemHandler(eventData);

        if (theItem == null)
        {
            addItem(theDragItem.theItem);
            isDragEventHandler(eventData);
        }
        else
        {
            if (eventData.pointerDrag.gameObject.GetComponent<InventorySlot>() != null)
            {
                GameItem dummyItem = theItem;
                addItem(theDragItem.theItem);
                isDragEventHandler(eventData);
                eventData.pointerDrag.gameObject.GetComponent<InventorySlot>().theInventory.addItem(dummyItem);
            }
            else if(eventData.pointerDrag.gameObject.GetComponent<ItemSlot>() != null)
            {
                GameItem dummyItem = theItem;
                addItem(theDragItem.theItem);

                if(eventData.pointerDrag.gameObject.GetComponent<ItemSlot>().isResult != true)
                {
                    eventData.pointerDrag.gameObject.GetComponent<ItemSlot>().addItem(dummyItem);
                }
                else
                {
                    eventData.pointerDrag.gameObject.GetComponent<ItemSlot>().returnItem = theDragItem.theItem;
                    isDragEventHandler(eventData);
                }

            }
        }
    }
}
