using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public float maxHealth, currentHealth, maxArmor, currentArmor, maxStamina, currentStamina;
    public float weight;
    [SerializeField]
    private Sprite itemSprite;

    public void PreviewEquipment()
    {
        GameObject.Find("Player").GetComponent<PlayerMain>().PreviewEquipmentStats(maxHealth, maxArmor, maxStamina, weight, itemSprite);
    }

    public void EquipItem()
    {
        // Update Stats
        PlayerMain playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        playerMain.playerMaxHealth += maxHealth;
        playerMain.playerCurrentHealth += currentHealth;
        playerMain.playerMaxArmor += maxArmor;
        playerMain.playerCurrentArmor += currentArmor;
        playerMain.playerMaxStamina += maxStamina;
        playerMain.playerCurrentStamina += currentStamina;
        playerMain.AddWeight(weight);

        playerMain.UpdateEquipmentStats();
    }

    public void UnEquipItem()
    {
        // Update Stats
        PlayerMain playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        playerMain.playerMaxHealth -= maxHealth;
        playerMain.playerCurrentHealth -= currentHealth;
        playerMain.playerMaxArmor -= maxArmor;
        playerMain.playerCurrentArmor -= currentArmor;
        playerMain.playerMaxStamina -= maxStamina;
        playerMain.playerCurrentStamina -= currentStamina;
        playerMain.RemoveWeight(weight);

        playerMain.UpdateEquipmentStats();
    }
}
