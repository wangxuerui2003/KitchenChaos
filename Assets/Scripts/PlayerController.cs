using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking = false;
    private float playerHeight = 2f;
    private float playerRadius = 0.65f;
    private const float interactDistance = 2f;

	void Start()
	{
        gameInput.OnInteractAction += GameInput_OnInteraction;
	}

    private void GameInput_OnInteraction(object sender, System.EventArgs e) {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

	void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveDir2d = gameInput.GetMovementVecNormalized();
        Vector3 moveDir = new Vector3(moveDir2d.x, 0, moveDir2d.y);
        isWalking = moveDir2d != Vector2.zero;

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + new Vector3(0, playerHeight, 0), playerRadius, moveDir, moveDistance);
        if (!canMove && moveDir2d.x != 0 && moveDir2d.y != 0)
        {
            if (!Physics.CapsuleCast(transform.position, transform.position + new Vector3(0, playerHeight, 0), playerRadius, new Vector3(0, 0, moveDir2d.y), moveDistance))
            {
                moveDir = new Vector3(0, 0, moveDir2d.y).normalized;
                canMove = true;
            }
            else if (!Physics.CapsuleCast(transform.position, transform.position + new Vector3(0, playerHeight, 0), playerRadius, new Vector3(moveDir2d.x, 0, 0), moveDistance))
            {
                moveDir = new Vector3(moveDir2d.x, 0, 0).normalized;
                canMove = true;
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDistance * moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
