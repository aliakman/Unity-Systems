using Helpers;
using UnityEngine;

namespace Behaviours
{
    public partial class StateHandler : MonoBehaviour
    {
        public Enums.BehaviourStates mainState;
        public Enums.BehaviourStates subState;

        public BaseState currentMainState;
        public BaseState currentSubState;

        #region CommonStates
        private MoveState _moveState;
        public MoveState moveState { get { return _moveState != null ? _moveState : _moveState = new MoveState(this); } }
        #endregion

        #region PlayerStates
        #region MainStates
        private JumpState _jumpState;
        public JumpState jumpState { get { return _jumpState != null ? _jumpState : _jumpState = new JumpState(this); } }

        private DashState _dashState;
        public DashState dashState { get { return _dashState != null ? _dashState : _dashState = new DashState(this); } }
        #endregion

        #region SubStates
        #endregion
        #endregion

        #region EnemyStates
        #region MainStates
        private PatrolState _patrolState;
        public PatrolState patrolState { get { return _patrolState != null ? _patrolState : _patrolState = new PatrolState(this); } }

        private AttackState _attackState;
        public AttackState attackState { get { return _attackState != null ? _attackState : _attackState = new AttackState(this); } }
        #endregion

        #region SubStates
        private IdleState _idleState;
        public IdleState idleState { get { return _idleState != null ? _idleState : _idleState = new IdleState(this); } }
        #endregion
        #endregion

        private void Update()
        {
            if (currentMainState != null)
                currentMainState.UpdateState();

            if (currentSubState != null)
                currentSubState.UpdateState();
        }

        private void FixedUpdate()
        {
            if (currentMainState != null)
                currentMainState.FixedUpdateState();

            if (currentSubState != null)
                currentSubState.FixedUpdateState();
        }

        public void ChangeMainState(Enums.BehaviourStates _nextState)
        {
            if (currentMainState != null) currentMainState.ExitState();

            switch (_nextState)
            {
                case Enums.BehaviourStates.Move:
                    currentMainState = moveState;
                    break;
                case Enums.BehaviourStates.Jump:
                    currentMainState = jumpState;
                    ChangeSubState(Enums.BehaviourStates.None);
                    break;
                case Enums.BehaviourStates.Dash:
                    currentMainState = dashState;
                    break;
            }

            if (currentMainState != null)
                currentMainState.EnterState();

        }

        public void ChangeSubState(Enums.BehaviourStates _nextState)
        {
            if (currentSubState != null) currentSubState.ExitState();

            switch (_nextState)
            {
                case Enums.BehaviourStates.None:
                    currentSubState = null;
                    Debug.Log("Sub state is null.");
                    break;
            }

            if (currentSubState != null)
                currentSubState.EnterState();

            if (_nextState == Enums.BehaviourStates.None)
                subState = Enums.BehaviourStates.None;

        }

    }
}
