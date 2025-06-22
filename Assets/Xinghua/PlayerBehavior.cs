using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] PlayerAttacks attacks;
    PlayerInputActions playerInputActions;
   

    private void OnEnable()
    {
        if (playerInputActions == null)
            playerInputActions = new PlayerInputActions();

        playerInputActions.Movement.MoveInput.performed += OnMoveInput;
        playerInputActions.Movement.MoveInput.canceled += OnMoveInput;
        playerInputActions.Attack.Punch.performed += OnPunchInput;
        playerInputActions.Attack.Kick.performed += OnKickInput;

        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Movement.MoveInput.performed -= OnMoveInput;
        playerInputActions.Movement.MoveInput.canceled -= OnMoveInput;
        playerInputActions.Attack.Punch.performed -= OnPunchInput;
        playerInputActions.Attack.Kick.performed -= OnKickInput;

        playerInputActions.Disable();
    }

    private void OnMoveInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        playerMove.MovementInput(moveInput);
        attacks.DirectionToHit(moveInput);
    }

    private void OnPunchInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (attacks != null)
            attacks.PunchInput();
    }

    private void OnKickInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (attacks != null)
            attacks.KickInput();
    }
}
