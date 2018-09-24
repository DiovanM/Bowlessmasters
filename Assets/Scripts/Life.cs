using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

    public delegate void Win();
    public static event Win EnemyDied;
    public delegate void Lose();
    public static event Lose PlayerDied;

    private int life = 100;
    [SerializeField]
    private GameObject HealthBar;
    
    private void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.CompareTag("OldArrow"))
        {
            life -= 40;
            HealthBar.GetComponent<Image>().fillAmount = life/100f;
            if (life < 0)
            {
                if (gameObject.CompareTag("Player"))
                {
                    PlayerDied();
                }
                else
                {
                    EnemyDied();
                }
            }
        }
    }

}
