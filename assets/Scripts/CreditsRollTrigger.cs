using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsRollTrigger : MonoBehaviour
{
    public string creditsSceneName = "CreditsScene";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(creditsSceneName);
        }
    }
}





