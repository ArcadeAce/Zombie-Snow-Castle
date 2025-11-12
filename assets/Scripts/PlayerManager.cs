using System;// Gives access to basic C# functionality, including math operations and system utilities.
using System.Collections;// Enables IEnumerator (used for coroutines), which helps with timed events like weapon switching delays.
using System.Diagnostics;
using UnityEngine;// Provides access to Unity-specific functions like GameObjects, physics, and UI interactions.

public class WeaponSwitcher : MonoBehaviour// 🎮 Defines a class that handles switching between weapons.
{
    public static Weapon slot1;// 🔫 Stores reference to **weapon in slot 1** (first equipped weapon).
    public static Weapon slot2;// 🔫 Stores reference to **weapon in slot 2** (second equipped weapon).
    public Weapon activeWeapon { get; private set; }// 🔄 Keeps track of **currently active weapon** → Ensures proper firing and UI updates.

    public bool slot1Occupied; // Made public // Tracks whether a weapon **exists in slot 1** → true = weapon equipped, false = empty.
    public bool slot2Occupied; // Made public // Tracks whether a weapon **exists in slot 2** → true = weapon equipped, false = empty.

    private int slot1index;// 🔢 Stores **weapon type index** for slot 1 → Helps retrieve correct weapon if needed later.
    private int slot2index;// 🔢 Stores **weapon type index** for slot 2 → Helps retrieve correct weapon if needed later.

    private bool activeisslot1;
    // 🔄 Tracks **which weapon is currently equipped**.
    // ✅ true = slot 1 weapon is equipped.
    // ✅ false = slot 2 weapon is equipped.



    public bool Switchable() // 🔄 Checks if **weapon switching is allowed**.
    {
        return slot1Occupied && slot2Occupied;
        // Returns `true` **only if BOTH slot1 and slot2 have weapons equipped**.
        // If the player has **only one weapon**, this function returns `false`, preventing switching.
    }



    // ******************** for twin turbos appearing in posion zombie boss scene


    public void Pickup(Weapon prefab, bool ischest = false)
    {
        if (!slot1Occupied)
        {
            InstantiateWeapon(prefab, true, ischest);
            slot1.gameObject.SetActive(true);
            activeWeapon = slot1;
            activeisslot1 = true;
            slot1Occupied = true;

            PlayerManager.Instance.selectedWeaponType = prefab.weaponType; // ✅ Only update when weapon is equipped
        }
        else if (!slot2Occupied)
        {
            InstantiateWeapon(prefab, false, ischest);
            slot2.gameObject.SetActive(false);
            slot2Occupied = true;
            // ❌ Do NOT update selectedWeaponType here
        }
        else
        {
            DropCurrentWeapon(slot2);
            InstantiateWeapon(prefab, false, ischest);
            slot2.gameObject.SetActive(false);
            slot2Occupied = true;
            // ❌ Do NOT update selectedWeaponType here
        }
    }

    // ******************** for twin turbos appearing in posion zombie boss scene










    private void InstantiateWeapon(Weapon prefab, bool isSlot1, bool chest)// Creates a new weapon and places it in the correct inventory slot.
    {
        UnityEngine.Debug.Log("entering instaniate weapon");// Logs a message in the console for debugging.
        Weapon weapon = Instantiate(prefab, PlayerController.Instance.WeaponHolder);// Creates a new weapon object from the prefab and places it under the player's WeaponHolder.
        if (isSlot1)// If the weapon is being assigned to Slot 1...
        {
            slot1 = weapon;// 🔄 Stores the new weapon in slot 1.
            slot1index = slot1.index;// 🔢 Saves the weapon's index for future reference.
        }
        else// Otherwise, assign it to Slot 2...
        {
            slot2 = weapon;// 🔄 Stores the new weapon in slot 2.
            slot2index = slot2.index;// Saves the weapon's index for future reference.
        }



        ///////////////////////////////////////////////////////////////////////////////////////////
        //THIS LINE (BELOW) HAS BEEN COMMENTED OUT BECAUSE IT IS ADDING 20 BULLETS TO THE TOTL BULLETS EACH TIME THE PLAYER GOES TO EACH SCENE!
        //PlayerManager.Instance.AddAmmo(prefab.weaponType, chest ? prefab.totalBullets : prefab.clipSize);
        // Determines how much ammo to add based on whether the weapon came from a chest.
        // If `chest == true`, add full ammo (`totalBullets`). Otherwise, only add clip-sized ammo (`clipSize`).
        /////////////////////////////////////////////////////////////////////////////////////////////////////////





    }

    private void DropCurrentWeapon(Weapon weapon)// Handles dropping the currently held weapon in Slot 2.
    {
        GameObject droppedWeapon = Instantiate(weapon.gameObject, transform.position, transform.rotation);// Instantiates the weapon in the scene at the player's current position.
        Rigidbody rb = droppedWeapon.GetComponent<Rigidbody>() ?? droppedWeapon.AddComponent<Rigidbody>();// Adds a Rigidbody component if the weapon doesn’t already have one.
                                                                                                          // 🔄 This allows the dropped weapon to interact with physics (fall to the ground, bounce, etc.).
        rb.useGravity = true;// Enables gravity, so the weapon falls naturally.
        MeshCollider meshCollider = droppedWeapon.GetComponent<MeshCollider>() ?? droppedWeapon.AddComponent<MeshCollider>();// Adds a MeshCollider component if the weapon doesn’t already have one.
                                                                                                                             // Ensures the weapon has physical collision, preventing it from falling through the ground.
        meshCollider.convex = true;// Allows proper physics interaction (necessary for objects that will move).




    }





    public void SwitchWeapon()// Handles switching between the player's equipped weapons.
    {
        if (activeWeapon == slot1)// Checks if the currently active weapon is **slot1**.
        {
            StartCoroutine(Switch(slot1, slot2));// Starts weapon switching animation **from slot1 to slot2**.
            activeisslot1 = false;// 🔄 Updates tracking → **slot2 is now the active weapon**.
        }
        else
        {
            StartCoroutine(Switch(slot2, slot1));// Starts weapon switching animation **from slot2 to slot1**.
            activeisslot1 = true;// 🔄 Updates tracking → **slot1 is now the active weapon**.
        }
    }




    private IEnumerator Switch(Weapon oldweapon, Weapon newweapon)// Handles switching between weapons with a **smooth transition delay**.
    {

        yield return new WaitForSeconds(1.2f);// ⏳ Adds a **1.2-second delay** before completing the weapon switch.
        oldweapon.gameObject.SetActive(false);// ❌ **Deactivates the old weapon**, making it **invisible** and **unusable**.
        newweapon.gameObject.SetActive(true);// ✅ **Activates the new weapon**, allowing the player to use it.
        activeWeapon = newweapon;// Updates the `activeWeapon` variable to **track the new equipped weapon**.

    }


    public void SwitchWeaponTo(string weaponName)// Switches to a specific weapon **based on its name**.
    {
        if (weaponName == "Shotgun" && slot2Occupied)// If the player selects "Shotgun" and slot 2 is occupied...
        {
            StartCoroutine(Switch(slot1, slot2));// Starts the transition **from slot 1 (TwinTurbos) to slot 2 (Shotgun)**.
            activeisslot1 = false;// 🔄 Updates tracking → **Shotgun is now the active weapon**.
            activeWeapon = slot2; // Marks the Shotgun as the currently equipped weapon.
        }
        else if (weaponName == "TwinTurbos" && slot1 != null)// If the player selects "TwinTurbos" and slot 1 exists...
        {
            StartCoroutine(Switch(slot2, slot1));// Starts the transition **from slot 2 (Shotgun) to slot 1 (TwinTurbos)**.
            activeisslot1 = true;// 🔄 Updates tracking → **TwinTurbos is now the active weapon**.
            activeWeapon = slot1;// Marks TwinTurbos as the currently equipped weapon.


        }
    }





    public void SetupWeapons()// 🔄 Restores equipped weapons when transitioning between scenes.
    {
        if (!slot1Occupied && !slot2Occupied)// If **no weapons are equipped**, exit the function.
            return;

        if (slot1Occupied)// If the **player has a weapon in Slot 1**, recreate it from saved weapon data.
        {
            InstantiateWeapon(GameAssets.Instance.Weaponprefabs[slot1index], true, false);// Loads weapon from GameAssets
            slot1.gameObject.SetActive(true);// Activates Slot 1 weapon so it's ready to use.
        }
        if (slot2Occupied)// If the **player has a weapon in Slot 2**, recreate it from saved weapon data.
        {
            InstantiateWeapon(GameAssets.Instance.Weaponprefabs[slot2index], false, false);// Loads weapon from GameAssets.
        }

        if (activeisslot1)// Ensures the correct weapon remains equipped when transitioning between scenes.
        {
            activeWeapon = slot1;// Keep Slot 1 weapon active.
        }
        else
        {
            activeWeapon = slot2;// Keep Slot 2 weapon active.
            SwitchWeapon();// Switch if needed, ensuring correct weapon setup.

        }
    }
    // **********************************************************For twin turbos to show by themselves no shotgun in poison zombie boss scene
    public void SyncWeaponVisibility()
    {
        if (PlayerManager.Instance.selectedWeaponType == "TwinTurbos")
        {
            if (slot1 != null) slot1.gameObject.SetActive(true);  // Show Twin Turbos
            if (slot2 != null) slot2.gameObject.SetActive(false); // Hide Shotgun
            activeWeapon = slot1;
            activeisslot1 = true;
        }
        else if (PlayerManager.Instance.selectedWeaponType == "Shotgun")
        {
            if (slot1 != null) slot1.gameObject.SetActive(false); // Hide Twin Turbos
            if (slot2 != null) slot2.gameObject.SetActive(true);  // Show Shotgun
            activeWeapon = slot2;
            activeisslot1 = false;
        }
    }

}


// ===================== WEAPON SWITCHER SUMMARY =====================
//
// 🔹 The WeaponSwitcher script **manages weapon switching, pickups, and inventory handling**.
// 🔹 It ensures the player can equip and swap between **Twin Turbos and Shotgun** smoothly.
//
// ✅ Core Responsibilities:
// ✅ Tracks equipped weapons in **slot1 and slot2**.
// ✅ Handles **pickup logic**, placing weapons in available slots or replacing old weapons.
// ✅ Handles **dropping weapons**, ensuring proper physics interactions.
// ✅ Supports **smooth weapon transitions** using coroutines for a **realistic swap delay**.
// ✅ Enables direct selection of weapons using **SwitchWeaponTo()** (e.g., selecting Shotgun or Twin Turbos).
// ✅ Restores weapons when transitioning between scenes using **SetupWeapons()**.
//
// ✅ Weapon Switching Mechanics:
// ✅ If the player has **both weapons equipped**, they can toggle between them using **SwitchWeapon()**.
// ✅ Uses `Switch()` with a **1.2-second delay** to prevent instant switching (creating a more immersive transition).
// ✅ Ensures the correct weapon stays active throughout the game.
//
// ❌ What It Does NOT Do:
// ❌ It does NOT handle **weapon firing** or **ammo consumption** (handled by `Weapon` and `PlayerManager`).
// ❌ It does NOT generate weapons automatically—**weapons must be picked up** in-game.
//
// 🔹 Think of **WeaponSwitcher as the player's inventory manager**, keeping the player's weapons organized and allowing dynamic switching while maintaining a smooth gameplay experience! 
















