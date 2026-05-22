using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool alreadyPickedUp = false;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public GameObject itemGameObject;

    [TextArea]
    [SerializeField]
    public string itemDescription;


    private InventoryManager inventoryManager;

    public ItemType itemType;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory - Canvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyPickedUp) return;

        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;

            alreadyPickedUp = true;

            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemGameObject, itemDescription, itemType);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
                GetComponent<Collider>().enabled = true;
            }
        }
    }

}
