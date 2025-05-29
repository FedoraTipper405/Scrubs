using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    PlayerInputActions playerInput;
    [SerializeField] float moveSpeed;
    Vector2 inputMovement;
    Rigidbody2D rb;
    [SerializeField]Transform PlayerTransform;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(inputMovement);
    }
    public void MovementInput(Vector2 input)
    {
        inputMovement = input;
        if(input.x > 0)
        {
            PlayerTransform.localScale = new Vector3(1,1, 1);
        }
        else if(input.x < 0)
        {
            PlayerTransform.localScale = new Vector3(-1, 1, 1);
        } 
        
    }
    private void HandleMovement(Vector2 input)
    {
        //xh add anima
        if (input.x == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
       // xh code end

        Vector2 move = new Vector2(input.x, input.y);
        if (move.sqrMagnitude > 1) move.Normalize();

        this.transform.position += ((transform.right * move.x + transform.up * move.y) * moveSpeed * Time.deltaTime);
    }
}
