using UnityEngine;

public class Key : MonoBehaviour
{
    public float followDistance = 1f;

    private bool isCollected = false;

    private Transform player;

    private void Update()
    {
        if (isCollected && player != null)
        {
            Vector3 targetPosition = player.position - player.up * followDistance;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                Time.deltaTime * 10f
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