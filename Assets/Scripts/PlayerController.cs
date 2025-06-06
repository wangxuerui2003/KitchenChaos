using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 10f;

    private bool isWalking = false;

    void Update()
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = 1;
        }

        isWalking = moveDir != Vector2.zero;

        Vector3 move = moveSpeed * Time.deltaTime * new Vector3(moveDir.x, 0, moveDir.y).normalized;
        transform.position += move;
        transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
