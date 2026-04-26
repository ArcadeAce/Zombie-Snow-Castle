using UnityEngine;
using UnityEngine.EventSystems;

public class TwinTurbos : Weapon
{
    

    public override void Shoot()
    {
        base.Shoot();
        PlayerManager.Instance.bulletsInClip--;
        if (PlayerManager.Instance.bulletsInClip < 0)
            PlayerManager.Instance.bulletsInClip = 0;

        GameManager.UIManager.UpdateBullets(PlayerManager.Instance.bulletsInClip);
        // Additional shooting logic for Twin Turbos
    }
}
// The twin turbos pistols are 2 pistols the player is holding in each hand
// TwinTurbos script inherits from Weapon script


