using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    private Coroutine coroutine;
    public float spawnTime = 1.5f;
    public float spawnPositionZ = 75;
    
    [Header("Cube")]
    public GameObject cube;
    private OP.ObjectPulling cube_objectPulling;

    public List<Material> listColors;

    private float speed = 0;
    public float[] spawnPositionsX = { -1.5f, 0f, 1.5f };

    private Material lastMaterial;
    private float lastPosition;

    [Header("River")]
    public GameObject river;
    private OP.ObjectPulling river_objectPulling;

    public int riverPercent = 50;

    [Header("Coin")]
    public GameObject coin;
    private OP.ObjectPulling coin_ObjectPulling;
    public int coinPercent = 50;

    private void Start()
    {
        cube_objectPulling = new OP.ObjectPulling(cube, 5, "Cubes");
        river_objectPulling = new OP.ObjectPulling(river, 2, "Rivers");
        coin_ObjectPulling = new OP.ObjectPulling(coin, 5, "Coins");
    }

    public void StartSpawn()
    {
        coroutine = StartCoroutine(CoroutineSpawn());
    }

    IEnumerator CoroutineSpawn()
    {
        bool coinSpawnEnable = true;
        while (true)
        {
            yield return new WaitForSecondsRealtime(spawnTime);

            //Cube
            if (Random.Range(0, 100) < 100 - riverPercent)
            {
                GameObject cube = cube_objectPulling.GetObject();

                cube.GetComponent<MeshRenderer>().material = RandomMaterial();

                cube.transform.position = new Vector3(RandomPosition(true), -1f, spawnPositionZ);
                cube.SetActive(true);
                cube.GetComponent<MoveObjects>().Move(speed, MoveTo.z);
                coinSpawnEnable = true;
            }
            //River
            else
            {
                GameObject river = river_objectPulling.GetObject();

                river.transform.position = new Vector3(spawnPositionsX[1], -1f, spawnPositionZ);
                river.SetActive(true);
                river.GetComponent<MoveObjects>().Move(speed, MoveTo.z);
                coinSpawnEnable = false;
            }

            //Coin
            if (coinSpawnEnable)
            {
                if (Random.Range(0, 100) < coinPercent)
                {
                    GameObject coin = coin_ObjectPulling.GetObject();

                    coin.transform.position = new Vector3(RandomPosition(false), -1f, spawnPositionZ);
                    coin.SetActive(true);
                    coin.GetComponent<MoveObjects>().Move(speed, MoveTo.z);
                }
            }
        }
    }

    Material RandomMaterial()
    {
        Material material;
        material = listColors[Random.Range(0, listColors.Count)];

        while(material == lastMaterial)
            material = listColors[Random.Range(0, listColors.Count)];

        lastMaterial = material;

        return material;
    }

    float RandomPosition(bool changeLast)
    {
        float position;
        position = spawnPositionsX[Random.Range(0, spawnPositionsX.Length)];

        while (position == lastPosition)
            position = spawnPositionsX[Random.Range(0, spawnPositionsX.Length)];

        if(changeLast)
            lastPosition = position;

        return position;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }
}
