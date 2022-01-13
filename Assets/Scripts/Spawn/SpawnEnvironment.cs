using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnvironment : MonoBehaviour
{
    public List<GameObject> listGameObjects;

    private List<OP.ObjectPulling> listObjectPulling;

    public string name_op;

    private float speed = 0;
    public float spawnTimeMin = 1.5f;
    public float spawnTimeMax = 1.5f;

    public float spawnRotationMin = -25;
    public float spawnRotationMax = 25;

    public Vector3 spawnPosition;

    private bool stop = false;

    private Coroutine coroutine;

    private void Start()
    {
        listObjectPulling = new List<OP.ObjectPulling>();

        int cpt = 1;
        foreach (GameObject go in listGameObjects)
        {
            listObjectPulling.Add(new OP.ObjectPulling(go, 4, name_op + cpt));
            cpt++;
        }
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

            GameObject env = listObjectPulling[Random.Range(0, listObjectPulling.Count)].GetObject();

            env.transform.position = spawnPosition;

            float rotationY = Random.Range(spawnRotationMin, spawnRotationMax);
            env.transform.rotation = Quaternion.Euler(0, rotationY, 0);

            env.SetActive(true);
            env.GetComponent<MoveObjects>().Move(speed, MoveTo.z);
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
