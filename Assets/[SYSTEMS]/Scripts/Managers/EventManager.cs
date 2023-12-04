using System;

public static class EventManager
{
    public static ScriptHolder Scripts = new ScriptHolder();

}

public struct ScriptHolder
{
    public Func<PoolManager> PoolManager;

}
