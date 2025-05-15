using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] PlayerAttacks attacks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.MoveInput.performed += (var) => playerMove.MovementInput(var.ReadValue<Vector2>());
        playerInputActions.Movement.MoveInput.canceled += (var) => playerMove.MovementInput(var.ReadValue<Vector2>());
        playerInputActions.Attack.Punch.performed += (var) => attacks.PunchInput();
        playerInputActions.Attack.Kick.performed += (var) => attacks.KickInput();
        playerInputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
