using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndQuit : MonoBehaviour
{
    public string gameScene;

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
