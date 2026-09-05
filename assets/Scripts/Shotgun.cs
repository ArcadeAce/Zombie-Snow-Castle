using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject barrel;          // Barrel reference (assigned in Inspector)
    private ShootSound shootsound;     // Sound system for firing + spin sound

    // ⭐ Smooth rotation settings
    public float barrelRotationSpeed = 200f;   // Adjustable in Inspector
    private bool isSpinning = false;

    // Charge-up system
    private int chargeLevel = 0;
    private const int maxCharge = 3;

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
        if (barrel == null || isSpinning)
            return;

        // Play spin sound
        if (shootsound != null)
            shootsound.PlaySpinSound();

        // Smooth rotation coroutine
        StartCoroutine(SmoothSpin());

        // Increase charge level
        if (chargeLevel < maxCharge)
            chargeLevel++;
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
        // No ammo? No firing.
        if (PlayerManager.Instance.shotgunShells <= 0)
            return;

        Debug.Log($"[Shotgun] Shoot() called on '{name}'. Using base raycast logic.");

        // Fire raycast + damage using Weapon.cs logic
        base.Shoot();

        // Consume ammo
        int before = PlayerManager.Instance.shotgunShells;
        PlayerManager.Instance.shotgunShells--;

        if (PlayerManager.Instance.shotgunShells < 0)
            PlayerManager.Instance.shotgunShells = 0;

        int after = PlayerManager.Instance.shotgunShells;
        Debug.Log($"[Shotgun] Ammo consumed: {before} -> {after}");

        // Update UI
        if (GameManager.UIManager != null)
            GameManager.UIManager.UpdateShotgunShells(after);
        else
            Debug.LogWarning("[Shotgun] UIManager is NULL — cannot update UI.");

        // Reset charge after firing
        chargeLevel = 0;
    }
}





















