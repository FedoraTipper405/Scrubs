using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
  InputAction moveAction;
PlayerInputActions playerInput;
    [SerializeField] float moveSpeed;
    Vector2 inputMovement;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(inputMovement);
    }
    public void MovementInput(Vector2 input)
    {
        inputMovement = input; 
    }
    private void HandleMovement(Vector2 input)
    {
        
        if (input.x != 0 || input.y != 0)
        {
            Debug.Log(input);
        }
        Vector2 move = new Vector2(input.x,input.y);
        if (move.sqrMagnitude > 1) move.Normalize();

        this.transform.position += ((transform.right * move.x + transform.up * move.y ) * moveSpeed * Time.deltaTime);
    }
}
