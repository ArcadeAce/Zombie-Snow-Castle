using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject barrel;
    private ShootSound shootsound;
    private Quaternion rotation;
    private bool instantiated;
    private float ZRotation;

    public override void Start()
    {
        base.Start();

        UnityEngine.Debug.Log($"[Shotgun] Start() on '{name}'. Initializing references + rotation.");

        shootsound = GetComponent<ShootSound>();
        UnityEngine.Debug.Log($"[Shotgun] ShootSound component found? {(shootsound != null ? "YES" : "NO")}");

        if (barrel == null)
        {
            UnityEngine.Debug.LogError($"[Shotgun] ERROR: Barrel reference is NULL on '{name}'. Assign it in the inspector!");
        }
        else
        {
            UnityEngine.Debug.Log($"[Shotgun] Barrel assigned: '{barrel.name}'. Applying initial rotation.");
        }

        ZRotation = -90f;
        rotation = Quaternion.Euler(0f, -90f, ZRotation);

        if (barrel != null)
        {
            barrel.transform.localRotation = rotation;
            UnityEngine.Debug.Log($"[Shotgun] Initial barrel rotation set -> Euler(0, -90, {ZRotation}).");
        }

        instantiated = true;
        UnityEngine.Debug.Log($"[Shotgun] Initialization complete. instantiated={instantiated}");
    }

    public void SpinBarrel()
    {
        UnityEngine.Debug.Log($"[Shotgun] SpinBarrel() called. Starting Spin() coroutine.");
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        UnityEngine.Debug.Log($"[Shotgun] Spin() started. Waiting 0.5s before triggering animation.");

        yield return new WaitForSeconds(0.5f);

        if (_animator == null)
        {
            UnityEngine.Debug.LogWarning($"[Shotgun] WARNING: _animator is NULL. Cannot trigger 'Charge Up'.");
        }
        else
        {
            UnityEngine.Debug.Log($"[Shotgun] Triggering animator: 'Charge Up'.");
            _animator.SetTrigger("Charge Up");
        }

        UnityEngine.Debug.Log($"[Shotgun] Waiting 0.1s before playing spin sound.");
        yield return new WaitForSeconds(0.1f);

        if (shootsound == null)
        {
            UnityEngine.Debug.LogWarning($"[Shotgun] WARNING: ShootSound is NULL. Cannot play spin sound.");
        }
        else
        {
            UnityEngine.Debug.Log($"[Shotgun] Playing spin sound.");
            shootsound.PlaySpinSound();
        }

        float oldZ = ZRotation;
        ZRotation += 72f;
        rotation = Quaternion.Euler(0f, -90f, ZRotation);

        UnityEngine.Debug.Log($"[Shotgun] Barrel target rotation updated: Z {oldZ} -> {ZRotation} (added 72).");
        UnityEngine.Debug.Log($"[Shotgun] New target rotation set (Quaternion) - FixedUpdate will slerp toward it.");
    }

    // ⭐ FINAL FIXED SHOTGUN SHOOT()
    public override void Shoot()
    {
        UnityEngine.Debug.Log($"[Shotgun] Shoot() called on '{name}'. About to base.Shoot() (raycast/damage logic).");

        // 🔫 Fire raycast + damage using Weapon.cs logic
        base.Shoot();

        // 🔫 Consume shotgun ammo
        if (PlayerManager.Instance == null)
        {
            UnityEngine.Debug.LogError($"[Shotgun] ERROR: PlayerManager.Instance is NULL. Cannot consume ammo or update UI.");
            return;
        }

        int before = PlayerManager.Instance.shotgunShells;
        PlayerManager.Instance.shotgunShells--;
        if (PlayerManager.Instance.shotgunShells < 0)
            PlayerManager.Instance.shotgunShells = 0;

        int after = PlayerManager.Instance.shotgunShells;
        UnityEngine.Debug.Log($"[Shotgun] Ammo consumed: {before} -> {after} (clamped at 0).");

        // 🔄 Update UI
        if (GameManager.UIManager == null)
        {
            UnityEngine.Debug.LogWarning($"[Shotgun] WARNING: GameManager.UIManager is NULL. UI will not update.");
        }
        else
        {
            UnityEngine.Debug.Log($"[Shotgun] Updating UI shotgun shells to: {after}");
            GameManager.UIManager.UpdateShotgunShells(after);
        }
    }

    private void FixedUpdate()
    {
        if (!instantiated)
            return;

        if (barrel == null)
            return;

        // (No log here to avoid spamming every physics tick)
        barrel.transform.localRotation = Quaternion.Slerp(
            barrel.transform.localRotation,
            rotation,
            Time.deltaTime * 5f
        );
    }
}



















