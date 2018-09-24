using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowControl : MonoBehaviour {

    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    GameObject forceText, angleText, ParametersWindow;
    public static GameObject arrow;
    private Vector3 mousePos;
    private float yMouseDistance, xMouseDistance;
    private bool mouseClicks, hasShot;
    public static float shootingForce;
        
    private void OnEnable()
    {
        hasShot = false;
        mouseClicks = true;
        arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
        arrow.transform.parent = transform;
    }
    
    void Update () {

        if (!hasShot)
        {
            if (Input.GetMouseButton(0))
            {                
                if (mouseClicks)
                {
                    mousePos = Input.mousePosition;
                }
                ParametersWindow.SetActive(true);
                GetComponent<ArrowPrediction>().enabled = true;
                mouseClicks = false;
                GetYDistance();
                GetXDistance();
                RotateBow();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ParametersWindow.SetActive(false);
                CameraFollow.whoToFollow = 2;
                GetComponent<ArrowPrediction>().enabled = false;
                mouseClicks = true;
                Shoot();
            }
        }
	}
        
    void GetYDistance()
    {
        yMouseDistance = Input.mousePosition.y - mousePos.y;
    }

    void GetXDistance()
    {
        if (Input.mousePosition.x - mousePos.x < 0 && -(Input.mousePosition.x - mousePos.x)*5 < 1500) {
            xMouseDistance = Input.mousePosition.x - mousePos.x;
            shootingForce = -xMouseDistance*5;
            forceText.GetComponent<Text>().text = "Force: " + shootingForce / 50;
        }
    }    

    void RotateBow()
    {
        if (yMouseDistance < 180 && yMouseDistance > -180)
        {
            arrow.GetComponent<Rigidbody2D>().rotation = -(yMouseDistance / 2);
            angleText.GetComponent<Text>().text = "Angle: " + arrow.GetComponent<Rigidbody2D>().rotation + "°" ;
        }
    }

    public void Shoot()
    {
        hasShot = true;
        arrow.GetComponent<Rigidbody2D>().gravityScale = 1;
        arrow.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * shootingForce);
        
        ArrowBehaviour.inTheAir = true;
    }

}
