using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public static Weapon slot1;
    public static Weapon slot2;
    public Weapon activeWeapon { get; private set; }

    public bool slot1Occupied;
    public bool slot2Occupied;

    private int slot1index;
    private int slot2index;

    private bool activeisslot1;

    public bool Switchable()
    {
        return slot1Occupied && slot2Occupied;
    }

    public void Pickup(Weapon prefab, bool ischest = false)
    {
        if (!slot1Occupied)
        {
            InstantiateWeapon(prefab, true, ischest);
            slot1.gameObject.SetActive(true);
            activeWeapon = slot1;
            activeisslot1 = true;
            slot1Occupied = true;

            PlayerManager.Instance.selectedWeaponType = prefab.weaponType;
            PlayerManager.Instance.AcquireTwinTurbos();
        }
        else if (!slot2Occupied)
        {
            InstantiateWeapon(prefab, false, ischest);
            slot2.gameObject.SetActive(false);
            slot2Occupied = true;

            //PlayerManager.Instance.AcquireShotgun();  ///commented out but maybe be needed.
        }
        else
        {
            DropCurrentWeapon(slot2);
            InstantiateWeapon(prefab, false, ischest);
            slot2.gameObject.SetActive(false);
            slot2Occupied = true;
        }
    }

    private void InstantiateWeapon(Weapon prefab, bool isSlot1, bool chest)
    {

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

        // To prevent ammo duplication across scenes
        // PlayerManager.Instance.AddAmmo(prefab.weaponType, chest ? prefab.totalBullets : prefab.clipSize);
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
            PlayerManager.Instance.SetActiveWeapon("Shotgun");  // is working for the Twin Turbos to appear when weapons have not been switched
        }
        else
        {
            StartCoroutine(Switch(slot2, slot1));
            activeisslot1 = true;
            PlayerManager.Instance.SetActiveWeapon("TwinTurbos"); // is working 

        }
    }
    /// /// added from Copilot

    //*public void SwitchWeapon()
    //{
        //if (activeWeapon == slot1)
        //{
            //StartCoroutine(Switch(slot1, slot2));
            //activeisslot1 = false;
            //PlayerManager.Instance.SetActiveWeapon("Shotgun"); // ADD
        //}
        //else
        //{
            //StartCoroutine(Switch(slot2, slot1));
            //activeisslot1 = true;
            //PlayerManager.Instance.SetActiveWeapon("TwinTurbos"); // ADD
        //}
    //}

    /// ////////added from Copilot


    private IEnumerator Switch(Weapon oldweapon, Weapon newweapon)
    {
        yield return new WaitForSeconds(1.2f);
        oldweapon.gameObject.SetActive(false);
        newweapon.gameObject.SetActive(true);
        activeWeapon = newweapon;
    }

    public void SwitchWeaponTo(string weaponName)
    {
        if (weaponName == "Shotgun" && slot2Occupied)
        {
            StartCoroutine(Switch(slot1, slot2));
            activeisslot1 = false;
            activeWeapon = slot2;
            PlayerManager.Instance.SetActiveWeapon("Shotgun");
        }
        else if (weaponName == "TwinTurbos" && slot1 != null)
        {
            StartCoroutine(Switch(slot2, slot1));
            activeisslot1 = true;
            activeWeapon = slot1;
            PlayerManager.Instance.SetActiveWeapon("TwinTurbos");
        }
    }

    public void SetupWeapons()
    {
        if (!slot1Occupied && !slot2Occupied)
            return;

        if (slot1Occupied)
        {
            InstantiateWeapon(GameAssets.Instance.Weaponprefabs[slot1index], true, false);
            slot1.gameObject.SetActive(true);
        }
        if (slot2Occupied)
        {
            InstantiateWeapon(GameAssets.Instance.Weaponprefabs[slot2index], false, false);
        }

        if (activeisslot1)
        {
            activeWeapon = slot1;
        }
        else
        {
            activeWeapon = slot2;
            //SwitchWeapon();
        }
    }

    public void SyncWeaponVisibility()
    {
        if (PlayerManager.Instance.selectedWeaponType == "TwinTurbos")
        {
            if (slot1 != null) slot1.gameObject.SetActive(true);
            if (slot2 != null) slot2.gameObject.SetActive(false);
            activeWeapon = slot1;
            activeisslot1 = true;
        }
        else if (PlayerManager.Instance.selectedWeaponType == "Shotgun")
        {
            if (slot1 != null) slot1.gameObject.SetActive(false);
            if (slot2 != null) slot2.gameObject.SetActive(true);
            activeWeapon = slot2;
            activeisslot1 = false;
        }
    }
}
