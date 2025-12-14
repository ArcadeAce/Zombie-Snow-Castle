using System; // Gives access to basic C# functionality, such as math operations and system utilities.
using System.Collections; // Enables IEnumerator, which helps with delayed effects like animations or barrel spinning.
using System.Collections.Generic; // Allows you to use advanced collections like Lists<>.
using UnityEngine; // Grants access to Unity’s core functions, such as GameObjects, physics, and UI elements.

public class Shotgun : Weapon//🔫 Defines a**Shotgun class**that * *inherits from Weapon**, meaning it automatically gains shooting mechanics
{
    public GameObject barrel;// Gives access to basic C# functionality, such as math operations and system utilities.
    private ShootSound shootsound;// 🔊 Holds the **shotgun sound effects**, controlling firing and spin sound.
    private Quaternion rotation;// 📐 Represents **rotation angles**, used for spinning the barrel dynamically.
    private bool instantiated;// Tracks whether the **shotgun has been initialized**.
    private float ZRotation;// 🔄 Controls the **barrel’s rotation along the Z-axis**.

    public override void Start()// Runs **automatically** when the shotgun is spawned in the scene.
    {
        base.Start();// 🔄 Calls the **Weapon class's Start() function** to ensure core weapon mechanics are initialized.
        shootsound = GetComponent<ShootSound>();// 🎵 Finds and assigns the **shotgun's sound effects component**.
        ZRotation = -90f;// 📐 Sets the **initial rotation value** for the shotgun barrel.
        rotation = Quaternion.Euler(0f, -90f, ZRotation);// Configures the **default angle for the barrel’s rotation**.
        barrel.transform.localRotation = rotation;// 📐 Applies the rotation settings to the **shotgun’s barrel**.
        instantiated = true;// Marks the shotgun as **activated**, making sure it’s ready for use.
    }

    public void SpinBarrel() // Handles **barrel spinning activation** before charging up a shotgun blast.
    {
        Debug.Log("spin"); // Logs `"spin"` in the Unity Console → Helps debug whether the function is being called correctly.
        StartCoroutine(Spin()); // ⏳ **Starts the Spin() coroutine**, which delays and executes **the barrel rotation animation**.
    }


    private IEnumerator Spin() // ⏳ Coroutine → Handles **delayed rotation effects** for smooth barrel spinning.
    {
        yield return new WaitForSeconds(0.5f); // ⏳ **Pauses for 0.5 seconds** before triggering the charge-up animation.

        _animator.SetTrigger("Charge Up"); // **Triggers the "Charge Up" animation**, showing visual feedback when the shotgun is preparing for a blast.

        yield return new WaitForSeconds(0.1f); // ⏳ **Waits an additional 0.1 seconds** before playing the spin sound.

        shootsound.PlaySpinSound(); // 🔊 **Plays the spin sound effect**, giving an audio cue that the barrel rotation is happening.

        ZRotation += 72f; // 📐 **Increases the rotation angle by 72 degrees**, making the barrel spin visually.

        rotation = Quaternion.Euler(0f, -90f, ZRotation); // 🔄 **Applies the new rotation angle to the shotgun barrel**, ensuring it spins correctly.
    }


    public override void Shoot()// 🔫 Handles the **shotgun firing mechanics**.
    {
        Debug.Log("Firing the shotgun");// Logs "Firing the shotgun" in Unity Console → Helps confirm this function runs correctly.

        PlayerManager.Instance.shotgunShells--;// 🔄 **Reduces the number of shotgun shells left** when the shotgun is fired.


        if (PlayerManager.Instance.shotgunShells < 0)// 🚫 Prevents negative ammo values → Ensures **ammo count doesn’t drop below zero**.
        {
            PlayerManager.Instance.shotgunShells = 0;// If ammo goes below **zero**, **sets it back to zero**.
        }


        GameManager.UIManager.UpdateShotgunShells(PlayerManager.Instance.shotgunShells);// 🔄 **Updates the UI** to reflect the remaining shotgun shells after firing

        RaycastHit hit; // 🔎 Creates a **RaycastHit variable** to store information about what the shotgun hits.

        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f)) // 🔫 Fires an **invisible laser (Raycast)** from the player's camera **straight forward**.
        {
            if (hit.collider && hit.collider.transform.TryGetComponent(out Enemy enemy)) // If the Raycast **hits an enemy**, apply damage.
            {
                enemy.TakeDamage(damage, hit); // **Reduces enemy health** based on the shotgun’s damage value.
            }
            else if (hit.collider.transform.parent != null) // 🔄 If the Raycast **hits a child object** (like an enemy’s limb), check its parent.
            {
                if (hit.collider.transform.parent.TryGetComponent(out Enemy enemy2)) // If the **parent object is an enemy**, apply damage.
                {
                    enemy2.TakeDamage(damage, hit); // **Damages the parent enemy**, ensuring shots hit properly even if aimed at limbs.
                }
            }
        }
    }

    private void FixedUpdate() // 🔄 FixedUpdate runs **at a consistent time interval**, ideal for physics-based actions.
    {
        if (!instantiated) // 🚫 Checks if the shotgun **has not been activated** (instantiated).
        {
            return; // **Stops execution immediately** if the shotgun hasn’t been set up yet.
        }

        barrel.transform.localRotation = Quaternion.Slerp(
            barrel.transform.localRotation, // 📐 **Current rotation of the barrel**.
            rotation, // 📐 **Target rotation (which updates in the Spin coroutine)**.
            Time.deltaTime * 5f // ⏳ **Smoothly transitions from the current rotation to the target rotation** over time.
        );
    }
}


// Explanation of the shotgun when it charges up, I am going to make the coding for the charging up shots later on after my demo is done, and Grenade Throwing Zombie drops this shotgun when he is defeated.

//The shotgun uses a 4 Charge up for the shotgun, the shotgun can be charged 2 to 4 times for a more power blast, when the player presses the charge up sprite button.

//Example, if you pump action the shotgun 3 times before firing the blast will be 3 times as powerful, 4 times for a awesome blast💥

//3 Boxing gloves sprites signify charge ups when the charge up sprite button is pressed.

// Copilot you can ignore charge up functions for now till its time to write the functions for the charge up shotgun part of the shotguns firing.


