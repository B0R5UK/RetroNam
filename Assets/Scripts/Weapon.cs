using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {Pistol, Shotgun, Fist }

public class Weapon : MonoBehaviour
{
    public WeaponType type;
    public bool isUnlocked;
    public int weaponDamage;
    public float weaponRange;


    public int magSize;
    public int startingAmmo;
    public int currentAmmo;
    public int currentMagAmmo;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
    [SerializeField]protected AudioSource audioSource;
    [SerializeField]protected Animator animator;
    public bool isShooting;
    public bool isReloading;


    protected virtual void Start()
    {
        currentAmmo = startingAmmo;
        currentMagAmmo = magSize;
    }

    protected virtual void Update()
    {
        if (!GameController.Instance.isPaused && !isShooting && !isReloading)
        {

            if (Input.GetKeyDown(KeyCode.R) && currentMagAmmo != magSize)
                Reload();
            else if (Input.GetButtonDown("Fire1"))
            {
                if (currentMagAmmo > 0)
                    Shot();
                else
                    Reload();
            }
        }
    }

    protected virtual void Shot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2,0));

        currentMagAmmo--;
        audioSource.PlayOneShot(shotSound);
        animator.SetTrigger("Shooting");

        if (Physics.Raycast(ray, out RaycastHit hit, weaponRange))
        {
            Debug.Log("trafiono : " + hit.collider.gameObject.name);

            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

            if (!enemy)
                SetBullethole(hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            else
            {
                Instantiate(GameController.Instance.blood, hit.point, Quaternion.identity);

                if (type == WeaponType.Pistol)
                    enemy.TakeDamage(weaponDamage, false);
                else if (type == WeaponType.Shotgun)
                    enemy.TakeDamage(weaponDamage, true);
            }
                
        }
    }

    protected virtual void SetBullethole(Vector3 pos, Quaternion rot)
    {
        Instantiate(GameController.Instance.bulletHole, pos, rot);
    }

    protected virtual void Reload()
    {
        int neededToReload = magSize - currentMagAmmo;

        if (currentAmmo > 0)
        {
            audioSource.PlayOneShot(reloadSound);
            animator.SetTrigger("Reloading");

            if (currentAmmo >= neededToReload)
            {
                currentAmmo -= neededToReload;
                currentMagAmmo = magSize;
            }
            else if (currentAmmo < neededToReload && currentAmmo > 0)
            {
                currentMagAmmo += currentAmmo;
                currentAmmo = 0;
            }
        }
        else
        {
            audioSource.PlayOneShot(emptySound);
        }

    }


}
