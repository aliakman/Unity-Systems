using Helpers;

namespace States.PlayerStates
{
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
            StateDictionary.Add(moveState.stateKey, moveState);
            StateDictionary.Add(attackState.stateKey, attackState);
            StateDictionary.Add(deatState.stateKey, deatState);
        }

        public override void SetMainStateNone()
        {
            base.SetMainStateNone();
            mainState = Enums.PlayerStates.None;
            isTransitioningMainState = false;
        }

        public override void SetSubStateNone()
        {
            base.SetSubStateNone();
            subState = Enums.PlayerStates.None;
            isTransitioningSubState = false;
        }

    }
}