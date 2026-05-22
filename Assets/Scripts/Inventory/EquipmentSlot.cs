using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
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

    [Header("Item Slot")]

    [SerializeField]
    private Image itemImage;

    [Header("Equipped Slots")]
    [SerializeField]
    private EquippedSlot headSlot, bodySlot, shirtSlot, legsSlot, bootsSlot, mainHandSlot, offHandSlot, relicSlot;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory - Canvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("Inventory - Canvas").GetComponent<EquipmentSOLibrary>();
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
        this.quantity = 1;
        isFull = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
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
        if (isFull)
        {
            if (thisItemSelected)
            {
                EquipGear();
            }
            else
            {
                inventoryManager.DeselectAllSlots();
                selectedShader.SetActive(true);
                thisItemSelected = true;
                for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
                {
                    if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                    {
                        equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                    }
                }
            }
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerMain>().TurnOffPreviewStats();
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    private void EquipGear()
    {
        if (itemType == ItemType.head)
        {
            headSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.body)
        {
            bodySlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.shirt)
        {
            shirtSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.legs)
        {
            legsSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.boots)
        {
            bootsSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.mainHand)
        {
            mainHandSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.offHand)
        {
            offHandSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }
        if (itemType == ItemType.relic)
        {
            relicSlot.EquipGear(itemSprite, itemName, itemGameObject, itemDescription);
        }

        EmptySlot();
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
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }

    private void EmptySlot()
    {
        itemImage.sprite = emptySprite;
        isFull = false;
    }
}

