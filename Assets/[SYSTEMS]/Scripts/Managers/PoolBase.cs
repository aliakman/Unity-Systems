using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class PoolObject<T>
{
    public T poolObject;
    public string poolKey;
    public Transform parentTransform;

    public PoolObject(T _poolObject, string _poolKey, Transform _parentTransform)
    {
        poolObject = _poolObject;
        poolKey = _poolKey;
        parentTransform = _parentTransform;
    }

}

[System.Serializable]
public class PoolBase<T> where T : MonoBehaviour
{
    public int poolCount;
    private int initCount = 5;
    private int expandCount = 5;

    private string canUsableKey = "c";
    private string onUsageKey = "o";

    private bool isExpandable = true;

    private Dictionary<string, LinkedList<T>> poolDictionary = new Dictionary<string, LinkedList<T>>();

    [SerializeField] private List<PoolObject<T>> poolObjects = new List<PoolObject<T>>();

    #region
    //private PoolBase(int _initCount = 5, bool _isExpandable = true, int _expandCount = 5)
    //{
    //    initCount = _initCount;
    //    poolCount = initCount;
    //    isExpandable = _isExpandable;
    //    expandCount = _expandCount;

    //    foreach (var item in poolObjects)
    //    {
    //        LinkedList<T> _canUsableList = new LinkedList<T>();
    //        LinkedList<T> _onUsageList = new LinkedList<T>();

    //        for (int i = 0; i < initCount; i++)
    //            _canUsableList.AddLast(Object.Instantiate(item.poolObject, item.parentTransform));

    //        poolDictionary.Add(item.poolKey + onUsageKey, _onUsageList);
    //        poolDictionary.Add(item.poolKey + canUsableKey, _canUsableList);
    //    }

    //}
    #endregion

    public PoolBase(int _initCount, bool _isExpandable, int _expandCount, bool _isActive, params PoolObject<T>[] _poolObjects)
    {
        initCount = _initCount;
        poolCount = initCount;
        isExpandable = _isExpandable;
        expandCount = _expandCount;

        foreach (var item in _poolObjects)
        {
            poolObjects.Add(item);

            LinkedList<T> _canUsableList = new LinkedList<T>();
            LinkedList<T> _onUsageList = new LinkedList<T>();

            for (int i = 0; i < initCount; i++)
            {
                if(_isActive)
                    _canUsableList.AddLast(Object.Instantiate(item.poolObject, item.parentTransform));
                else
                {
                    T _t = Object.Instantiate(item.poolObject, item.parentTransform);
                    _t.gameObject.SetActive(false);
                    _canUsableList.AddLast(_t);
                }
            }

            poolDictionary.Add(item.poolKey + onUsageKey, _onUsageList);
            poolDictionary.Add(item.poolKey + canUsableKey, _canUsableList);
        }

    }


    public T GetTFromPool(T _poolObject)
    {
        string _key = GetKey(_poolObject);

        CheckPoolIsAvailable(_key);

        T _tObject = poolDictionary[_key + canUsableKey].Last.Value;
        poolDictionary[_key + onUsageKey].AddFirst(_tObject);
        poolDictionary[_key + canUsableKey].RemoveLast();

        if(!_tObject.gameObject.activeSelf)
            _tObject.gameObject.SetActive(true);

        return _tObject;

    }

    public void TToPool(T _poolObject)
    {
        string _key = GetKey(_poolObject);

        _poolObject.gameObject.SetActive(false);

        poolDictionary[_key + canUsableKey].AddFirst(_poolObject);
        poolDictionary[_key + onUsageKey].Remove(_poolObject);

    }

    private bool CheckPoolIsAvailable(string _key)
    {
        if (poolDictionary[_key + canUsableKey].Count == 0)
        {
            if (isExpandable)
            {
                ExpandPool(_key);
                return true;
            }
            else
                return false;

        }
        else
            return true;

    }

    private void ExpandPool(string _key)
    {
        for (int i = 0; i < expandCount; i++)
            poolDictionary[_key + canUsableKey].AddLast(Object.Instantiate(poolDictionary[_key + onUsageKey].First.Value, poolDictionary[_key + onUsageKey].First.Value.transform.parent));

        poolCount += expandCount;

    }

    private string GetKey(T _poolObject)
    {
        return poolObjects.Find(x => x.poolObject.GetType() == _poolObject.GetType()).poolKey;
    }

}
