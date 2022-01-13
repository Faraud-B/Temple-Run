using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    public bool countPoints = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OP.ObjectPullingChild>())
            other.gameObject.SetActive(false);

        if (countPoints)
            GameController.Instance.AddPoint(1);
    }
}
