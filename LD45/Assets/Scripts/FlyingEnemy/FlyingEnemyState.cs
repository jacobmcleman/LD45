

public enum FlyingEnemyState 
{
    Idle,
    // TODO: does this need to be here?
    Attacking,
    Retreating
}
public struct FlyingEnemyStateEvent
{
    public FlyingEnemyState nextState;
    public FlyingEnemyState prevState;
    
    // other stuff??/???/?
}