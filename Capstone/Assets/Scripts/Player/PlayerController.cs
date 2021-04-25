/******************************************************************************
 * 
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer playerColor;

    [SerializeField] private new GameObject camera;

    [SerializeField] private PlayerStat stats;

    [SerializeField] private PlayerMovement move;

    [SerializeField] private PlayerAttack attack;

    public InventoryItem theInventory;
    public InventoryItem theEquipment;


    public MouseInventory theMouseItem = new MouseInventory();

    // Start is called before the first frame update
    private void Start()
    {
        if (IsLocalPlayer)
        {
            playerColor.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            stats = gameObject.GetComponent<PlayerStat>();

            move = gameObject.GetComponent<PlayerMovement>();
        }

        if (!IsLocalPlayer)
        {
            camera.GetComponent<CinemachineVirtualCamera>().enabled = false;

        }
    }

    public override void NetworkStart()
    {
        base.NetworkStart();
        setupInventory();
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            move.UpdateMovement();

            attack.UpdateAttack();
        }
    }

    private void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            move.UpdateFixedMovement();
        }
    }

    private void setupInventory()
    {

        theEquipment.storage.inventory[0].allowedItem = new itemType[1];
        theEquipment.storage.inventory[0].allowedItem[0] = itemType.HEAD;
        theEquipment.storage.inventory[1].allowedItem = new itemType[1];
        theEquipment.storage.inventory[1].allowedItem[0] = itemType.NECKLACE;
        theEquipment.storage.inventory[2].allowedItem = new itemType[1];
        theEquipment.storage.inventory[2].allowedItem[0] = itemType.ARMOR;
        theEquipment.storage.inventory[3].allowedItem = new itemType[5];
        theEquipment.storage.inventory[3].allowedItem[0] = itemType.SWORD;
        theEquipment.storage.inventory[3].allowedItem[1] = itemType.GREATSWORD;
        theEquipment.storage.inventory[3].allowedItem[2] = itemType.DAGGER;
        theEquipment.storage.inventory[3].allowedItem[3] = itemType.BOW;
        theEquipment.storage.inventory[3].allowedItem[4] = itemType.MAGIC;
        theEquipment.storage.inventory[4].allowedItem = new itemType[1];
        theEquipment.storage.inventory[4].allowedItem[0] = itemType.RING;
        theEquipment.storage.inventory[5].allowedItem = new itemType[1];
        theEquipment.storage.inventory[5].allowedItem[0] = itemType.RING;
    }
}
