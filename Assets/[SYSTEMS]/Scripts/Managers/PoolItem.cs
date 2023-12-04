using UnityEngine;
public abstract class PoolItem : MonoBehaviour 
{
    public string key;

    public virtual void Init(Vector3 _initPos, Vector3 _lookPos, float _bulletSpeed) { }

}