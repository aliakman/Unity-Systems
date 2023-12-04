using System;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class StateHandler<T> : MonoBehaviour where T : Enum
    {
        protected Dictionary<T, BaseState<T>> States = new();
        
        protected BaseState<T> CurrentMainState;
        protected BaseState<T> CurrentSubState;

        protected bool isTransitioningMainState = false;
        protected bool isTransitioningSubState = false;


        private void Update()
        {
            if (!isTransitioningMainState && CurrentMainState != null)
                CurrentMainState.UpdateState();

            if (!isTransitioningSubState && CurrentSubState != null)
                CurrentSubState.UpdateState();
        }

        private void FixedUpdate()
        {
            if (!isTransitioningMainState && CurrentMainState != null)
                CurrentMainState.FixedUpdateState();

            if (!isTransitioningSubState && CurrentSubState != null)
                CurrentSubState.FixedUpdateState();
        }

        public void ChangeMainState(T _stateKey)
        {
            isTransitioningMainState = true;

            if(CurrentMainState != null)
                CurrentMainState.ExitState();

            if (States.ContainsKey(_stateKey))
            {
                CurrentMainState = States[_stateKey];
                CurrentMainState.EnterState();
            }
            else
                CurrentMainState = null;

            isTransitioningMainState = false;
        }

        public void ChangeSubState(T _stateKey)
        {
            isTransitioningSubState = true;

            if (CurrentSubState != null)
                CurrentSubState.ExitState();

            if (States.ContainsKey(_stateKey))
            {
                CurrentSubState = States[_stateKey];
                CurrentSubState.EnterState();
            }
            else
                CurrentSubState = null;

            isTransitioningSubState = false;
        }



    }
}