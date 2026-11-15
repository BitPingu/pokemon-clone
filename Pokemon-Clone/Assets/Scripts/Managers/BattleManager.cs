using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private CharacterBase player, enemy;

    private void Awake()
    {
        // get the player and enemy
        player = FindObjectOfType<PlayerBase>();
        enemy = FindObjectOfType<EnemyBase>();

        // set battle positions
        player.transform.position = new Vector3(-3, 0, -3);
        enemy.transform.position = new Vector3(6, 0, 2);

        // start battle (transition state)
        player.IsBattling = true;
        enemy.IsBattling = true;

        // player always goes first (for now)
        player.IsTurn = true;
    }
}
