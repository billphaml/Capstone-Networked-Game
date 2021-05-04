using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject RoomNameHost;
    public GameObject RoomNameClient;
    public GameObject NickName;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventory = null;
    private bool isOpen = false;

    [Header("Android UI")]
    [SerializeField] private GameObject androidUI = null;

    [SerializeField] private GameDialogueManager localDialogueManager = null;

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
    }

    private void CheckInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpen)
            {
                inventory.alpha = 0;
                inventory.interactable = false;
                inventory.blocksRaycasts = false;
                isOpen = false;
            }
            else
            {
                inventory.alpha = 1;
                inventory.interactable = true;
                inventory.blocksRaycasts = true;
                isOpen = true;
            }
        }
    }
}
