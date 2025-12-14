
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButtonTouch : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var weapon = PlayerManager.Instance.WeaponSwitcher.activeWeapon;
        if (weapon != null && PlayerManager.Instance.bulletsInClip > 0)
        {
            weapon.Shoot();
            
        }
    }
}

