using System.Collections;

using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject barrel;          // Barrel reference (assigned in Inspector)
    private ShootSound shootsound;     // Sound system for firing + spin sound

    // ⭐ Smooth rotation settings
    public float barrelRotationSpeed = 200f;   // Adjustable in Inspector
    private bool isSpinning = false;

    // ⭐ Charge-up system
    private int chargeLevel = 0;
    private bool chargeLocked = false; // Prevents more than one charge

    public override void Start()
    {
        base.Start();

        // Get ShootSound component
        shootsound = GetComponent<ShootSound>();

        if (barrel == null)
            Debug.LogError("[Shotgun] Barrel reference is NULL!");
    }

    // ===========================
    //  BARREL SPIN (Charge-Up)
    // ===========================
    public void SpinBarrel()
    {
        // ⭐ Prevent charging more than once
        if (chargeLocked)
            return;

        if (barrel == null || isSpinning)
            return;

        // Play spin sound
        if (shootsound != null)
            shootsound.PlaySpinSound();

        // Smooth rotation coroutine
        StartCoroutine(SmoothSpin());

        // ⭐ Set charge level to 1 (only one charge allowed for now)
        chargeLevel = 1;

        // ⭐ Lock the charge-up system
        chargeLocked = true;

        // ⭐ Disable the charge-up button visually
        if (GameManager.UIManager != null)
            GameManager.UIManager.DisableChargeUpButton();
    }

    private IEnumerator SmoothSpin()
    {
        isSpinning = true;

        float currentRotation = barrel.transform.localEulerAngles.z;
        float targetRotation = currentRotation + 70f;

        // Normalize to 0–360
        if (targetRotation >= 360f)
            targetRotation -= 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) > 0.1f)
        {
            currentRotation = Mathf.MoveTowardsAngle(
                currentRotation,
                targetRotation,
                barrelRotationSpeed * Time.deltaTime
            );

            barrel.transform.localEulerAngles = new Vector3(
                barrel.transform.localEulerAngles.x,
                barrel.transform.localEulerAngles.y,
                currentRotation
            );

            yield return null;
        }

        // Snap exactly to final angle
        barrel.transform.localEulerAngles = new Vector3(
            barrel.transform.localEulerAngles.x,
            barrel.transform.localEulerAngles.y,
            targetRotation
        );

        isSpinning = false;
    }

    // ===========================
    //  SHOOT — Shotgun override
    // ===========================
    public override void Shoot()
    {
        if (PlayerManager.Instance.shotgunShells <= 0)
            return;

        // ⭐ CHARGED BLAST (player pressed charge-up once)
        if (chargeLevel == 1)
        {
            Debug.Log("[Shotgun] Playing 2x blast animation.");

            // Play the charged animation
            _animator.Play("Shotgun charge 2 times blast");

            // Fire raycast + damage
            base.Shoot();

            // Consume ammo
            PlayerManager.Instance.shotgunShells--;
            GameManager.UIManager.UpdateShotgunShells(PlayerManager.Instance.shotgunShells);

            // Reset charge system
            chargeLevel = 0;
            chargeLocked = false;
            GameManager.UIManager.EnableChargeUpButton();

            return;   // ⭐ Prevent normal blast from playing
        }

        // ⭐ NORMAL BLAST (no charge-up)
        _animator.Play("Shotgun firing");

        base.Shoot();

        PlayerManager.Instance.shotgunShells--;
        GameManager.UIManager.UpdateShotgunShells(PlayerManager.Instance.shotgunShells);

        chargeLevel = 0;
        chargeLocked = false;
        GameManager.UIManager.EnableChargeUpButton();
    }
}























