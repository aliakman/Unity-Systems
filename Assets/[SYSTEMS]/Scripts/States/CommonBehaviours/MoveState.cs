using UnityEngine;
using Helpers;

namespace Behaviours
{
    public class MoveState : BaseState
    {
        public MoveState(StateHandler _stateHandler) : base(_stateHandler) { }


        public override void EnterState()
        {
            Debug.Log(stateHandler.name + " Move enter.");
            stateHandler.mainState = Enums.BehaviourStates.Move;
        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {
            Debug.Log(stateHandler.name + " Move exit.");

        }
    }
}
