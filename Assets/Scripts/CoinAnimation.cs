using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private void Update()
    {
        float y = Mathf.PingPong(Time.time * 2, 1);

        transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
    }
}
