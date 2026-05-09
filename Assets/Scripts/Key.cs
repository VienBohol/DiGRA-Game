using UnityEngine;

public class Key : MonoBehaviour
{
    public float followDistance = 1f;

    private bool isCollected = false;

    private Transform player;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if (isCollected && player != null)
        {
            TopDownController controller = player.GetComponent<TopDownController>();

            Vector2 followDirection = Vector2.down;

            if (controller != null)
            {
                followDirection = controller.lastMoveDirection;
            }

            Vector3 targetPosition =
                player.position - (Vector3)(followDirection * followDistance);
            
            targetPosition += Vector3.up * Mathf.Sin(Time.time * 5f) * 0.1f;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                0.25f
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected)
            return;

        TopDownController inventory = other.GetComponent<TopDownController>();

        if (inventory != null)
        {
            inventory.hasKey = true;
            inventory.carryKey = this;
            player = other.transform;
            isCollected = true;

            Debug.Log("Key picked up!");
        }
    }

    public void ConsumeKey()
    {
        Destroy(gameObject);
    }
}