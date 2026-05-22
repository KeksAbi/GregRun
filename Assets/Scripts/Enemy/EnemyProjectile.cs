using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Damage")]
    public int damageAmount = 10;
    public float destroyDelay = 5f;

    private bool hasHit = false;

    private void Start()
    {
        // Destroy projectile after a certain time if it hasn't hit anything
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Prevent multiple damage hits
        if (hasHit)
            return;

        // Check if the collided object or its parent has the PlayerMain script
        PlayerMain playerMain = collision.gameObject.GetComponent<PlayerMain>();

        // If not found on the collided object, check the parent
        if (playerMain == null)
        {
            playerMain = collision.gameObject.GetComponentInParent<PlayerMain>();
        }

        // If we found the PlayerMain script, deal damage
        if (playerMain != null)
        {
            hasHit = true;

            // Apply damage to armor first
            float remainingDamage = damageAmount;

            if (playerMain.playerCurrentArmor > 0)
            {
                playerMain.playerCurrentArmor -= remainingDamage;

                if (playerMain.playerCurrentArmor < 0)
                {
                    // Armor was broken, apply remaining damage to health
                    remainingDamage = -playerMain.playerCurrentArmor;
                    playerMain.playerCurrentArmor = 0;
                    playerMain.PlayerTakeDamage(remainingDamage);
                }
                else
                {
                    // Armor absorbed all damage
                    remainingDamage = 0;
                }

                // Update armor bar
                playerMain.armorBar.SetArmor(playerMain.playerCurrentArmor);
            }
            else
            {
                // No armor, all damage goes to health
                playerMain.PlayerTakeDamage(remainingDamage);
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
        // Optionally destroy on hitting other objects
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            hasHit = true;
            Destroy(gameObject);
        }
    }
}