using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private PlayerControls controls;
    private float toggleCooldown = 0.5f;
    private float lastToggleTime = -Mathf.Infinity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
        controls.Player.Interact.performed += OnToggleWorld;
    }

    void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Player.Interact.performed -= OnToggleWorld;
        controls.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            if (input.x > 0)
            {
                movement = new Vector2(1, 0);
                animator.SetInteger("direction", 3); // D
            }
            else
            {
                movement = new Vector2(-1, 0);
                animator.SetInteger("direction", 2); // A
            }
        }
        else if (Mathf.Abs(input.y) > 0)
        {
            if (input.y > 0)
            {
                movement = new Vector2(0, 1);
                animator.SetInteger("direction", 0); // W
            }
            else
            {
                movement = new Vector2(0, -1);
                animator.SetInteger("direction", 1); // S
            }
        }
        else
        {
            movement = Vector2.zero;
        }

        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    private void OnToggleWorld(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.time < lastToggleTime + toggleCooldown)
            return;

        lastToggleTime = Time.time;
        animator.SetTrigger("onShift"); 
        WorldStateManager.Instance.ToggleWorld();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
    
}