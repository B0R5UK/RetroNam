using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyAI
{

    EnemyStates enemy;

    public ChaseState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void ToAttackState()
    {
        Debug.Log("To attack state!");
        enemy.currentState = enemy.attackState;
    }

    public void ToChaseState()
    {
        //enemy.currentState = enemy.chaseState;
    }

    public void UpdateActions()
    {
        Chase();
    }


    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        if (Vector3.Distance(enemy.chaseTarget.transform.position, enemy.transform.position) <= enemy.attackRange)
        {
            enemy.navMeshAgent.isStopped = true;
            enemy.myController.rb.isKinematic = true;//zeby animacja chodzenia sie ogarnela :)
            ToAttackState();
        }
            
    }


}
