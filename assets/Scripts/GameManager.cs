using UnityEngine; // Importing the UnityEngine namespace to access Unity's core functionality.
using UnityEngine.SceneManagement; // Importing the UnityEngine.SceneManagement namespace to work with scene management.

public class GameManager : MonoBehaviour // Declaring a public class named GameManager, inheriting from MonoBehaviour.
{
    public static GameManager Instance; // Declaring a public static variable named Instance to hold a reference to the GameManager instance.
    // Declaring public static properties to provide global access to UIManager and PlayerController instances.
    public static UIManager UIManager { get; set; }
    



    private void Awake() // Awake method is called when the script instance is being loaded.
    {
        if (Instance == null) // Checking if the GameManager instance doesn't exist.
        {
            Instance = this; // Assigning the current GameManager instance to the Instance variable.
            DontDestroyOnLoad(gameObject); // Preventing the GameManager object from being destroyed when loading a new scene.
        }
    }




    // Method to open a scene specified by its build index.
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index); // Loading the scene with the given build index.
    }

    // Method to quit the game.
    public void QuitGame()
    {
        Application.Quit(); // Quitting the application. Note: This method only works in standalone platforms.
    }
}
