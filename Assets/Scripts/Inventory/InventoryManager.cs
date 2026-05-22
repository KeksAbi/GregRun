using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    public GameObject ShopMenu;
    public GameObject SkillTreeMenu;
    public GameObject NotebookMenu;

    public GameObject ButtonPanel;

    public bool InventoryOpen;
    public bool EquipmentOpen;

    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;
    public EquippedSlot[] equippedSlot;

    public ItemSO[] itemSOs;

    [Header("Keybinds")]
    public KeyCode InventoryKey = KeyCode.Tab;
    public KeyCode EquipmentKey = KeyCode.E;

    private void Start()
    {
        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(false);
        ShopMenu.SetActive(false);
        SkillTreeMenu.SetActive(false);
        NotebookMenu.SetActive(false);

        ButtonPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(InventoryKey))
        {
            Inventory();
        }

        if (Input.GetKeyDown(EquipmentKey))
        {
            Equipment();
        }
    }

    public void DisableAll()
    {
        Time.timeScale = 1;

        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(false);
        ShopMenu.SetActive(false);
        SkillTreeMenu.SetActive(false);
        NotebookMenu.SetActive(false);

        ButtonPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            DisableAll();
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
            ShopMenu.SetActive(false);
            SkillTreeMenu.SetActive(false);
            NotebookMenu.SetActive(false);

            ButtonPanel.SetActive(true);

            InventoryOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Equipment()
    {
        if (EquipmentMenu.activeSelf)
        {
            DisableAll();
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
            ShopMenu.SetActive(false);
            SkillTreeMenu.SetActive(false);
            NotebookMenu.SetActive(false);

            ButtonPanel.SetActive(true);

            EquipmentOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Shop()
    {
        if (ShopMenu.activeSelf)
        {
            DisableAll();
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            ShopMenu.SetActive(true);
            SkillTreeMenu.SetActive(false);
            NotebookMenu.SetActive(false);

            ButtonPanel.SetActive(true);

            EquipmentOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SkillTree()
    {
        if (SkillTreeMenu.activeSelf)
        {
            DisableAll();
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            ShopMenu.SetActive(false);
            SkillTreeMenu.SetActive(true);
            NotebookMenu.SetActive(false);

            ButtonPanel.SetActive(false);

            InventoryOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Notebook()
    {
        if (NotebookMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            ShopMenu.SetActive(false);
            SkillTreeMenu.SetActive(false);
            NotebookMenu.SetActive(false);

            ButtonPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            ShopMenu.SetActive(false);
            SkillTreeMenu.SetActive(false);
            NotebookMenu.SetActive(true);

            ButtonPanel.SetActive(true);

            InventoryOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public bool UseItem(string itemName)
    {
        Debug.Log("Found InventoryManger UseItem()");

        for (int i = 0; i < itemSOs.Length; i++)
        {
            Debug.Log("Searching Item");

            if (itemSOs[i].itemName == itemName)
            {
                Debug.Log("Found Item - Send Back");

                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, GameObject itemGameObject, string itemDescription, ItemType itemType)
    {
        if (itemType == ItemType.consumable || itemType == ItemType.scrap)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false && (itemSlot[i].name == name || itemSlot[i].quantity == 0))
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemGameObject, itemDescription, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemGameObject, itemDescription, itemType);

                        return leftOverItems;
                    }
                    return 0;
                }
            }
            return quantity;
        }
        else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].isFull == false && (equipmentSlot[i].name == name || equipmentSlot[i].quantity == 0))
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemGameObject, itemDescription, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemGameObject, itemDescription, itemType);

                        return leftOverItems;
                    }
                    return 0;
                }
            }
            return quantity;
        }
    }

    public void DeselectAllSlots()
    {
        for(int i = 0;i < itemSlot.Length;i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].selectedShader.SetActive(false);
            equipmentSlot[i].thisItemSelected = false;
        }

        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisItemSelected = false;
        }
    }

}

public enum ItemType
{
    none,
    consumable,
    scrap,
    head,
    body,
    shirt,
    legs,
    boots,
    mainHand,
    offHand,
    relic,
};
