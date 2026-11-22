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

        Debug.Log("Player: " + player.CurrentHealth + "/" + player.MaxHealth
            + " | Enemy: " + enemy.CurrentHealth + "/" + enemy.MaxHealth);

        // player always goes first (for now)
        player.IsTurn = true;
    }

    public void End()
    {
        // Move this enemy to the scene to destroy it
        // (Currently enemies are always active in the overworld)
        SceneManager.MoveGameObjectToScene(enemy.gameObject, SceneManager.GetActiveScene());

        // reset battle vars

        // Back to idle state
        player.IsBattling = false;

        // Return to overworld
        GameManager.Instance.EventSender.ExitBattleEventSend();
    }

    public void PassTurn(CharacterBase passer)
    {
        Debug.Log("Player: " + player.CurrentHealth + "/" + player.MaxHealth
            + " | Enemy: " + enemy.CurrentHealth + "/" + enemy.MaxHealth);

        if (passer.Type == CharacterType.Player)
        {
            // now enemy's turn
            player.IsTurn = false;
            enemy.IsTurn = true;
        }
        else if (passer.Type == CharacterType.Enemy)
        {
            // now player's turn
            enemy.IsTurn = false;
            player.IsTurn = true;
        }
    }

    public void Attack(CharacterBase attacker)
    {
        CharacterBase receiver = null;

        if (attacker.Type == CharacterType.Player)
        {
            receiver = enemy;
        }
        else if (attacker.Type == CharacterType.Enemy)
        {
            receiver = player;
        }

        Debug.Log(attacker.name + " attacks!");

        // Deal damage based on strength (atk) stat
        int amount = attacker.Strength;

        receiver.Damage(amount);

        if (receiver.CurrentHealth > 0)
        {
            // Pass turn
            PassTurn(attacker);
        }
        else
        {
            Debug.Log(attacker.name + " defeated " + receiver.name + "!");
            
            if (attacker.Type == CharacterType.Player)
            {
                // End Battle
                End();
            }
            else if (attacker.Type == CharacterType.Enemy)
            {
                // Game over
                Debug.Log("game over");

                #if UNITY_EDITOR
                    // Exits Play Mode in the Unity Editor
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    // Closes the built application
                    Application.Quit();
                #endif
            }
        }
    }
}
