using UnityEngine;

public class BattleState : StateBase
{
    // pass in any parameters you need in the constructors
    public BattleState(CharacterBase character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }

    // code that runs when we first enter the state
    public override void EnterState()
    {
        base.EnterState();
        
        Debug.Log(character.name + " is battling.");
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
        if (character.IsBattling)
            character.Battle();

        // exit battle
        if (!character.IsBattling)
            character.StateMachine.ChangeState(character.IdleState);

        // death
        if (character.CurrentHealth <= 0)
            character.StateMachine.End();
    }
}

