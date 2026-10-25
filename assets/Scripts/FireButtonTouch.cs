
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButtonTouch : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var weapon = PlayerManager.Instance.WeaponSwitcher.activeWeapon;
Debug.Log("pointer down");
        if (weapon != null && PlayerManager.Instance.bulletsInClip > 0)
        {
Debug.Log("weapon shoot");
            weapon.Shoot();
            
        }
    }
}

