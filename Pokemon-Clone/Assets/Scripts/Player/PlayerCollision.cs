using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is on the Player Prefab.
/// It handles all collision detection with other objects
/// </summary>

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WildPokemon"))
        {
            //Start Battle
            GameManager.Instance.EventSender.EnterBattleEventSend();
        }
    }
}
