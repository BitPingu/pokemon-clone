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

        // battle positions
        player.transform.position = new Vector3(-3, 0, -3);
        enemy.transform.position = new Vector3(6, 0, 2);
    }
}
