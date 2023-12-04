using UnityEngine;
using State;
using Helpers;

public class UIMenuState : BaseState<Enums.UIStates>
{
    public UIMenuState(Enums.UIStates _key, StateHandler<Enums.UIStates> _stateHandler) : base(_key, _stateHandler) { }


    public override void EnterState()
    {
        Debug.Log($"{StateKey} State Enter");
    }

    public override void ExitState()
    {
        Debug.Log($"{StateKey} State Exit");
    }
}
