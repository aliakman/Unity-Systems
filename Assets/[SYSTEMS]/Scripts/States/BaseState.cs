using System;

namespace States
{
    public abstract class BaseState<T> where T : Enum
    {
        public BaseState(T _key, StateHandler<T> _stateHandler)
        {
            stateKey = _key;
            stateHandler = _stateHandler;
        }

        public T stateKey;
        protected StateHandler<T> stateHandler;

        public virtual void EnterState() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void ExitState() { }

    }
}