using System;

namespace State
{
    public abstract class BaseState<T> where T : Enum
    {
        public BaseState(T _key, StateHandler<T> _stateHandler) 
        { 
            StateKey = _key;
            StateHandler = _stateHandler;
        }

        protected StateHandler<T> StateHandler { get; private set; }
        public T StateKey { get; private set; }

        public virtual void EnterState() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void ExitState() { }

    }
}