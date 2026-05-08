using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WorldCollider : MonoBehaviour
{
    public bool enabledInPurpleWorld = false;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        WorldStateManager.OnWorldChanged += UpdateCollider;
        UpdateCollider();
    }

    private void OnDisable()
    {
        WorldStateManager.OnWorldChanged -= UpdateCollider;
    }

    private void UpdateCollider()
    {
        bool isPurple = WorldStateManager.Instance.isPurpleWorld;

        // Enable collider depending on your setting
        boxCollider.enabled = (isPurple == enabledInPurpleWorld);
    }
}