using UnityEngine;

public class CreatureBase : MonoBehaviour
{
    // Pokemon data
    public CreatureInstance Creature { get; set; }

    // states
    public StateMachine StateMachine { get; set; }
    public StateBase IdleState { get; set; }
    public StateBase BattleState { get; set; }

    // conditions
    public bool IsBattling { get; set; }
    public bool IsTurn { get; set; }

    protected virtual void Start()
    {
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

    public virtual void Battle()
    {
        if (!IsTurn) return;

        if (gameObject.CompareTag("CaughtPokemon"))
        {
            // player creature's turn
            BattleManager.Instance.PlayerTurn(this);
        }
        else
        {
            // enemy creature turn
            BattleManager.Instance.Attack(this);
        }
    }
}

