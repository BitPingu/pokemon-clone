
public class StateBase
{
    protected CreatureBase creature;
    protected StateMachine stateMachine;


    public StateBase(CreatureBase creature, StateMachine stateMachine)
    {
        this.creature = creature;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
}

