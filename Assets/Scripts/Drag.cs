using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    float dragStartX = 0;
    float dragStartY = 0;
    float dragEndX = 0;
    float dragEndY = 0;

    public void BeginDrag()
    {
        dragStartX = Input.mousePosition.x;
        dragStartY = Input.mousePosition.y;
    }

    public void EndDrag()
    {
        dragEndX = Input.mousePosition.x;
        dragEndY = Input.mousePosition.y;

        if (dragStartX == dragEndX && dragStartY == dragEndY)
            return;

        float dragStart = 0;
        float dragEnd = 0;
        bool horizontal;

        if (Mathf.Abs(dragEndX - dragStartX) > (Mathf.Abs(dragEndY - dragStartY)))
        {
            dragStart = dragStartX;
            dragEnd = dragEndX;
            horizontal = true;
        }
        else
        {
            dragStart = dragStartY;
            dragEnd = dragEndY;
            horizontal = false;
        }

        Direction direction;
        if (dragEnd - dragStart < 0)
        {
            if (horizontal)
                direction = Direction.Left;
            else
                direction = Direction.Down;
        }
        else
        {
            if (horizontal)
                direction = Direction.Right;
            else
                direction = Direction.Up;
        }

        PlayerController.Instance.Move(direction);
    }
}

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}
