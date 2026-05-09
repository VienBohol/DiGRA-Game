using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;

    public void OnTriggerEnter2D(Collider2D other)
    {
        winPanel.SetActive(true);
    }
}
