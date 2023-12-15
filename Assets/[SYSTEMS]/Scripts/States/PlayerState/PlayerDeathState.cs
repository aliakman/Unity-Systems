using Helpers;

namespace States.PlayerStates
{
    public class PlayerDeathState : BaseState<Enums.PlayerStates>
    {
        public PlayerDeathState(Enums.PlayerStates _key, StateHandler<Enums.PlayerStates> _stateHandler) : base(_key, _stateHandler) { }
    
    }
}