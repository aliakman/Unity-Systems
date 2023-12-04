namespace Helpers
{
    public struct Enums
    {
        public enum InputActionMaps
        {
            None,
            Player,
            UI,
        }

        public enum BehaviourStates
        {
            /* Common */
            None,
            Move,
            Death,
            TakeRage,
            /* Player */
            BasicAttack,
            AdvancedAttack,
            Jump,
            Dash,
            Repulse,
            DeadEye,
            /* Enemy */
            Patrol,
            Idle,
            Attack,
            Defence,
        }

    }
}
