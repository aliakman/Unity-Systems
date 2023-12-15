using Helpers;

namespace States.PlayerStates
{
    public class PlayerAttackState : BaseState<Enums.PlayerStates>
    {
        public PlayerAttackState(Enums.PlayerStates _key, StateHandler<Enums.PlayerStates> _stateHandler) : base(_key, _stateHandler) { }

    }
}