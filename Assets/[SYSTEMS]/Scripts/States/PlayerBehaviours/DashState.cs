using UnityEngine;
using Helpers;

namespace Behaviours
{
    public class DashState : BaseState //V-Mouse[Look]
    {
        public DashState(StateHandler _stateHandler) : base(_stateHandler) { }


        public override void EnterState()
        {
            Debug.Log("Dash enter");

            stateHandler.mainState = Enums.BehaviourStates.Dash;
        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {
            Debug.Log("Dash exit");        
        }

    }
}
