using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private PoolBase<PoolItem> poolBase;

    public Dictionary<string, PoolBase<PoolItem>> pools = new Dictionary<string, PoolBase<PoolItem>>();

    [SerializeField] private List<PoolItem> poolItems = new List<PoolItem>();
    [SerializeField] private List<string> poolKeys = new List<string>();
    [SerializeField] private Transform[] parents;

    private void Awake()
    {
        InitPool();
    }

    private void OnEnable()
    {
        EventManager.Scripts.PoolManager += () => this;
    }
    private void OnDisable()
    {
        EventManager.Scripts.PoolManager -= () => this;
    }

    private void InitPool()
    {
        for (int i = 0; i < poolItems.Count; i++)
            MakePool(poolKeys[i], 5, true, 5, false, new PoolObject<PoolItem>(poolItems[i], poolKeys[i], parents[i + 1]));
    }

    public void MakePool(string _key, int _initCount, bool _isExpandable, int _expandCount, bool _isActive, params PoolObject<PoolItem>[] _poolObjects)
    {
        poolBase = new PoolBase<PoolItem>(_initCount, _isExpandable, _expandCount, _isActive, _poolObjects);

        pools.Add(_key, poolBase);

    }

    public PoolItem GetPoolItem(PoolItem _poolItem)
    {
        if (poolKeys.Contains(_poolItem.key))
            return pools[_poolItem.key].GetTFromPool(_poolItem);
        else
        {
            poolItems.Add(_poolItem);
            poolKeys.Add(_poolItem.key);
            MakePool(poolKeys[poolKeys.Count - 1], 5, true, 5, false, new PoolObject<PoolItem>(poolItems[poolItems.Count - 1], poolKeys[poolKeys.Count - 1], parents[0]));

            return pools[_poolItem.key].GetTFromPool(_poolItem);
        }

    }

}
