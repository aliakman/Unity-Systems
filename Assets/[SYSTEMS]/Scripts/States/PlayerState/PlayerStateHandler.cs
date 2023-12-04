using State;
using Helpers;

public class PlayerStateHandler : StateHandler<Enums.PlayerStates>
{
    private PlayerMoveState _moveState;
    public PlayerMoveState moveState { get { return _moveState != null ? _moveState : _moveState = new PlayerMoveState(Enums.PlayerStates.Move, this); } }

    private PlayerAttackState _attackState;
    public PlayerAttackState attackState { get { return _attackState != null ? _attackState : _attackState = new PlayerAttackState(Enums.PlayerStates.Attack, this); } }

    private PlayerDeathState _deatState;
    public PlayerDeathState deatState { get { return _deatState != null ? _deatState : _deatState = new PlayerDeathState(Enums.PlayerStates.Death, this); } }


    private void Start()
    {
        InitStateHandler();
    }

    private void InitStateHandler()
    {
        States.Add(moveState.StateKey, moveState);
        States.Add(attackState.StateKey, attackState);
        States.Add(deatState.StateKey, deatState);
    }

}
