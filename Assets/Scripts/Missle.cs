using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    int lifetime = 15;
    public float speed;
    public int damage;
    float timer;
    Transform target;

    private void Start()
    {
        target = GameController.Instance.Player.transform;
        transform.LookAt(target);
        
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
            Destroy(gameObject);


        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.Player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetDamageSpeed(int damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

}
