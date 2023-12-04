using UnityEngine;
using Helpers;

namespace Behaviours
{
    public class JumpState : BaseState //Space-Mouse[Look]
    {
        public JumpState(StateHandler _stateHandler) : base(_stateHandler) { }


        public override void EnterState()
        {
            Debug.Log("Jump enter");
            stateHandler.mainState = Enums.BehaviourStates.Jump;
        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {
            Debug.Log("Jump exit");
        }

    }
}
