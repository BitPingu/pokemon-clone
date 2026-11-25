using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is on the Player Prefab.
/// It handles all collision detection with other objects
/// </summary>

public class PlayerCollision : MonoBehaviour
{
    public Vector3 PlayerSavedPos { get; set; }
    private ItemPickup_Overworld isTouchingItem;

    private void OnTriggerEnter(Collider other)
    {
        // Encounter Pokemon in the overworld
        if (other.CompareTag("WildPokemon") && SceneManager.GetActiveScene().name == "Overworld_old")
        {
            // Get the normalized direction from the enemy back to player
            Vector3 directionToSelf = (transform.position - other.transform.position).normalized;

            // Save player position away from enemy for reload
            PlayerSavedPos = other.transform.position + (directionToSelf * 3f);

            // Save partner and enemy
            DontDestroyOnLoad(GameObject.FindWithTag("CaughtPokemon"));
            DontDestroyOnLoad(other);

            //Start Battle
            GameManager.Instance.EventSender.EnterBattleEventSend();
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            isTouchingItem = collision.transform.GetComponent<ItemPickup_Overworld>();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.CompareTag("Item"))
        {
            isTouchingItem = null;
        }
    }

    public ItemPickup_Overworld GetTouchedItem()
    {
        return isTouchingItem;
    }
}
