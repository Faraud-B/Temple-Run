using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private UIController uiController;
    private SaveData saveData;

    public SpawnObstacles spawnObstacles;
    public SpawnEnvironment spawnEnv1;
    public SpawnEnvironment spawnEnv2;
    public SpawnCloud spawnCloud;

    public float speed = 15;

    private int nbPoints = 0;
    private int nbRecord = 0;
    private int nbCoins = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        uiController = UIController.Instance;
        saveData = SaveData.Instance;

        nbCoins = saveData.GetCoin();
        uiController.SetCoin(nbCoins);

        nbRecord = saveData.GetRecord();
        uiController.SetRecord(nbRecord);

        spawnObstacles.SetSpeed(speed);
        spawnObstacles.StartSpawn();

        spawnEnv1.SetSpeed(speed);
        spawnEnv1.StartSpawn();

        spawnEnv2.SetSpeed(speed);
        spawnEnv2.StartSpawn();

        spawnCloud.SetSpeed(speed);
        spawnCloud.StartSpawn();
    }

    public void Restart()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject go in obstacles)
        {
            go.SetActive(false);
        }

        GameObject[] env = GameObject.FindGameObjectsWithTag("Environment");
        foreach (GameObject go in env)
        {
            go.SetActive(false);
        }

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject go in coins)
        {
            go.SetActive(false);
        }

        nbPoints = 0;
        uiController.SetPoint(nbPoints);

        Time.timeScale = 1;

        uiController.SetDrag();
        PlayerController.Instance.ResetPosition();

        spawnObstacles.StartSpawn();
        spawnEnv1.StartSpawn();
        spawnEnv2.StartSpawn();
        spawnCloud.StartSpawn();
    }

    public void Death()
    {
        spawnObstacles.Stop();
        spawnEnv1.Stop();
        spawnEnv2.Stop();
        spawnCloud.Stop();

        if(nbPoints > nbRecord)
        {
            nbRecord = nbPoints;
            saveData.SaveRecord(nbRecord);
            uiController.SetRecord(nbRecord);
        }

        uiController.SetDeath();
        Time.timeScale = 0;
    }

    public void AddPoint(int nb)
    {
        nbPoints += nb;
        uiController.SetPoint(nbPoints);
    }

    public void AddCoin()
    {
        nbCoins += 1;
        uiController.SetCoin(nbCoins);
        saveData.SaveCoin(nbCoins);
    }
}
