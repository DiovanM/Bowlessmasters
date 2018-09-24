using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    public delegate void ChangeTurn();
    public static event ChangeTurn Change;

    Rigidbody2D arrowRigidbody;
    public static bool inTheAir = false;

    private void OnEnable()
    {
        arrowRigidbody = GetComponent<Rigidbody2D>();
    }
       
    void Start () {
        inTheAir = false;
    }
    
    void Update()
    {        
        if (inTheAir) //Atualiza a rotação da flecha para acompanhar o movimento de acordo com as velocidades X e Y
        {            
            float fallAngle = Mathf.Atan2(arrowRigidbody.velocity.y, arrowRigidbody.velocity.x) * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(-transform.eulerAngles.x, transform.eulerAngles.y, fallAngle);            
        }
    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        Change();
        gameObject.transform.SetParent(collision.transform);
        arrowRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        arrowRigidbody.isKinematic = true;
        arrowRigidbody.velocity = new Vector2(0,0);
        inTheAir = false;
        tag = "OldArrow";
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<ArrowBehaviour>().enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

}
