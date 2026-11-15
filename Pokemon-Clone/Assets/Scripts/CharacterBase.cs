using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    // states
    public StateMachine StateMachine { get; set; }
    public StateBase IdleState { get; set; }
    public StateBase BattleState { get; set; }

    // conditions
    public bool IsBattling { get; set; }

    // battle stats
    public int Level = 1;
    public int MaxHealth = 20;
    public int CurrentHealth { get; set; }
    public int Strength = 4;
    public int Defence = 2;
    public int CurrentDefence { get; set; }
    public int RewardExp = 0;

    // Singleton instance
    public static CharacterBase Instance { get; private set; }

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

    protected virtual void Start()
    {
        // set parameters
        CurrentHealth = MaxHealth;
        CurrentDefence = Defence;

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

    public virtual void Idle() {}

    public virtual void Battle() { }
}

