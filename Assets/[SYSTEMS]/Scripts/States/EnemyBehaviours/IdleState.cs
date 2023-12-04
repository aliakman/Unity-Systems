using UnityEngine;
using Helpers;

namespace Behaviours
{
    public class IdleState : BaseState
    {
        public IdleState(StateHandler _stateHandler) : base(_stateHandler) { }


        public override void EnterState()
        {
            Debug.Log(stateHandler.name + " Idle enter.");
            stateHandler.subState = Enums.BehaviourStates.Idle;
        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {
            Debug.Log(stateHandler.name + " Idle exit.");
        }
    }
}
