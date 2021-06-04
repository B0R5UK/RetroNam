using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI 
{
    void UpdateActions();

    void ToAttackState();
    void ToChaseState();



}
