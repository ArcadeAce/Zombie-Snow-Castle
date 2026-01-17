using System;// For Int32
using UnityEngine;// Unity engine core
using UnityEngine.EventSystems;// For EventSystem

public class Weapon : MonoBehaviour

// All these public's (below) show for the Twin Turbos and the Shotgun in the Inpsector Panel.
// These public variables appear in the Inspector for both Twin Turbos and Shotgun.
// Because both weapons INHERIT from Weapon, they automatically get these stats.
// Unity shows public fields in the Inspector so each weapon can have different values.

{
    public int damage;// Damage per shot
    public int clipSize;// Number of bullets per clip
    public int totalBullets;// Total bullets carried

    protected Transform fpsCam;// Reference to the player's camera (protected means child classes can access it) (child classes are scripts that inherit from weapon)
    // Weapon.cs can use fpsCam, TwinTurbos.cs can use fpsCam, Shotgun.cs can use fpsCam, BUT no other script can touch it
    
    // Transform That cam is the Unity Starter Assets camera, the one the player looks through.
    
    //  fpsCam is the Unity Starter Assets FPS camera.

    [HideInInspector] public Animator _animator;// Reference to the weapon's animator

    public int index;// Weapon index (e.g., 0 for Twin Turbos, 1 for Shotgun)
    public bool Active;
    // A bool (short for boolean) is the simplest kind of variable in C#.
    // It can only ever be one of two states:
    // true → yes
    // false → no
    // Is this weapon currently active (equipped)? 

    public bool chestweapon;// Is this weapon obtained from a chest?

    private bool Firing;// It’s a private switch that helps the weapon control its own behavior without interference from other scripts.
    // Why it’s private, because no other script should be allowed to mess with the firing state. Only Weapon.cs controls it.
    // A yes/no switch that ONLY the Weapon script can see or change.
    // Controls if the weapon is currently firing.
    // it exists to prevent the weapon from firing nonstop or firing multiple times per frame.
    public string weaponType;// Weapon type identifier (e.g., "TwinTurbos", "Shotgun")



    // Virtual ammo check — child classes override if needed
    protected virtual bool HasAmmo()
    {
        return PlayerManager.Instance.bulletsInClip > 0;
    }

    // Virtual ammo consumption — child classes override if needed
    protected virtual void ConsumeAmmo()
    {
        PlayerManager.Instance.bulletsInClip--;
        if (PlayerManager.Instance.bulletsInClip < 0)
            PlayerManager.Instance.bulletsInClip = 0;

        GameManager.UIManager.UpdateBullets(PlayerManager.Instance.bulletsInClip);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////// For the raycast shooting to study


    public virtual void Shoot() // public  → This function can be called from ANY script in your project. // virtual → Child classes (like Shotgun or TwinTurbos) are ALLOWED to replace this function. (meaning Twin turbos script and Shotgun script inherit from Weapon script) void    → The function does not return anything; it just performs an action. Shoot() → The name of the function; this is the main firing logic for the weapon.
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f)) // Physics.Raycast Meaning, Unity’s physics engine fires an invisible laser beam.// fpsCam.position means:start the ray from the camera from the weapon prefab this is why your bullets always come from the center of the screen.// 'out hit' tells Unity to fill the 'hit' variable with the impact details if the ray hits something // 1000f is simply the maximum distance your raycast can travel

        {
            
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();// Enemy This is the type of thing we want to find think of it like saying:“I’m looking for the zombie script.”// enemy This is the variable name you’re creating meaning:“Store the zombie script in this variable called enemy.”// = Assignment Operator meaning:“Take whatever is on the right side and put it into the variable on the left side.”// hit This is the RaycastHit data from your raycast meaning: “This is the impact info from the bullet.”//hit.collider This is the exact collider the bullet hit.dot. means: now look inside this object for something else.”//GetComponentInParent<Enemy>() means: Find the Enemy script on the object I hit (or its parent). In my setup, the collider and Enemy script are on the same root object, so this returns the zombie immediately.

            if (enemy != null)// if “Only do something if a condition is true.” “Before I try to damage the zombie, let me check if I actually hit a zombie.”// enemy means This is the variable that stores the Enemy script you found. Earlier you wrote Enemy enemy = hit.collider.GetComponentInParent<Enemy>(); so enemy = the zombie you hit, If you hit a wall → enemy = null, If you hit a zombie → enemy = the Enemy script, in the game enemy represents the zombie boss or regular zombie you shot.//!= “Is NOT equal to.” This is a comparison operator. in the game unity is asking “Is the thing we hit NOT empty?”// null nothing, did not find anything. So in the game A wall → enemy = null a zombie = not null, Meaning:“If I actually found a zombie script (meaning I hit a zombie), then proceed to damage it.” Without this check, Unity would try to damage everything you hit:walls floors, this would cause errors, so this line protect the game from crashing.

            {
                enemy.TakeDamage(damage, hit);// These curly braces define a code block. think of them as “Everything inside these braces belongs to the IF statement.”// enemy This is the variable that stores the Enemy script you found with: so if you hit a zombie → enemy contains the zombie’s Enemy script If you hit a wall → enemy is null. enemy represents the zombie boss or regular zombie you shot.//
            }
        }
    }


    //////////////////////////////////////////////////////////////////////////
 
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        fpsCam = PlayerController.Instance.cam.transform;

        Active = true;

        // Sync UI
        GameManager.UIManager.ReloadGun(
            weaponType,
            PlayerManager.Instance.bulletsInClip,
            PlayerManager.Instance.remainingBullets
        );
    }

    public virtual void Start()
    {
        // Child classes override if needed
    }

    public void Setup(bool chest)
    {
        GameManager.UIManager.ReloadGun(
            weaponType,
            PlayerManager.Instance.bulletsInClip,
            PlayerManager.Instance.remainingBullets
        );
    }

    public virtual void Reload()
    {
        int remainingBullets, bulletsInClip;

        switch (weaponType)
        {
            case "TwinTurbos":
                remainingBullets = PlayerManager.Instance.remainingBullets;
                bulletsInClip = PlayerManager.Instance.bulletsInClip;
                break;

            case "Shotgun":
                remainingBullets = PlayerManager.Instance.remainingShotgunShells;
                bulletsInClip = PlayerManager.Instance.shotgunShells;
                break;

            default:
                return;
        }

        if (remainingBullets <= 0 || bulletsInClip == clipSize)
            return;

        _animator.SetTrigger("Reload");

        int reload = clipSize - bulletsInClip;

        if (remainingBullets >= reload)
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, clipSize);
            PlayerManager.Instance.AddAmmo(weaponType, -reload);
        }
        else
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, remainingBullets);
            PlayerManager.Instance.AddAmmo(weaponType, -remainingBullets);
        }
    }

    private void Update()
    {
        Int32 pointerID;

#if UNITY_EDITOR
        pointerID = -1;
#else
        pointerID = 0;
#endif

        // Fire button pressed
        if (EventSystem.current.IsPointerOverGameObject(pointerID) &&
            EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.gameObject.name == "Fire Button")
            {
                if (!HasAmmo())
                    return;

                Firing = true;
            }
        }

        // Fire button released
        if (Input.GetMouseButtonUp(0))
            Firing = false;

        // Out of ammo
        if (!HasAmmo())
            Firing = false;

        _animator.SetBool("Firegun", Firing);
    }
}













