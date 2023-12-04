using UnityEngine;
using Helpers;

namespace Behaviours
{
    public class AttackState : BaseState
    {
        public AttackState(StateHandler _stateHandler) : base(_stateHandler) { }


        public override void EnterState()
        {
            Debug.Log(stateHandler.name + " Attack enter.");
            stateHandler.mainState = Enums.BehaviourStates.Attack;

        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {
            Debug.Log(stateHandler.name + " Attack exit.");
        }
    }
}
