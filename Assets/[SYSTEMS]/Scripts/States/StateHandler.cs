using System;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public partial class StateHandler<T> : MonoBehaviour where T : Enum
    {
        protected Dictionary<T, BaseState<T>> StateDictionary = new();

        public T mainState;
        public T subState;

        public BaseState<T> currentMainState;
        public BaseState<T> currentSubState;

        public BaseState<T> formerMainState;
        public BaseState<T> formerSubState;

        protected bool isTransitioningMainState = false;
        protected bool isTransitioningSubState = false;

        private void Update()
        {
            if (!isTransitioningMainState && currentMainState != null)
                currentMainState.UpdateState();

            if (!isTransitioningSubState && currentSubState != null)
                currentSubState.UpdateState();
        }

        private void FixedUpdate()
        {
            if (!isTransitioningMainState && currentMainState != null)
                currentMainState.FixedUpdateState();

            if (!isTransitioningSubState && currentSubState != null)
                currentSubState.FixedUpdateState();
        }

        public void ChangeMainState(T _stateKey)
        {
            isTransitioningMainState = true;

            if (currentMainState != null)
            {
                currentMainState.ExitState();
                formerMainState = currentMainState;
                Debug.Log($"MAIN {currentMainState.stateKey} STATE EXIT  :  {gameObject.name}");
            }
            else
                formerMainState = null;

            if (StateDictionary.ContainsKey(_stateKey))
            {
                currentMainState = StateDictionary[_stateKey];
                currentMainState.EnterState();
                mainState = _stateKey;
                Debug.Log($"MAIN {_stateKey} STATE ENTER  :  {gameObject.name}");
            }
            else
                currentMainState = null;

            isTransitioningMainState = false;
        }

        public void ChangeSubState(T _stateKey)
        {
            isTransitioningSubState = true;

            if (currentSubState != null)
            {
                currentSubState.ExitState();
                formerSubState = currentSubState;
                Debug.Log($"SUB {currentSubState.stateKey} STATE EXIT  :  {gameObject.name}");
            }
            else
                formerSubState = null;

            if (StateDictionary.ContainsKey(_stateKey))
            {
                currentSubState = StateDictionary[_stateKey];
                currentSubState.EnterState();
                subState = _stateKey;
                Debug.Log($"SUB {_stateKey} STATE ENTER  :  {gameObject.name}");
            }
            else
                currentSubState = null;

            isTransitioningSubState = false;
        }

        public virtual void SetMainStateNone()
        {
            isTransitioningMainState = true;

            if (currentMainState == null) return;

            currentMainState.ExitState();
            currentMainState = null;

            Debug.Log($"MAIN STATE NONE  :  {gameObject.name}");
        }

        public virtual void SetSubStateNone()
        {
            isTransitioningSubState = true;

            if (currentSubState == null) return;

            currentSubState.ExitState();
            currentSubState = null;
            Debug.Log($"SUB STATE NONE  :  {gameObject.name}");
        }

    }
}