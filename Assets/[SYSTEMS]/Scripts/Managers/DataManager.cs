using UnityEngine;

public class DataManager : MonoBehaviour
{
    public DataSO data;

    private void OnEnable() => EventManager.Scripts.DataManager += () => this;
    private void OnDisable() => EventManager.Scripts.DataManager -= () => this;

}
