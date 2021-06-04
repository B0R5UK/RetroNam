using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]List<Weapon> weapons;


    [SerializeField] AudioClip playerHit;
    [SerializeField] AudioSource audioSource;

    public Weapon currentWeapon;

    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        //currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        audioSource.PlayOneShot(playerHit);
        GameController.Instance.TakeHit();
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            GameController.Instance.ToggleDiePanel();
        }

    }


    private void Update()
    {
        if (!GameController.Instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(WeaponType.Fist);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(WeaponType.Pistol);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(WeaponType.Shotgun);
            }
        }
    }

    public void ChangeWeapon(WeaponType type)
    {
        Weapon foundWeapon = weapons.Find(x => x.type == type);

        if (foundWeapon && foundWeapon.isUnlocked)
        {
            foreach (Weapon weapon in weapons)
                weapon.gameObject.SetActive(false);

            foundWeapon.gameObject.SetActive(true);

            currentWeapon = foundWeapon;

            GameController.Instance.UpdateWeaponIcon();
        }       
    }

    public void PickUp(PickupType type)
    {
        if(type == PickupType.FirstAid)
        {
            currentHealth += 50;
            Mathf.Clamp(currentHealth, 0, maxHealth);
        }else if (type == PickupType.PistolAmmo)
        {
            foreach(Weapon weapon in weapons)
            {
                if (weapon.isUnlocked && weapon.type == WeaponType.Pistol)
                    weapon.currentAmmo =+ 10;
            }
        }else if (type == PickupType.ShotgunAmmo)
        {
            foreach (Weapon weapon in weapons)
            {
                if (weapon.isUnlocked && weapon.type == WeaponType.Shotgun)
                    weapon.currentAmmo = +7;
            }
        }else if (type == PickupType.Shotgun)
        {
            foreach (Weapon weapon in weapons)
            {
                if (weapon.type == WeaponType.Shotgun)
                {
                    weapon.isUnlocked = true;
                    ChangeWeapon(WeaponType.Shotgun);
                }
                    
            }
        }
        else if (type == PickupType.Pistol)
        {
            foreach (Weapon weapon in weapons)
            {
                if (weapon.type == WeaponType.Pistol)
                {
                    weapon.isUnlocked = true;
                    ChangeWeapon(WeaponType.Pistol);
                }
                    
            }
        }

    }

}
