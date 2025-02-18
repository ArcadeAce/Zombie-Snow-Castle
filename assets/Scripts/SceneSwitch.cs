using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public int sceneIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            GameManager.Instance.OpenScene(sceneIndex);
        }
    }
}