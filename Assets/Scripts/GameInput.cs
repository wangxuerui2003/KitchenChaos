using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
	}

	public Vector2 GetMovementVecNormalized()
    {
        Vector2 moveDir = playerInputActions.Player.Move.ReadValue<Vector2>();
        return moveDir.normalized;
    }
}
