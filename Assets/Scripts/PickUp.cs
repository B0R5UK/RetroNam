using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType { FirstAid, PistolAmmo, ShotgunAmmo, Pistol, Shotgun}

public class PickUp : MonoBehaviour
{
    public PickupType myType;
    [SerializeField] AudioClip pickupSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.globalAudioSource.PlayOneShot(pickupSound);
            GameController.Instance.Player.PickUp(myType);
            Destroy(gameObject);
        }
    }

}
