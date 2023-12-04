namespace Behaviours
{
    public abstract class BaseState
    {
        public BaseState(StateHandler _stateHandler)
        {
            stateHandler = _stateHandler;
        }

        protected StateHandler stateHandler;

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void ExitState();
    }

}