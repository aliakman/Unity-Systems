namespace Helpers
{
    public static class Scripts
    {
        private static PoolManager _poolManager;
        public static PoolManager PoolManager() { return _poolManager ? _poolManager : _poolManager = EventManager.Scripts.PoolManager?.Invoke(); }

        private static DataManager _dataManager;
        public static DataManager DataManager() { return _dataManager ? _dataManager : _dataManager = EventManager.Scripts.DataManager?.Invoke(); }
    }

}