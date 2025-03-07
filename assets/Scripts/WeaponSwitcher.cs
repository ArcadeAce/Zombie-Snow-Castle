using System;
using System.Collections;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Weapon slot1 { get; private set; }
    public Weapon slot2 { get; private set; }
    public Weapon activeWeapon { get; private set; }
    public bool slot1Occupied; // Made public
    public bool slot2Occupied; // Made public
    private int slot1index;
    private int slot2index;
    private bool activeisslot1;

    public bool Switchable()
    {
        return slot1Occupied && slot2Occupied;
    }

    public void Pickup(Weapon prefab,bool ischest=false)
    {
        if (!slot1Occupied)
        {
            InstantiateWeapon(prefab, true, ischest);
            slot1.gameObject.SetActive(true);
            activeWeapon = slot1;
            activeisslot1 = true;
            slot1Occupied = true;
        }
        else if (!slot2Occupied)
        {
            InstantiateWeapon(prefab, false, ischest);
            slot2.gameObject.SetActive(false); // Add the new weapon to slot2 and keep it inactive
            slot2Occupied = true;
        }
        else
        {
            // Drop the current second weapon and replace it with the new weapon
            DropCurrentWeapon(slot2);
            InstantiateWeapon(prefab, false, ischest);
            slot2.gameObject.SetActive(false); // Add the new weapon to slot2 and keep it inactive
            slot2Occupied = true;
        }
    }

    private void InstantiateWeapon(Weapon prefab, bool isSlot1, bool chest)
    {
        Debug.Log("entering instaniate weapon");
        Weapon weapon = Instantiate(prefab, PlayerController.Instance.WeaponHolder);
        if (isSlot1)
        {
            slot1 = weapon;
            slot1index = slot1.index;
        }
        else
        {
            slot2 = weapon;
            slot2index = slot2.index;
        }
        PlayerManager.Instance.AddAmmo(prefab.weaponType, chest ? prefab.totalBullets : prefab.clipSize);
    }

    private void DropCurrentWeapon(Weapon weapon)
    {
        GameObject droppedWeapon = Instantiate(weapon.gameObject, transform.position, transform.rotation);
        Rigidbody rb = droppedWeapon.GetComponent<Rigidbody>() ?? droppedWeapon.AddComponent<Rigidbody>();
        rb.useGravity = true;
        MeshCollider meshCollider = droppedWeapon.GetComponent<MeshCollider>() ?? droppedWeapon.AddComponent<MeshCollider>();
        meshCollider.convex = true;
    }

    public void SwitchWeapon()
    {
        if (activeWeapon == slot1)
        {
            StartCoroutine(Switch(slot1, slot2));
            activeisslot1 = false;
        }
        else
        {
            StartCoroutine(Switch(slot2, slot1));
            activeisslot1 = true;
        }
    }

    private IEnumerator Switch(Weapon oldweapon, Weapon newweapon)
    {
        GameManager.UIManager.switchbutton.enabled = false;
        yield return new WaitForSeconds(1.2f);
        oldweapon.gameObject.SetActive(false);
        newweapon.gameObject.SetActive(true);
        activeWeapon = newweapon;
        GameManager.UIManager.switchbutton.enabled = true;
    }

    public void SwitchWeaponTo(string weaponName)
    {
        if (weaponName == "Shotgun" && slot2Occupied) // Ensure the player has picked up the shotgun
        {
            StartCoroutine(Switch(slot1, slot2));
            activeisslot1 = false;
            activeWeapon = slot2; // Set the active weapon to slot2 (Shotgun)
        }
        else if (weaponName == "TwinTurbos" && slot1 != null)
        {
            StartCoroutine(Switch(slot2, slot1));
            activeisslot1 = true;
            activeWeapon = slot1; // Set the active weapon to slot1 (Twin Turbos)
        }
    }

    public void SetupWeapons()
    {
        if (!slot1Occupied && !slot2Occupied)
            return;

     /*   if (slot1Occupied)
        {
            InstantiateWeapon(GameAssets.Instance.Weaponprefabs[slot1index], true, false);
            slot1.gameObject.SetActive(true);
        }
        if (slot2Occupied)
        {
            InstantiateWeapon(GameAssets.Instance.Weaponprefabs[slot2index], false, false);
        }*/

        if (activeisslot1)
        {
            activeWeapon = slot1;
        }
        else
        {
            activeWeapon = slot2;
            SwitchWeapon();
        }
    }
}











