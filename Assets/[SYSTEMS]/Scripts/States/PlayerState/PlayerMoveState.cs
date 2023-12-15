using Helpers;

namespace States.PlayerStates
{
    public class PlayerMoveState : BaseState<Enums.PlayerStates>
    {
        public PlayerMoveState(Enums.PlayerStates _key, StateHandler<Enums.PlayerStates> _stateHandler) : base(_key, _stateHandler) { }

    }
}