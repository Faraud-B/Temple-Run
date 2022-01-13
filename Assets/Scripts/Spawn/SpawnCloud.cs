using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCloud : MonoBehaviour
{
    public GameObject cloud;

    private OP.ObjectPulling objectPulling;

    [Header("Spawn Time")]
    private float speed = 0;
    public float speedMultiplier = 0.1f;

    public float spawnTimeMin = 1.5f;
    public float spawnTimeMax = 1.5f;

    [Header("Spawn Position")]
    public float posYMin = 0;
    public float posYMax = 0;
    public Vector3 spawnPosition;

    [Header("Scale")]
    public float scaleMin = 0;
    public float scaleMax = 0;

    private bool stop = false;

    private Coroutine coroutine;

    private void Start()
    {
        objectPulling = new OP.ObjectPulling(cloud, 3, "Cloud");
    }

    public void StartSpawn()
    {
        coroutine = StartCoroutine(CoroutineSpawn());
    }

    IEnumerator CoroutineSpawn()
    {
        stop = false;

        while (stop == false)
        {
            yield return new WaitForSecondsRealtime(Random.Range(spawnTimeMin, spawnTimeMax));

            GameObject cloud = objectPulling.GetObject();

            cloud.transform.position = new Vector3(spawnPosition.x, Random.Range(posYMin, posYMax), spawnPosition.z);

            float scale = Random.Range(scaleMin, scaleMax);
            cloud.transform.localScale = new Vector3(scale, scale, scale);

            cloud.SetActive(true);
            cloud.GetComponent<MoveObjects>().Move(speed * speedMultiplier, MoveTo.x);
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void Stop()
    {
        stop = true;
        StopCoroutine(coroutine);
    }
}
