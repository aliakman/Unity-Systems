using UnityEngine;

public class SaveManager
{
    public static void SaveData(DataSO _data)
    {
        string _dataString = JsonUtility.ToJson(_data);
        PlayerPrefs.SetString("data", _dataString);
    }

    public static void LoadData(DataSO _data)
    {
        if (!PlayerPrefs.HasKey("data"))
        {
            SaveData(_data);
            return;
        }

        string _dataString = PlayerPrefs.GetString("data");
        JsonUtility.FromJsonOverwrite(_dataString, _data);
    }
}
