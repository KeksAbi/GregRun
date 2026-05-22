using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text weightText;

    [SerializeField]
    private GameObject tooltipPanel;

    public void ShowTooltip(string itemName)
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(true);
        }

        // Try to find the item's weight from EquipmentSO
        EquipmentSOLibrary library = GameObject.Find("Inventory - Canvas")?.GetComponent<EquipmentSOLibrary>();
        if (library != null)
        {
            foreach (EquipmentSO equipment in library.equipmentSO)
            {
                if (equipment.itemName == itemName)
                {
                    if (weightText != null)
                    {
                        if (equipment.weight <= 0)
                        {
                            weightText.text = "Weight: N/A";
                        }
                        else
                        {
                            weightText.text = $"Weight: {equipment.weight:F1}kg";
                        }
                    }
                    return;
                }
            }
        }

        // If not found in equipment, show N/A
        if (weightText != null)
        {
            weightText.text = "Weight: N/A";
        }
    }

    public void HideTooltip()
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
        }
    }
}
