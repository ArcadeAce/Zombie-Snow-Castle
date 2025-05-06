using UnityEngine; // Importing the UnityEngine namespace to access Unity's core functionality.
using UnityEngine.SceneManagement; // Importing the UnityEngine.SceneManagement namespace to work with scene management.
// Build Settings is where you list available scenes and assign them index numbers → Scene Manager uses those index numbers to load scenes correctly. 
// Your GameManager uses Scene Manager functions to swap scenes based on the scene index stored in Build Settings.

public class GameManager : MonoBehaviour // Declaring a public class named GameManager, inheriting from MonoBehaviour.
 // MonoBehaviour is like Unity’s "passport"—any script that inherits from it can access Unity’s core features, like physics, scenes, input, and triggers!

{
    public static GameManager Instance; // Declaring a public static variable named Instance to hold a reference to the GameManager instance.
                                        // Think of static like this: static is like a global radio frequency—all scripts can tune in and use it without needing a separate copy!
                                        // Declaring public static properties to provide global access to UIManager and PlayerController instances.
                                        // Instead of needing a new GameManager, other scripts can simply call GameManager.Instance globally! Used for singleton patterns → Ensures only ONE GameManager exists, controlling scene transitions, player deaths, and more.
                                        // 
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
