using UnityEngine;

public class BattleState : StateBase
{
    // pass in any parameters you need in the constructors
    public BattleState(CreatureBase creature, StateMachine stateMachine) : base(creature, stateMachine)
    {

    }

    // code that runs when we first enter the state
    public override void EnterState()
    {
        base.EnterState();

        creature.StartBattle();

        // Debug.Log(creature.name + " is battling.");
    }

    // code that runs when we exit the state
    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // battle
        if (creature.IsBattling)
            creature.Battle();

        // exit battle
        if (!creature.IsBattling)
            creature.StateMachine.ChangeState(creature.IdleState);

        // death
        if (creature.Creature.CurrentHealth <= 0)
            creature.StateMachine.End();
    }
}

