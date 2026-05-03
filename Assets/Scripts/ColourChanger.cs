using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    private SpriteRenderer sr;

    public Color greenColor = Color.green;
    public Color purpleColor = Color.magenta;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void Update()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (WorldStateManager.Instance.isPurpleWorld)
            sr.color = purpleColor;
        else
            sr.color = greenColor;
    }

    private void OnEnable()
    {
        WorldStateManager.OnWorldChanged += UpdateColor;
    }

    private void OnDisable()
    {
        WorldStateManager.OnWorldChanged -= UpdateColor;
    }
}