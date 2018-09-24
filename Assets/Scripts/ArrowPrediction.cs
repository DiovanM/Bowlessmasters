using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPrediction : MonoBehaviour {

    [SerializeField]
    GameObject predictionPathBallPrefab;
    GameObject[] ball = new GameObject[20];
    [SerializeField]   
    private float timeBetweenInstants = 0.1f;
    private float time = 0;
    private int ballsAmount;
    private float angle, force, xForce, yForce;
    private Vector3 arrowPosition;

    private void OnEnable()
    {
        ballsAmount = ball.Length;
        time = 0;
        for (int i = 0; i < ballsAmount; i++)
        {
            ball[i] = predictionPathBallPrefab;
            ball[i] = Instantiate(ball[i], transform.position, Quaternion.identity) as GameObject;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < ballsAmount; i++)
        {
            Destroy(ball[i]);
        }
    }

    // Update is called once per frame
    void Update () {
        force = BowControl.shootingForce/50;
        angle = BowControl.arrow.GetComponent<Rigidbody2D>().rotation;
        arrowPosition = BowControl.arrow.transform.position;
        xForce = force * Mathf.Cos(angle * Mathf.Deg2Rad);
        yForce = force * Mathf.Sin(angle * Mathf.Deg2Rad);        

        for(int i = 0; i<ballsAmount; i++)
        {
            float xPos = (xForce * time + arrowPosition.x);
            float yPos = (yForce * time + arrowPosition.y - (9.81f*time*time)/2);
            
            ball[i].transform.position = new Vector2(xPos, yPos);
            if (ball[i].transform.position.y <= -3)
            {
                ball[i].SetActive(false);
            } else
            {
                ball[i].SetActive(true);
            }
            time += timeBetweenInstants;
        }
        time = 0;

    }
}
