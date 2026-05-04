using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private PlayerControls controls;
    private float toggleCooldown = 0.5f;
    private float lastToggleTime = -Mathf.Infinity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    void Update()
    {
        if (movement != Vector2.zero)
        {
            if (movement.x > 0)
                transform.rotation = Quaternion.Euler(0, 0, -90);
            else if (movement.x < 0)
                transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (movement.y > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (movement.y < 0)
                transform.rotation = Quaternion.Euler(0, 0, 180);
        }
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

        // Snap to 4 directions
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            movement = new Vector2(Mathf.Sign(input.x), 0);
        }
        else if (Mathf.Abs(input.y) > 0)
        {
            movement = new Vector2(0, Mathf.Sign(input.y));
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    private void OnToggleWorld(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.time < lastToggleTime + toggleCooldown)
            return;

        lastToggleTime = Time.time;
        WorldStateManager.Instance.ToggleWorld();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}