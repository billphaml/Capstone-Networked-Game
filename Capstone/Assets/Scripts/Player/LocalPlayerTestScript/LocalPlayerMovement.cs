using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    public bool canMove;
    public InventoryItem theInventory;
    public InventoryItem theEquipment;

    
    public MouseInventory theMouseItem = new MouseInventory();

    [SerializeField]
    private float moveSpeed = 7f;

    private Rigidbody2D rb;


    private void Awake()
    {
        canMove = true;
        
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        setupInventory();
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

    void Update()
    {
        GrabInputPC();

        if (Input.GetKeyDown(KeyCode.P))
        {
            theInventory.saveInventory();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            theInventory.loadInventory();
        }
    }

    void GrabInputPC()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1f);
    }

    private void FixedUpdate()
    {
         rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void turnOnMove()
    {
        canMove = true;
    }

    public void turnOffMove()
    {
        canMove = false;
    }
}
