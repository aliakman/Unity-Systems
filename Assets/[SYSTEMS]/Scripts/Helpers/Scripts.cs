namespace Helpers
{
    public static class Scripts
    {
        private static PoolManager _poolManager;
        public static PoolManager PoolManager() { return _poolManager ? _poolManager : _poolManager = EventManager.Scripts.PoolManager?.Invoke(); }
    }

}