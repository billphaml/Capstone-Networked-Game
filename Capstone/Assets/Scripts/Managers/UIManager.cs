/******************************************************************************
 * This Class manages all the interactions between player and the UI. It also
 * deterimines if an Android or PC player is playing and shows the
 * appropriate UI.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class UIManager : NetworkBehaviour
{
    public static UIManager theUIManager;

    [Header("Hosting")]
    public GameObject RoomNameHost;
    public GameObject RoomNameClient;
    public GameObject NickNameHost;
    public GameObject NickNameClient;


    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventory = null;
    public bool isOpenInventory = false;

    [Header("Equipment")]
    [SerializeField] private CanvasGroup equipment = null;
    public bool isOpenEquipment = false;

    [Header("Shop")]
    [SerializeField] private CanvasGroup shop = null;
    public bool isOpenShop = false;

    [Header("Android")]
    [SerializeField] private GameObject androidUI = null;

    //[Header("Dialogue")]
    //[SerializeField] private DialogueSystem localDialogueManager = null;

    [Header("Crafting")]
    public
        bool isOpenCrafting = false;
    [SerializeField] private CanvasGroup crafting = null;


     void Awake()
    {
        theUIManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID
        Destroy(androidUI);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        CheckInventoryUI();
        CheckEquipmentUI();
        CheckCraftingUI();
    }

    private void CheckInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.I) && !DialogueSystem.theLocalGameManager.isDialogueActive && (IsHost || IsClient))
        {
            if (isOpenInventory)
            {
                turnOffInventory();
                turnOffCrafting();
                turnOffEquipment();
                turnOffShop();
            }
            else
            {
                turnOnInventory();
            }
        }
    }

    private void CheckCraftingUI()
    {
        if (Input.GetKeyDown(KeyCode.O) && !DialogueSystem.theLocalGameManager.isDialogueActive && (IsHost || IsClient))
        {
            if (isOpenCrafting)
            {
                turnOffCrafting();
            }
            else
            {
                turnOnCrafting();
                turnOffEquipment();
                turnOffShop();
            }
        }
    }

    private void CheckEquipmentUI()
    {
        if (Input.GetKeyDown(KeyCode.E) && !DialogueSystem.theLocalGameManager.isDialogueActive && (IsHost || IsClient))
        {
            if (isOpenEquipment)
            {
                turnOffEquipment();
            }
            else
            {
                turnOffCrafting();
                turnOnEquipment();
                turnOffShop();
            }
        }
    }

    public void turnOnShopUI()
    {
        if (!isOpenShop)
        {
            turnOnShop();
            turnOffCrafting();
            turnOffEquipment();
        }
    }

    private void turnOnCrafting()
    {
        crafting.alpha = 1;
        crafting.interactable = true;
        crafting.blocksRaycasts = true;
        isOpenCrafting = true;
    }

    private void turnOffCrafting()
    {
        crafting.alpha = 0;
        crafting.interactable = false;
        crafting.blocksRaycasts = false;
        isOpenCrafting = false;
    }

    private void turnOnInventory()
    {
        inventory.alpha = 1;
        inventory.interactable = true;
        inventory.blocksRaycasts = true;
        isOpenInventory = true;
    }

    private void turnOffInventory()
    {
        inventory.alpha = 0;
        inventory.interactable = false;
        inventory.blocksRaycasts = false;
        isOpenInventory = false;
    }

    private void turnOnEquipment()
    {
        equipment.alpha = 1;
        equipment.interactable = true;
        equipment.blocksRaycasts = true;
        isOpenEquipment = true;
    }

    private void turnOffEquipment()
    {
        equipment.alpha = 0;
        equipment.interactable = false;
        equipment.blocksRaycasts = false;
        isOpenEquipment = false;
    }

    private void turnOnShop()
    {
        shop.alpha = 1;
        shop.interactable = true;
        shop.blocksRaycasts = true;
        isOpenShop = true;
    }

    public void turnOffShop()
    {
        shop.alpha = 0;
        shop.interactable = false;
        shop.blocksRaycasts = false;
        isOpenShop = false;
    }
}
