using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    public bool canMove = false;

    public void Move(float speed, MoveTo moveTo)
    {
        canMove = true;
        StartCoroutine(CoroutineMove(speed, moveTo));
        StartCoroutine(CoroutineMoveUp());
    }

    IEnumerator CoroutineMove(float speed, MoveTo moveTo)
    {
        int multX = 0;
        int multZ = 0;

        if (moveTo == MoveTo.x)
            multX = 1;
        else if (moveTo == MoveTo.z)
            multZ = 1;

        while (canMove)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime * multX, 0, speed * Time.deltaTime * multZ);
            yield return null;
        }
    }

    IEnumerator CoroutineMoveUp()
    {
        while (this.transform.position.y <= 0)
        {
            this.transform.position += new Vector3(0, 5 * Time.deltaTime, 0);
            yield return null;
        }
    }

    private void OnDisable()
    {
        Disable();
    }

    public void Disable()
    {
        canMove = false;
    }
}

public enum MoveTo
{
    x,
    y,
    z
}
