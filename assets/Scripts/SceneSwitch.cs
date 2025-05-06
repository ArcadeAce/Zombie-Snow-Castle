using UnityEngine; //using UnityEngine; is crucial because it gives your script access to all of Unity’s core features, like collisions, scene management, and physics interactions. Without it, your SceneSwitch script wouldn't function properly!//

public class SceneSwitch : MonoBehaviour
{// The first curly brace opens the door to the whole function
    public int sceneIndex; //This is for when you put a number for the player to go to what scene from the scene build
    // int only accepts whole numbers (no decimals) → Example: sceneIndex = 5; ✅, but sceneIndex = 5.5; ❌ (not allowed).

    void OnTriggerEnter(Collider other) //void means the function does not return anything → It only executes an action, like switching scenes, but doesn’t pass back a value. ✅ bool returns either true or false → You could use it to confirm if the player entered the scene switch collider or not.
                                        // Notes: void OnTriggerEnter(Collider other) is a function that runs automatically when the player walks into an invisible trigger zone, like your scene switch Box Collider
                                        // Notes: For OnTriggerEnter(Collider other) to work, the Box Collider must have Is Trigger enabled—otherwise, Unity won’t recognize it as a trigger zone, and the player won’t be able to activate the scene switch

    // Notes: Collider other is the object that enters the invisible trigger zone—like your FPS Controller!
   

    // Think of {} Like a Doorway The opening lets code inside the block. Everything inside runs together until you hit the closing  }.Once it closes, the function or condition is complete!
    {
        if (other.CompareTag("Player"))// other stores whatever object entered the scene switch collider, whether it's the player, an enemy, or an item. Your script checks other.CompareTag("Player") → This confirms if the object is the FPS Controller before switching scenes.
        {
            // CompareTag("Player") is a function in Unity that checks if an object has a specific tag—in this case, it verifies if the FPS Controller has the "Player" tag before switching scenes.
            // CompareTag("Player") is used to check if the object that entered the trigger zone is the FPS Controller. If it is the player, Unity switches scenes!
            GameManager.Instance.OpenScene(sceneIndex);// GameManager script controls player deaths and when the player goes to the next scene.
        }//If you had two GameManager scripts running at the same time, it would cause problems because GameManager is designed to be a singleton—meaning only one instance should exist across all scenes.
    }// GameManager is a singleton is a special type of class in programming that ensures only ONE instance of that class exists at any time.
}
// The last curly brace ends the function
