using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    [Header("Item Data")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public GameObject itemGameObject;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    [SerializeField]
    private int maxNumberOfItems;

    [Header("Item Slot")]
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    [Header("Item Description")]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory - Canvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, GameObject itemGameObject, string itemDescription, ItemType itemType)
    {
        // Check to see if Slot already full
        if (isFull)
        {
            return quantity;
        }

        // Update Item-Type
        this.itemType = itemType;

        // Update Name
        this.itemName = itemName;

        this.itemGameObject = itemGameObject;

        // Update Image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        // Update Description
        this.itemDescription = itemDescription;

        // Update Quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFull = true;
        

            // Return Leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        // Update Quantity Text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            Debug.Log("Item ALREADY Selected");

            bool usable = inventoryManager.UseItem(itemName);
            if (usable == true)
            {
                Debug.Log("Item USED");

                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
        }
        else
        {
            Debug.Log("Item NOW Selected");

            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        } 
    }

    public void OnRightClick()
    {
        // Create new item
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemGameObject = itemGameObject;
        newItem.itemDescription = itemDescription;
        newItem.itemType = itemType;

        // Create and modify the MR / MF
        MeshRenderer meshRenderer = itemToDrop.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = itemToDrop.AddComponent<MeshFilter>();

        // Assing a mesh
        if (itemGameObject != null)
        {
            MeshFilter prefabMeshFilter = itemGameObject.GetComponent<MeshFilter>();
            if (prefabMeshFilter != null)
            {
                meshFilter.mesh = prefabMeshFilter.sharedMesh; 
            }

            MeshRenderer prefabMeshRenderer = itemGameObject.GetComponent<MeshRenderer>();
            if (prefabMeshRenderer != null)
            {
                meshRenderer.material = prefabMeshRenderer.sharedMaterial;
            }
        }

        // Create Collider
        BoxCollider collider = itemToDrop.AddComponent<BoxCollider>();

        // Set the location
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, 2);
        itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

        // Subtract the item
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }
}
