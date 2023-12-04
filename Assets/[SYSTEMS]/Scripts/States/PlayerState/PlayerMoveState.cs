using UnityEngine;
using State;
using Helpers;

public class PlayerMoveState : BaseState<Enums.PlayerStates>
{
    public PlayerMoveState(Enums.PlayerStates _key, StateHandler<Enums.PlayerStates> _stateHandler) : base(_key, _stateHandler) { }


    public override void EnterState()
    {
        Debug.Log($"{StateKey} State Enter");
    }

    public override void ExitState()
    {
        Debug.Log($"{StateKey} State Exit");
    }

}
