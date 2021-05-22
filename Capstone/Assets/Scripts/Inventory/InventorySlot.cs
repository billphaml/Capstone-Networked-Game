/******************************************************************************
 * The purpose of this class is to represent each individual "box" in a
 * player's inventory. Each one of the InventorySlot is a button; when clicked,
 * it called the UseItem method to differentiate each individual item's
 * function based on type. It contact with the EquipmentManager in order to
 * allows for equipment communication. Also implements drag and drop
 * functionality of items.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

// Comment out to debug
#undef DEBUG

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;

    public CraftingManager theCraftingManager;

    public ShopManager theShopManager;

    public TextMeshProUGUI itemAmountText;

    public Button dropButton;

    GameItem theItem;

    public GameObject itemImagePrefab;

    //int itemAmount = 0;

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

    public void AddItem(GameItem iItem)
    {
        theItem = iItem;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = iItem.itemImage;
        //itemAmount++;
        //itemAmountText.text = itemAmount.ToString();
        dropButton.interactable = true;
    }

    public void ClearSlot()
    {
        theItem = null;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptyImage;
        //itemAmount = 0;
        itemAmountText.text = "";
        dropButton.interactable = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
#if DEBUG
        Debug.Log("You begin dragging something");
#endif
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

    public void OnDrag(PointerEventData eventData)
    {
#if DEBUG
        Debug.Log("You are currently dragging" );
#endif
        if (theItem != null)
        {
            dragRectTransform.anchoredPosition += eventData.delta / theCanvas.scaleFactor;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
#if DEBUG
        Debug.Log("You just stopped dragging something");
#endif
        if (theItem != null)
        {
            Object.Destroy(dragItem);
            theSlotCanvas.alpha = 1f;
            theSlotCanvas.blocksRaycasts = true;
            if(isDragSucess == true)
            {
                theInventory.RemoveItem(theItem);
                isDragSucess = false;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
#if DEBUG
            Debug.Log("Did you just drop something");
#endif
            DragItem theDragItem = DragItemHandler(eventData);

            if (theDragItem != null)
            {
                theInventory.AddItem(theDragItem.theItem);
                IsDragEventHandler(eventData);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
#if DEBUG
        Debug.Log("Clicked onto the inventory slot");
#endif
    }

    public void OnRemoveButton()
    {
        theInventory.RemoveItem(theItem);
    }

    public void UseItem()
    {
        if (theItem != null)
        {
            if (UIManager.theUIManager.isOpenShop == true)
            {
                if (theShopManager.canAdd())
                {
                    theShopManager.addItem(theItem);
                    theInventory.RemoveItem(theItem);
                }

            } else if (UIManager.theUIManager.isOpenCrafting == true)
            {
                if(theCraftingManager.theCraftingSlot[0].theItem == null)
                {
                    theCraftingManager.theCraftingSlot[0].AddItem(theItem);
                    theInventory.RemoveItem(theItem);
                    theCraftingManager.UpdateRecipeResult();

                } else if(theCraftingManager.theCraftingSlot[1].theItem == null)
                {
                    theCraftingManager.theCraftingSlot[1].AddItem(theItem);
                    theInventory.RemoveItem(theItem);
                    theCraftingManager.UpdateRecipeResult();
                }
            }
            else
            {
                switch (theItem.gameItemType)
                {
                    case itemType.CONSUME:
                        // add health, mana and sp value to the player.

                        break;
                    case itemType.DEFAULT:
                        break;
                    default:
                        EquipItem dummyEquip = (EquipItem)theItem;
                        GameItem returnItem = theEquipmentManager.equipItem(dummyEquip);

                        if (returnItem == null)
                        {
                            theInventory.RemoveItem(theItem);
                        }
                        else
                        {
                            theInventory.RemoveItem(theItem);
                            theInventory.AddItem(returnItem);
                        }
                        break;
                }
            }
        }
    }

    private DragItem DragItemHandler(PointerEventData eventData)
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

    private void IsDragEventHandler(PointerEventData eventData)
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
