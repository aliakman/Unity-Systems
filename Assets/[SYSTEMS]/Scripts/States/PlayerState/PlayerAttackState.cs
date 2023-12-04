using UnityEngine;
using State;
using Helpers;

public class PlayerAttackState : BaseState<Enums.PlayerStates>
{
    public PlayerAttackState(Enums.PlayerStates _key, StateHandler<Enums.PlayerStates> _stateHandler) : base(_key, _stateHandler) { }


    public override void EnterState()
    {
        Debug.Log($"{StateKey} State Enter");
    }

    public override void ExitState()
    {
        Debug.Log($"{StateKey} State Exit");
    }

}
