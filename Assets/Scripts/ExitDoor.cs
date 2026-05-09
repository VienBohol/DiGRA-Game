using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
   // public string nextSceneName;

    private bool opened = false;
    public AudioClip unlockSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (opened)
            return;
        
        TopDownController inventory = other.GetComponent<TopDownController>();

        if (inventory != null)
        {
            if (inventory.hasKey)
            {
                opened = true;
                Debug.Log("Door unlocked!");

                if (unlockSound != null)
                {
                    audioSource.PlayOneShot(unlockSound);
                }

                if (inventory.carryKey != null)
                {
                    inventory.carryKey.ConsumeKey();
                    Destroy(gameObject, unlockSound.length);
                }

                inventory.hasKey = false;

                // // Load next scene
                // SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("The door is locked.");
            }
        }
    }
}