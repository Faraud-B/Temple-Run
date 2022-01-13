using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject uiDrag;
    public GameObject uiPoint;
    public GameObject uiCoin;
    
    public GameObject uiDeath;
    public GameObject uiRecord;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        uiDrag.SetActive(true);
        uiPoint.SetActive(true);
        uiCoin.SetActive(true);
        uiDeath.SetActive(false);
        uiRecord.SetActive(false);
    }

    public void SetDrag()
    {
        uiDrag.SetActive(true);
        uiPoint.SetActive(true);
        uiCoin.SetActive(true);
        uiDeath.SetActive(false);
        uiRecord.SetActive(false);
    }

    public void SetDeath()
    {
        uiDrag.SetActive(false);
        uiPoint.SetActive(true);
        uiCoin.SetActive(true);
        uiDeath.SetActive(true);
        uiRecord.SetActive(true);
    }

    public void ButtonRestart()
    {
        GameController.Instance.Restart();
    }

    public void SetPoint(int nb)
    {
        uiPoint.transform.GetChild(0).GetComponent<Text>().text = nb.ToString();
    }

    public void SetRecord(int nb)
    {
        uiRecord.transform.GetChild(0).GetComponent<Text>().text = "Record : " + nb.ToString();
    }

    public void SetCoin(int nb)
    {
        uiCoin.transform.GetChild(0).GetComponent<Text>().text = nb.ToString();
    }
}
