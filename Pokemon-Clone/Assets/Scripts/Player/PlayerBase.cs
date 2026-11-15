using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    // Singleton instance
    public static PlayerBase Instance { get; private set; }

    // battle vars
    private bool _prompt;

    private PlayerInputHandler inputManager;

    protected virtual void Awake()
    {
        // Make sure only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();

        inputManager = GetComponentInChildren<PlayerInputHandler>();
    }

    public override void StartIdle()
    {
        base.StartIdle();

        // anything here is called when entering the idle state

        // player can move in idle state
        GetComponent<PlayerMovement>().enabled = true;
    }

    public override void Idle()
    {
        base.Idle();

        // anything here is called during the idle state through update()
        
        // move player movement code here?
    }

    public override void StartBattle()
    {
        base.StartBattle();

        // anything here is called when entering the battle state

        // player cannot move in battle state
        GetComponent<PlayerMovement>().enabled = false;

        // stop movement
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void Battle()
    {
        base.Battle();

        // anything here is called during the battle state through update()

        if (IsTurn)
        {
            if (!_prompt)
            {
                // battle prompt
                Debug.Log("It is the " + name + "'s turn");
                Debug.Log("What will you do?");
                Debug.Log("F: Fight | R: Run");
                _prompt = true;
            }

            if (inputManager.PressedFightThisFrame())
            {
                // Fight
                BattleManager.Instance.Attack(this);
                _prompt = false;
            }

            if (inputManager.PressedRunThisFrame())
            {
                // Run
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    Debug.Log("Got away safely!");

                    // End Battle
                    BattleManager.Instance.End();
                }
                else
                {
                    Debug.Log("Couldn't get away!");

                    // Pass turn
                    BattleManager.Instance.PassTurn(this);
                }
                _prompt = false;
            }
        }
    }
}
