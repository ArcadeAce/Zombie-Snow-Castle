using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour
{
    [Header("Intro Panel Names (in order)")]
    public string[] panelNames = {
        "Copilot intro words",
        "Copilot cryogenic",
        "Copilot lab",
        "Copilot intro words 2",
        "Copilot lab explosion",
        "Copilot intro words 3",
        "Copilot intro words 4",
        "Copilot intro words 5"
    };

    public float displayTime = 5f;       // Time each image is fully visible
    public int snowSceneIndex = 1;       // Scene index for Snow Scene

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        foreach (string name in panelNames)
        {
            GameObject panel = GameObject.Find(name);
            if (panel == null)
            {
                Debug.LogWarning($"Panel '{name}' not found.");
                continue;
            }

            panel.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            panel.SetActive(false);
        }

        SceneManager.LoadScene(snowSceneIndex);
    }
}



