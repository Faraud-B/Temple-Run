using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private GameController gameController;

    private PlayerPosition playerPosition;

    public bool invincible = false;

    private bool is_moving = false;
    private Coroutine coroutine;
    
    [Header("Move")]
    public float moveSpeed = 0.25f;
    public float[] positions = { -1.5f, 0f, 1.5f };

    [Header("Jump")]
    public float jumpSpeed = 0.25f;
    public float jumpHeight = 2;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameController = GameController.Instance;

        playerPosition = PlayerPosition.middle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Move(Direction.Left);
        if (Input.GetKeyDown(KeyCode.D))
            Move(Direction.Right);
        if (Input.GetKeyDown(KeyCode.Space))
            Move(Direction.Up);
    }

    public void Move(Direction direction)
    {
        if (is_moving)
            return;

        switch(direction)
        {
            case Direction.Left:
                if (playerPosition == PlayerPosition.left)
                    break;

                if (playerPosition == PlayerPosition.right)
                    playerPosition = PlayerPosition.middle;
                else
                    playerPosition = PlayerPosition.left;

                coroutine = StartCoroutine(CoroutineMove());

                break;

            case Direction.Right:
                if (playerPosition == PlayerPosition.right)
                    break;

                if (playerPosition == PlayerPosition.left)
                    playerPosition = PlayerPosition.middle;
                else
                    playerPosition = PlayerPosition.right;

                coroutine = StartCoroutine(CoroutineMove());

                break;

            case Direction.Up:
                coroutine = StartCoroutine(CoroutineJump());
                break;

            case Direction.Down:

            default:
                Debug.Log(direction);
                break;

        }
    }

    IEnumerator CoroutineMove()
    {
        is_moving = true;

        float positionX = 0;
        if (playerPosition == PlayerPosition.left)
            positionX = positions[0];
        if (playerPosition == PlayerPosition.middle)
            positionX = positions[1];
        if (playerPosition == PlayerPosition.right)
            positionX = positions[2];

        Vector3 destination = new Vector3(positionX, this.transform.position.y, this.transform.position.z);

        float t = 0;
        Vector3 startPosition = transform.position;
        while (t < moveSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, destination, t / moveSpeed);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;

        is_moving = false;
    }

    IEnumerator CoroutineJump()
    {
        is_moving = true;

        float t = 0;
        Vector3 startPosition = transform.position;
        Vector3 destination = startPosition + new Vector3(0, jumpHeight, 0);
        while (t < jumpSpeed / 2)
        {
            transform.position = Vector3.Lerp(startPosition, destination, t / jumpSpeed);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;

        while (t < jumpSpeed)
        {
            transform.position = Vector3.Lerp(destination, startPosition, t / jumpSpeed);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;

        is_moving = false;
    }

    public void ResetPosition()
    {
        StopCoroutine(coroutine);
        is_moving = false;
        playerPosition = PlayerPosition.middle;
        transform.position = new Vector3(positions[1], 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!invincible)
                gameController.Death();
        }
        if (other.CompareTag("Coin"))
        {
            gameController.AddCoin();
            other.gameObject.SetActive(false);
        }
    }

}

enum PlayerPosition
{
    left,
    middle,
    right
}
