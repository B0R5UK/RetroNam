using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyStates enemyStates;
    [SerializeField] public Animator animator;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip damageSound;
    [SerializeField] protected AudioClip dieSound;
    [SerializeField] protected Collider myCollider;
    [SerializeField] public Rigidbody rb;
    public int health;
    


  
    private void Update()
    {
        if (rb.velocity.magnitude == 0f)
            animator.SetBool("walking", false);
        else
            animator.SetBool("walking", true);
    }

    public void TakeDamage(int amount, bool bloody = false)
    {
        if(enemyStates.currentState != enemyStates.chaseState)
        {
            enemyStates.chaseTarget = GameController.Instance.Player.transform;
            enemyStates.currentState = enemyStates.chaseState;
        }

        if(health > 0)
        {
            audioSource.PlayOneShot(damageSound);
            health -= amount;


            if (health <= 0)
            {
                if (bloody)
                    animator.SetTrigger("bloodydeath");
                else
                    animator.SetTrigger("death");
                
                audioSource.PlayOneShot(dieSound);


                enemyStates.navMeshAgent.enabled = false;
                enemyStates.isDead = true;
                rb.isKinematic = true;
                myCollider.enabled = false;
            }
        }

         
    }
}
