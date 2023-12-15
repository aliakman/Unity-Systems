using Helpers;

namespace States.UIStates
{
    public class UIGameState : BaseState<Enums.UIStates>
    {
        public UIGameState(Enums.UIStates _key, StateHandler<Enums.UIStates> _stateHandler) : base(_key, _stateHandler) { }

    }
}