using UnityEngine;
using Helpers;

namespace Behaviours
{
    public class PatrolState : BaseState
    {
        public PatrolState(StateHandler _stateHandler) : base(_stateHandler) { }


        public Transform[] patrolPoints;

        public bool isSafe;

        public override void EnterState()
        {
            Debug.Log("Patrol enter.");
            stateHandler.mainState = Enums.BehaviourStates.Patrol;

        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {
            Debug.Log("Patrol exit.");
        }
    }
}
