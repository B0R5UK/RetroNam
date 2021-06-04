using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyAI
{
    EnemyStates enemy;

    [SerializeField]int nextWaypoint = 0;

    public PatrolState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void UpdateActions()
    {
        Watch();
        Patrol();
    }

    void Watch()
    {

        if(Physics.Raycast(enemy.transform.position, -enemy.transform.forward, out RaycastHit hit, enemy.eyesRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                enemy.chaseTarget = hit.transform;
                ToChaseState();
            }
        }
    }

    void Patrol()
    {
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Count;
            enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;

        }
    }



}
