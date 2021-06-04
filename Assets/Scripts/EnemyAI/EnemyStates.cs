using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public Enemy myController;
    public Transform chaseTarget;
    public List<Transform> waypoints;
    public int eyesRange;
    public NavMeshAgent navMeshAgent;

    public int attackRange;
    public Transform eyes;
    public bool isDead;
    public int attackDelay;
    public int damageDealt;
    public float missleSpeed;


    public IEnemyAI currentState;
    //stany przeciwnikow
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public ChaseState chaseState;

    private void Start()
    {
        patrolState = new PatrolState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);



        currentState = patrolState;
    }

    private void Update()
    {
        if(!isDead)
            currentState.UpdateActions();
    }


}
