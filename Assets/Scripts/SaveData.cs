using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    private string coinName = "coin";
    private string recordName = "record";

    private void Awake()
    {
        Instance = this;
    }

    public void SaveCoin(int nb)
    {
        PlayerPrefs.SetInt(coinName, nb);
    }

    public int GetCoin()
    {
        return PlayerPrefs.GetInt(coinName);
    }

    public void SaveRecord(int nb)
    {
        PlayerPrefs.SetInt(recordName, nb);
    }

    public int GetRecord()
    {
        return PlayerPrefs.GetInt(recordName);
    }
}
