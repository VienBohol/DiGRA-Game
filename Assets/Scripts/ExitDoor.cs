using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
   // public string nextSceneName;

    private bool opened = false;

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

                if (inventory.carryKey != null)
                {
                    inventory.carryKey.ConsumeKey();
                    Destroy(gameObject);
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