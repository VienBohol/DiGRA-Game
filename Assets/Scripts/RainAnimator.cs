using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RainAnimator : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite[] frames;
    public float frameRate = 12f; // frames per second

    private Image image;
    private Coroutine animCoroutine;
    private bool isPlaying = false;

    void Awake()
    {
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        if (isPlaying)
            Stop();
        else
            Play();
    }

    private void Play()
    {
        isPlaying = true;
        gameObject.SetActive(true);
        animCoroutine = StartCoroutine(Animate());
    }

    private void Stop()
    {
        isPlaying = false;
        if (animCoroutine != null)
            StopCoroutine(animCoroutine);
        gameObject.SetActive(false);
    }

    private IEnumerator Animate()
    {
        int currentFrame = 0;
        float delay = 1f / frameRate;

        while (true)
        {
            image.sprite = frames[currentFrame];
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(delay);
        }
    }
}