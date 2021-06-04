using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyAI
{
    EnemyStates enemy;
    float timer;

    public AttackState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void ToAttackState()
    {
        
    }

    public void ToChaseState()
    {
        Debug.Log("To chase state!");        
        enemy.navMeshAgent.isStopped = false;
        enemy.myController.rb.isKinematic = false;
        enemy.currentState = enemy.chaseState;
    }

    public void UpdateActions()
    {
        timer += Time.deltaTime;

        float distance = Vector3.Distance(enemy.chaseTarget.transform.position, enemy.transform.position);

        if (distance > enemy.attackRange)
            ToChaseState();
        else if (timer >= enemy.attackDelay)
        {
            Attack();
            timer = 0;
        }
    }

    void Attack()
    {
        enemy.myController.animator.SetTrigger("shooting");
        GameObject missle = GameObject.Instantiate(GameController.Instance.bullet, enemy.transform.position, Quaternion.identity);
        missle.GetComponent<Missle>().SetDamageSpeed(enemy.damageDealt, enemy.missleSpeed);
    }

}
