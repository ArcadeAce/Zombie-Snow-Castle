using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour// Declares a Weapon class inheriting from MonoBehaviour (giving it access to Unity functions).
{
   
    public int damage;// Defines the amount of damage this weapon inflicts on enemies.
    public int clipSize;// Determines the max bullets the weapon can hold before reloading.
    public int totalBullets;// Tracks the **total number of bullets the player has** for this weapon.
    protected Transform fpsCam;// Stores a reference to the **FPS Controller's camera** for aiming and shooting.

    public int index;// Identifies the weapon by **index number** (used for weapon switching logic).
    public bool Active;// Indicates whether the weapon is **currently equipped and active**.
    public bool chestweapon;// Tracks whether this weapon was obtained from a chest (**only Twin Turbos start in a wooden chest**)

    [HideInInspector] public Animator _animator;// Stores the **weapon's Animator**, handling firing and reload animations.

    private bool Firing;// Flags whether the weapon is **currently being fired**.

    public string weaponType;// Holds the **weapon's name/type** (e.g., "TwinTurbos", "Shotgun").

    internal int weaponIndex;// Stores **internal weapon indexing** for further setup logic.









    private void OnEnable()// 🔄 Called when the weapon object becomes active (e.g., when switching weapons).
    {
      
        _animator = GetComponent<Animator>();// Retrieves and stores a reference to the weapon's Animator component.
                                             // This allows the weapon to trigger animations (shooting, reloading, etc.).
        fpsCam = PlayerController.Instance.cam.transform;// Gets a reference to the player's FPS camera.
                                                         // This is important for aiming and performing Raycast-based shooting.


        Active = true;// Marks the weapon as currently active.
                      // This ensures it can be used by the player.


        GameManager.UIManager.ReloadGun(weaponType, PlayerManager.Instance.bulletsInClip, PlayerManager.Instance.remainingBullets);// Updates the UI to display current bullet count.
                                                                                                                                   // Calls UIManager to refresh ammo values for the selected weapon.
    }


    public virtual void Start()// 🔄 Virtual Start function (meant for child classes to override if needed).
    {
        // This function is intentionally left empty.
        // Child weapon classes (like Shotgun or TwinTurbos) can override this function
        // To implement custom behavior when the weapon initializes.
    }










    public void Setup(bool chest)// 🔄 Sets up the weapon when picked up, adjusting ammo values if obtained from a chest.
    {
        if (chest)// Checks if the weapon was picked up from a chest.
                  // If true, it adjusts the bullets inside the clip and remaining ammo count.
        {

            //PlayerManager.Instance.bulletsInClip = clipSize - 10;// Reduces the bullets inside the weapon's clip when taken from a chest.
                                                                 // This ensures that chest weapons don't start fully loaded.
            //PlayerManager.Instance.remainingBullets = totalBullets - 20;// Adjusts the total ammo available when the weapon is obtained from a chest.
                                                                        // Limits available bullets when picking up weapons from storage.
        }

        // Calls UIManager to refresh the displayed ammo count.
        // Ensures the player sees the correct values after picking up the weapon.
        GameManager.UIManager.ReloadGun(weaponType, PlayerManager.Instance.bulletsInClip, PlayerManager.Instance.remainingBullets);
    }


    internal void Drop()// 🔄 Placeholder function for dropping a weapon (can be expanded later).
    {
        // This function is currently empty, meaning it **does not perform any action**.

        // Intended for future implementation → Can be used to **make the player drop weapons**.

        // Possible future behaviors:
        // - Remove the weapon from the player's inventory.
        // - Make the weapon fall to the ground using **Rigidbody physics**.
        // - Allow other players/enemies to pick up dropped weapons.

        // 💡 Since it's marked as `internal`, this function **can only be accessed within the same assembly**.
        // This means it **cannot be called from external scripts** outside of the main project.
    }


    public void Reload()// 🔄 Handles weapon reloading, ensuring the correct ammo type is updated.
    {
        Debug.Log("reload problem");// Logs a debug message when the reload function is called.
                                    // This helps in identifying potential reload issues during gameplay.
        int remainingBullets, bulletsInClip;// Declares two integer variables to store ammo data:
        switch (weaponType)// Uses a switch statement to determine which weapon is being reloaded.
        {
            case "TwinTurbos":// 🔫 Case 1: The player is reloading Twin Turbos.
                remainingBullets = PlayerManager.Instance.remainingBullets;// Retrieves the remaining Twin Turbos bullets from PlayerManager.
                bulletsInClip = PlayerManager.Instance.bulletsInClip;// Retrieves the number of bullets inside the Twin Turbos clip.
                break;

            case "Shotgun":// 🔫 Case 2: The player is reloading the Shotgun.
                remainingBullets = PlayerManager.Instance.remainingShotgunShells;// Retrieves the remaining shotgun shells from PlayerManager.
                bulletsInClip = PlayerManager.Instance.shotgunShells;// Retrieves the number of shells inside the Shotgun clip.
                break;
            default:// ❌ Default case: If the weapon type doesn’t match Twin Turbos or Shotgun.
                Debug.Log("don't have a ammo assigned");// Logs an error message if an incorrect weapon type is passed.
                return;// Stops function execution immediately to prevent unintended behavior.
        }

      
        if (remainingBullets <= 0 || bulletsInClip == clipSize)// Checks if reloading is possible → Ensures the player has bullets left and that the clip is not already full.
        {
            Debug.Log($"Reload 2 remaining bullets: {remainingBullets} {bulletsInClip} {clipSize}");// Logs a debug message showing current ammo values before stopping the reload attempt.
      
            return;// Stops the function execution early if:
            // - There are **zero bullets left to reload** (`remainingBullets <= 0`).
            // - The weapon’s clip is **already full** (`bulletsInClip == clipSize`).
        }


        _animator.SetTrigger("Reload");// Triggers the weapon’s reload animation using the Animator component.


        int reload = clipSize - bulletsInClip;// Calculates how many bullets are needed to **fill the clip to max capacity**.


        if (remainingBullets >= reload)// Checks if the player has **enough bullets** to completely reload the weapon.
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, clipSize);// If the player has **enough bullets** to refill the clip:
                                                                          // - Sets the clip to max capacity (`clipSize`).
            PlayerManager.Instance.AddAmmo(weaponType, -reload);// - Deducts the reloaded bullets from the player's total ammo.
        }
        else
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, remainingBullets);// If the player **does not** have enough bullets for a full reload:
                                                                                  // - Fills the clip only with the remaining bullets available.
            PlayerManager.Instance.AddAmmo(weaponType, -remainingBullets);// - Deducts all remaining bullets from the player's inventory.
        }

    }









    private void Update()
    {
        Int32 pointerID;// Declares an integer variable `pointerID`, used to track input (mouse or touch interaction).

#if UNITY_EDITOR// Preprocessor directive `#if UNITY_EDITOR`
        // - Checks if the game is running inside the Unity Editor.
        // - If true, assigns `pointerID = -1` (used for **mouse input** in the editor).
        pointerID = -1;
#else// ✅ If the game is running on an **actual device (mobile, console, etc.)**, assigns `pointerID = 0`. // - `0` typically refers to the **primary touch input** on mobile devices.
        pointerID = 0;
#endif









        //if (Input.GetMouseButtonDown(0))// 🔄 Detects if the player presses the fire button and handles shooting mechanics.
        {
          
            if (EventSystem.current.IsPointerOverGameObject(pointerID) && EventSystem.current.currentSelectedGameObject != null)// Checks if the mouse or touch input is over a UI element.
                                                                                                                                // Prevents firing while interacting with UI components like buttons.
            {

                if (EventSystem.current.currentSelectedGameObject.gameObject.name == "Fire Button")// Checks if the clicked UI element is specifically the **Fire Button**.
                {
              
                    if (PlayerManager.Instance.bulletsInClip <= 0)// Prevents firing if there are **zero bullets left** in the clip.
                    {
                        return;// Stops execution immediately (prevents shooting when out of ammo).
                    }

                  
                    Firing = true;// Enables the firing flag, allowing animations & shooting logic to run.
                }
            }
        }

     
        if (Input.GetMouseButtonUp(0))// 🔄 Detects if the player **releases** the fire button, stopping firing animations.
        {
         
            Firing = false;// Sets `Firing` to false, ensuring the weapon stops firing.
        }

       
        if (PlayerManager.Instance.bulletsInClip <= 0)// 🔄 Checks if the player has run out of bullets mid-firing.
        {
            Debug.Log("outofammo");// Logs a debug message to confirm the player is out of ammo.
            Firing = false;// Stops firing if ammo reaches zero.
        }

      
        _animator.SetBool("Firegun", Firing);// 🔄 Updates the weapon's firing animation based on whether the player is shooting.
    }








    
    public virtual void Shoot()// 🔫 Handles shooting mechanics when the player fires a weapon.
    {
        
        PlayerManager.Instance.bulletsInClip--;// Reduces the number of bullets in the clip when the weapon is fired.


        if (PlayerManager.Instance.bulletsInClip < 0)// Ensures the bullet count never goes below zero (prevents negative values).
        {
            PlayerManager.Instance.bulletsInClip = 0;
        }

      
        GameManager.UIManager.UpdateBullets(PlayerManager.Instance.bulletsInClip);// Updates the UI to reflect the new bullet count after firing.


        RaycastHit hit;// Creates a RaycastHit variable → Used to store information about what the bullet hits.
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f))// Performs a **Raycast** (invisible laser) from the player's camera forward, with a max range of 1000 units.
                                                                             // If the Raycast detects a hit, it stores the collision details inside the `hit` variable.
        {

            if (hit.collider && hit.collider.transform.TryGetComponent(out Enemy enemy))// If the object hit has a collider and is an enemy, apply damage.
            {

                enemy.TakeDamage(damage, hit);// Calls the enemy's **TakeDamage()** function, reducing its health.
            }
          
            else if (hit.collider.transform.parent != null)// If the object hit is **part of another object** (like a limb attached to a body),
                                                           // Check its parent object and apply damage if it’s an enemy.
            {
                if (hit.collider.transform.parent.TryGetComponent(out Enemy enemy2))
                {
                 
                    enemy2.TakeDamage(damage, hit);// Deals damage to the parent enemy object.
                }
            }
        }

    }
}



// ===================== WEAPON SCRIPT SUMMARY =====================

// The Weapon script NOT a singleton → Each weapon is an individual object that gets instantiated when picked up.
// The Weapon script serves as the base class for all weapons, handling core mechanics such as shooting, reloading, and damage calculations.


// Think of Weapon as the **foundation for all weapons**, providing core shooting mechanics while 
// allowing individual weapons like TwinTurbos to extend and specialize their behavior! 
// Weapon scripts works with the GameManager and the PlayerManager script to update the ammo value for the Twin Turbos
