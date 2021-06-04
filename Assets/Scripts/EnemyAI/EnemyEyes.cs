using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour
{

    private void Update()
    {
        transform.LookAt(GameController.Instance.Player.transform);
    }


}
