/******************************************************************************
 * This Class is for the UI manager. It deterimines if an Android or PC player
 * is playing. Based on that it loads in the respective UI that works for the
 * platform
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Hosting")]
    public GameObject RoomNameHost;
    public GameObject RoomNameClient;
    public GameObject NickName;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventory = null;
    private bool isOpenInventory = false;

    [Header("Android")]
    [SerializeField] private GameObject androidUI = null;

    [Header("Dialogue")]
    [SerializeField] private GameDialogueManager localDialogueManager = null;

    [Header("Crafting")]
    private bool isOpenCrafting = false;
    [SerializeField] private CanvasGroup crafting = null;

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

        CheckCraftingUI();
    }

    private void CheckInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.I) && !GameDialogueManager.theLocalGameManager.isDialogueActive)
        {
            if (isOpenInventory)
            {
                inventory.alpha = 0;
                inventory.interactable = false;
                inventory.blocksRaycasts = false;
                isOpenInventory = false;
            }
            else
            {
                inventory.alpha = 1;
                inventory.interactable = true;
                inventory.blocksRaycasts = true;
                isOpenInventory = true;
            }
        }
    }

    private void CheckCraftingUI()
    {
        if (Input.GetKeyDown(KeyCode.O) && !GameDialogueManager.theLocalGameManager.isDialogueActive)
        {
            if (isOpenCrafting)
            {
                crafting.alpha = 0;
                crafting.interactable = false;
                crafting.blocksRaycasts = false;
                isOpenCrafting = false;
            }
            else
            {
                crafting.alpha = 1;
                crafting.interactable = true;
                crafting.blocksRaycasts = true;
                isOpenCrafting = true;
            }
        }
    }
}
