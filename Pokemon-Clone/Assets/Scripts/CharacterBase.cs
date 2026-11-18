using UnityEngine;

public enum CharacterType
{
    Player,
    Enemy
}

public class CharacterBase : MonoBehaviour
{
    public CharacterType Type;

    // states
    public StateMachine StateMachine { get; set; }
    public StateBase IdleState { get; set; }
    public StateBase BattleState { get; set; }

    // conditions
    public bool IsBattling { get; set; }
    public bool IsTurn { get; set; }

    // battle stats
    public int Level = 1;
    public int MaxHealth = 20;
    public int CurrentHealth { get; set; }
    public int Strength = 4;

    protected virtual void Start()
    {
        // set parameters
        CurrentHealth = MaxHealth;

        // state machine
        StateMachine = new StateMachine();

        // states
        IdleState = new IdleState(this, StateMachine);
        BattleState = new BattleState(this, StateMachine);

        // start in idle state
        StateMachine.Initialize(IdleState);
    }

    protected virtual void Update()
    {
        // run current state
        StateMachine.CurrentState?.FrameUpdate();
    }

    public void ChangeState(StateBase newState)
    {
        StateMachine.ChangeState(newState);
    }

    public virtual void StartIdle() { }

    public virtual void Idle() { }

    public virtual void StartBattle() { }

    public virtual void Battle() { }

    public virtual void Damage(int damageAmount)
    {
        // prevent negative
        int damageTaken = Mathf.Min(damageAmount, CurrentHealth);

        Debug.Log(name + " took " + damageTaken + " damage");

        // take damage
        CurrentHealth -= damageTaken;

        if (CurrentHealth <= 0)
        {
            Debug.Log(name + " fainted!");
            // state automatically ends
        }
    }
}

