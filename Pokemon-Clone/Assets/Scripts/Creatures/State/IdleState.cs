using UnityEngine;

public class IdleState : StateBase
{
    // pass in any parameters you need in the constructors
    public IdleState(CreatureBase creature, StateMachine stateMachine) : base(creature, stateMachine)
    {
        
    }

    // code that runs when we first enter the state
    public override void EnterState()
    {
        base.EnterState();

        creature.StartIdle();

        // Debug.Log(creature.name + " is idle.");
    }

    // code that runs when we exit the state
    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // idle
        creature.Idle();

        // battle opponent
        if (creature.IsBattling)
            creature.StateMachine.ChangeState(creature.BattleState);
    }
}

