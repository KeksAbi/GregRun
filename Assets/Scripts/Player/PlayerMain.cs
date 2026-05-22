using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{
    [Header("Leveling System")]
    public int playerLevel;
    public float playerCurrentXP;
    public float playerXPToNextLevel;

    [Header("Health")]
    public float playerMaxHealth;
    public float playerCurrentHealth;

    public HealthBar healthBar;

    [Header("Armor")]
    public float playerMaxArmor;
    public float playerCurrentArmor;

    public ArmorBar armorBar;

    [Header("Stamina")]
    public float playerMaxStamina;
    public float playerCurrentStamina;

    public StaminaBar staminaBar;

    [Header("Weight")]
    public float playerMaxWeight;
    public float playerCurrentWeight;

    [Header("StatText")]
    [SerializeField]
    private TMP_Text playerHealthText;
    [SerializeField]
    private TMP_Text playerArmorText;
    [SerializeField]
    private TMP_Text playerStaminaText;

    [SerializeField]
    private TMP_Text playerMaxHealthPreText;
    [SerializeField]
    private TMP_Text playerMaxArmorPreText;
    [SerializeField]
    private TMP_Text playerMaxStaminaPreText;

    [SerializeField]
    private TMP_Text playerWeightPreText;

    [SerializeField]
    private string slash;

    [SerializeField]
    private Image previewImage;

    [SerializeField]
    private GameObject selectedItemStats;
    [SerializeField]
    private GameObject selectedItemImage;

    private void Start()
    {
        // Leveling
        playerLevel = 1;
        playerCurrentXP = 0f;
        playerXPToNextLevel = 100f;

        // Health
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);

        // Armor
        playerCurrentArmor = playerMaxArmor;
        armorBar.SetMaxArmor(playerMaxArmor);

        // Stamina
        playerCurrentStamina = playerMaxStamina;
        staminaBar.SetMaxStamina(playerMaxStamina);

        // Weight
        playerCurrentWeight = 0f;

        UpdateEquipmentStats();
        TurnOffPreviewStats();
    }

    public void PlayerTakeDamage(float damage)
    {
        playerCurrentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + playerCurrentHealth);

        // HealthBar
        healthBar.SetHealth(playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void AddMaxHealth(float amoutToChangeStat)
    {
        playerMaxHealth = playerMaxHealth + amoutToChangeStat;

        float playerNewMaxHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerNewMaxHealth);

        playerCurrentHealth = playerMaxHealth;
    }

    public void AddMaxStamina(float amoutToChangeStat)
    {
        playerMaxStamina = playerMaxStamina + amoutToChangeStat;

        float playerNewMaxStamina = playerMaxStamina;
        staminaBar.SetMaxStamina(playerNewMaxStamina);

        playerCurrentStamina = playerMaxStamina;
    }

    public void AddMaxArmor(float amoutToChangeStat)
    {
        playerMaxArmor += amoutToChangeStat;
        playerCurrentArmor = playerMaxArmor;
    }

    public void AddMaxWeight(float amountToChangeStat)
    {
        playerMaxWeight += amountToChangeStat;
    }

    public void AddWeight(float amountToAdd)
    {
        playerCurrentWeight += amountToAdd;
        if (playerCurrentWeight > playerMaxWeight)
        {
            playerCurrentWeight = playerMaxWeight;
        }
    }

    public void RemoveWeight(float amountToRemove)
    {
        playerCurrentWeight -= amountToRemove;
        if (playerCurrentWeight < 0)
        {
            playerCurrentWeight = 0;
        }
    }

    public float GetWeightPercentage()
    {
        if (playerMaxWeight == 0) return 0f;
        return playerCurrentWeight / playerMaxWeight;
    }

    public void UpdateEquipmentStats()
    {
        playerHealthText.text = playerCurrentHealth.ToString() + slash + playerMaxHealth.ToString();
        playerArmorText.text = playerCurrentArmor.ToString() + slash + playerMaxArmor.ToString();
        playerStaminaText.text = playerCurrentStamina.ToString() + slash + playerMaxStamina.ToString();
    }

    public void PreviewEquipmentStats(float playerMaxHealth, float playerMaxArmor, float playerMaxStamina, float weight, Sprite itemSprite)
    {
        playerMaxHealthPreText.text = playerMaxHealth.ToString();
        playerMaxArmorPreText.text = playerMaxArmor.ToString();
        playerMaxStaminaPreText.text = playerMaxStamina.ToString();

        if (playerWeightPreText != null)
        {
            if (weight <= 0)
            {
                playerWeightPreText.text = "N/A";
            }
            else
            {
                playerWeightPreText.text = weight.ToString("F1") + "kg";
            }
        }

        previewImage.sprite = itemSprite;

        selectedItemImage.SetActive(true);
        selectedItemStats.SetActive(true);
    }

    public void TurnOffPreviewStats()
    {
        selectedItemImage.SetActive(false);
        selectedItemStats.SetActive(false);
    }

    public void PlayerDie()
    {
        // Death-Screen
        Debug.Log("Player is dead");
    }

}
