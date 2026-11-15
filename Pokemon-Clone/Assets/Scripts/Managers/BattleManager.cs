using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private CharacterBase player, enemy;

    private void Awake()
    {
        //Singleton pattern - make sure only 1 Battle Manager is present at a time
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
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

    public void End()
    {
        // remove enemy
        // Moves the GameObject from the DontDestroyOnLoad scene back to the current active scene
        SceneManager.MoveGameObjectToScene(enemy.gameObject, SceneManager.GetActiveScene());

        // reset battle vars

        // Back to idle state
        player.IsBattling = false;

        // End Battle
        GameManager.Instance.EventSender.ExitBattleEventSend();
    }
}
