using Helpers;

namespace States.UIStates
{
    public class UIMenuState : BaseState<Enums.UIStates>
    {
        public UIMenuState(Enums.UIStates _key, StateHandler<Enums.UIStates> _stateHandler) : base(_key, _stateHandler) { }

    }
}