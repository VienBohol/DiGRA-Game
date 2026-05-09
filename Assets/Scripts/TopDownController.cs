using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector2 lastMoveDirection = Vector2.down;

    [Header("Key")]
    public bool hasKey = false;
    public Key carryKey;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private PlayerControls controls;
    private float toggleCooldown = 1f;
    private float lastToggleTime = -Mathf.Infinity;

    [Header("Audio")]
    public AudioClip shiftSound;
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
        }

        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    private void OnToggleWorld(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.time < lastToggleTime + toggleCooldown)
            return;

        lastToggleTime = Time.time;

        StartCoroutine(ShiftWorld());
    }

    private IEnumerator ShiftWorld()
    {
        animator.SetTrigger("onShift");

        if (shiftSound != null)
        {
            audioSource.PlayOneShot(shiftSound);
        }

        yield return new WaitForSeconds(0.5f);

        WorldStateManager.Instance.ToggleWorld();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
    
}