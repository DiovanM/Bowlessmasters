using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTurns : MonoBehaviour {

    [SerializeField]
    GameObject playerBow, enemyBow;

    private void Start()
    {
        ArrowBehaviour.Change += ChangeTurn;
    }

    void ChangeTurn()
    {
        if (enemyBow.GetComponent<EnemyBow>().enabled == true)
        {
            enemyBow.GetComponent<EnemyBow>().enabled = false;
            playerBow.GetComponent<BowControl>().enabled = true;
            CameraFollow.whoToFollow = 0;
        }
        else 
        {
            playerBow.GetComponent<BowControl>().enabled = false;
            enemyBow.GetComponent<EnemyBow>().enabled = true;
            CameraFollow.whoToFollow = 1;
        }

    }

}
