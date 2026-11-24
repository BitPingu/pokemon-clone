using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private PlayerBase p;
    private CreatureBase player, enemy;

    // player input
    private PlayerInputHandler inputManager;
    private bool _prompt;

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

    // called at the start of battle (when the battle scene loads)
    private void Start()
    {
        // get the player
        p = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();

        // player input
        inputManager = p.GetComponentInChildren<PlayerInputHandler>();

        // spawn the player creature
        CreatureInstance creature = p.GetFirstHealthyCreature();
        p.SpawnCreature(creature);

        // get the player and enemy creatures
        player = GameObject.FindWithTag("CaughtPokemon").GetComponent<CreatureBase>();
        enemy = GameObject.FindWithTag("WildPokemon").GetComponent<CreatureBase>();

        // set battle positions
        player.transform.position = new Vector3(-3, 0, -3);
        enemy.transform.position = new Vector3(6, 0, 2);

        // start battle (transition state)
        player.GetComponent<CreatureBase>().IsBattling = true;
        enemy.GetComponent<CreatureBase>().IsBattling = true;

        Debug.Log("A wild " + enemy.Creature.Name + " appeared!");
        Debug.Log("Go " + creature.Name + "!");

        Debug.Log(player.Creature.Name + ": " + player.Creature.CurrentHealth + "/" + player.Creature.MaxHealth
            + " | " + enemy.Creature.Name + ": " + enemy.Creature.CurrentHealth + "/" + enemy.Creature.MaxHealth);

        // player always goes first (for now)
        player.GetComponent<CreatureBase>().IsTurn = true;
    }

    // battle ends (player or enemy faints, or run)
    public void End()
    {
        // Move this enemy to the scene to destroy it
        // (Currently enemies are always active in the overworld)
        SceneManager.MoveGameObjectToScene(enemy.gameObject, SceneManager.GetActiveScene());

        // Back to idle state
        player.IsBattling = false;

        // Return to overworld
        GameManager.Instance.EventSender.ExitBattleEventSend();
    }

    // pass the turn to the opponent
    public void PassTurn(CreatureBase passer)
    {
        Debug.Log(player.Creature.Name + ": " + player.Creature.CurrentHealth + "/" + player.Creature.MaxHealth
            + " | " + enemy.Creature.Name + ": " + enemy.Creature.CurrentHealth + "/" + enemy.Creature.MaxHealth);

        if (passer.gameObject.CompareTag("CaughtPokemon"))
        {
            // now enemy's turn
            player.IsTurn = false;
            enemy.IsTurn = true;
        }
        else if (passer.gameObject.CompareTag("WildPokemon"))
        {
            // now player's turn
            enemy.IsTurn = false;
            player.IsTurn = true;
        }
    }

    // player's turn
    public void PlayerTurn(CreatureBase attacker)
    {
        // battle prompt
        if (!_prompt)
        {
            Debug.Log("What will " + attacker.Creature.Name + " do?");
            Debug.Log("F: Fight | C: Catch | R: Run");
            _prompt = true;
        }

        if (inputManager.PressedFightThisFrame())
        {
            // Fight
            Attack(attacker);
            _prompt = false;
        }

        if (inputManager.PressedCatchThisFrame())
        {
            Debug.Log("Player used a Pokeball!");
            Debug.Log("*wobble*");
            Debug.Log("*wobble*");
            // Catch
            if (Random.Range(0, 2) == 0)
            {
                // Success
                Caught();
            }
            else
            {
                Debug.Log("Aww! It appeared to be caught!");

                // Pass turn
                PassTurn(attacker);
            }
            _prompt = false;
        }

        if (inputManager.PressedRunThisFrame())
        {
            // Run
            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("Got away safely!");

                // End Battle
                End();
            }
            else
            {
                Debug.Log("Couldn't get away!");

                // Pass turn
                PassTurn(attacker);
            }
            _prompt = false;
        }
    }

    // attack the opponent
    public void Attack(CreatureBase attacker)
    {
        CreatureBase receiver = null;

        if (attacker.gameObject.CompareTag("CaughtPokemon"))
        {
            receiver = enemy;
        }
        else if (attacker.gameObject.CompareTag("WildPokemon"))
        {
            receiver = player;
        }

        Debug.Log(attacker.Creature.Name + " attacks!");

        // Deal damage based on strength (atk) stat
        int amount = attacker.Creature.Strength;

        receiver.Creature.Damage(amount);

        if (receiver.Creature.CurrentHealth > 0)
        {
            // Pass turn
            PassTurn(attacker);
        }
        else
        {
            Debug.Log(attacker.Creature.Name + " defeated " + receiver.Creature.Name + "!");
            
            if (attacker.CompareTag("CaughtPokemon"))
            {
                // End Battle
                End();
            }
            else if (attacker.CompareTag("WildPokemon"))
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

    // catch the opponent
    public void Caught()
    {
        Debug.Log("Gotcha! " + enemy.Creature.Name + " was caught!");

        // Add to party
        p.AddCreature(enemy.Creature.CreatureData);

        // End Battle
        End();
    }
}
