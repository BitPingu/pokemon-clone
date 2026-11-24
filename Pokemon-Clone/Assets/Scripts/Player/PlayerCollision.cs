using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is on the Player Prefab.
/// It handles all collision detection with other objects
/// </summary>

public class PlayerCollision : MonoBehaviour
{
    public Vector3 PlayerSavedPos { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WildPokemon") && other.GetComponent<EnemyBase>())
        {
            // Get enemy
            EnemyBase enemy = other.GetComponent<EnemyBase>();

            // Get the normalized direction from the enemy back to player
            Vector3 directionToSelf = (transform.position - enemy.transform.position).normalized;

            // Save player position away from enemy for reload
            PlayerSavedPos = enemy.transform.position + (directionToSelf * 3f);

            // Save enemy
            DontDestroyOnLoad(enemy);

            //Start Battle
            GameManager.Instance.EventSender.EnterBattleEventSend();
        }
    }
}
