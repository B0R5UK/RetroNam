using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon
{
    public AudioClip hitClip;
    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting && !GameController.Instance.isPaused)
        {
            MeleeHit();
        }
    }


    void MeleeHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        animator.SetTrigger("Hitting");
        audioSource.PlayOneShot(shotSound);

        if (Physics.Raycast(ray, out RaycastHit hit, weaponRange))
        {
            Debug.Log("trafiono : " + hit.collider.gameObject.name);

            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

            if (enemy)
            {
                audioSource.PlayOneShot(hitClip);
                Instantiate(GameController.Instance.blood, hit.point, Quaternion.identity);
                enemy.TakeDamage(weaponDamage, false);
            }

        }



    }

}
