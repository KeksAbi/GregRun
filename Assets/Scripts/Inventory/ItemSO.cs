using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{

    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public float amountToChangeStat;

    public bool UseItem()
    {
        // MaxHealth
        if (statToChange == StatToChange.maxHealth)
        {
            PlayerMain playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
            playerMain.AddMaxHealth(amountToChangeStat);
            return true;
        }

        // MaxStamina
        if (statToChange == StatToChange.maxStamina)
        {
            PlayerMain playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
            playerMain.AddMaxStamina(amountToChangeStat);
            return true;
        }
        
        return false;
    }



    public enum StatToChange
    {
        none,
        maxHealth,
        health,
        maxStamina,
        stamina,
        maxArmor,
        armor
    };

}
