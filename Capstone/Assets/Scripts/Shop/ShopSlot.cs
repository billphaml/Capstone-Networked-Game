using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerDownHandler
{
    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;

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

    public void AddItem(GameItem iItem)
    {
        theItem = iItem;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = iItem.itemImage;
        //itemAmount++;
        itemAmountText.text = itemAmount.ToString();
    }

    public void ClearSlot()
    {
        theItem = null;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptyImage;
        itemAmountText.text = "";
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
            // Check if you can add into inventory and check if player has enough money.
            if (theInventory.CanAdd())
            {
                //remove money
                theInventory.AddItem(theItem);
                ShopManager.theShopManager.removeItem(theItem);
            }
            
        }
    }
}

