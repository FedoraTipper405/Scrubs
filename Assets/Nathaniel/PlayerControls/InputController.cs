using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.MoveInput.performed += (var) => playerMove.MovementInput(var.ReadValue<Vector2>());
        playerInputActions.Movement.MoveInput.canceled += (var) => playerMove.MovementInput(var.ReadValue<Vector2>());
        playerInputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
