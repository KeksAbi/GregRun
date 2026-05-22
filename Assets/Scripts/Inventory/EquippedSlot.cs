using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;

    [SerializeField]
    private Image playerDisplayImage;

    [SerializeField]
    private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    public string itemName;
    private string itemDescription;

    [SerializeField]
    private GameObject itemGameObject;

    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    private bool slotInUse;
    [SerializeField]
    public GameObject selectedShader;

    [SerializeField]
    public bool thisItemSelected;

    [SerializeField]
    private Sprite emptySprite;


    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory - Canvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("Inventory - Canvas").GetComponent<EquipmentSOLibrary>();
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

    public void EquipGear(Sprite itemSprite, string itemName, GameObject itemGameObject, string itemDescription)
    {
        // If smth is already equipped, send back before rewriting data
        if(slotInUse)
        {
            UnEquipGear();
        }
        
        // Update image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        // Update data
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemGameObject = itemGameObject;

        // Update the Display Image
        playerDisplayImage.sprite = itemSprite;

        // Update Player Stats
        for(int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].EquipItem();
            }
        }

        slotInUse = true;
    }

    public void OnLeftClick()
    {
        if(thisItemSelected && slotInUse)
        {
            UnEquipGear();
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

    public void OnRightClick()
    {
        UnEquipGear();
    }

    public void UnEquipGear()
    {
        inventoryManager.DeselectAllSlots();

        inventoryManager.AddItem(itemName, 1, itemSprite, itemGameObject, itemDescription, itemType);

        // Update Slot Image
        this.itemSprite = emptySprite;
        slotImage.sprite = this.emptySprite;
        slotName.enabled = true;

        playerDisplayImage.sprite = emptySprite;

        // Update Player Stats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].UnEquipItem();
            }
        }

        GameObject.Find("Player").GetComponent<PlayerMain>().TurnOffPreviewStats();
    }
}
