using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject Bow;
    private GameObject objectToFollow;
    private Vector3 cameraPos;
    public static int whoToFollow;
    private bool endedCoroutine;

    private void Start()
    {
        endedCoroutine = false;
        whoToFollow = 0;
        cameraPos = transform.position;
        StartCoroutine(InitialCameraMovement(4, 2));
    }
    
    void Update () {

        if (endedCoroutine)
        {
            if (whoToFollow == 0)
            {
                Bow.GetComponent<BowControl>().enabled = true;
                GoToPlayer(2);
            }
            else if (whoToFollow == 1)
            {
                GoToEnemy(2);
            }
            else
            {
                FollowArrow();
            }
        }

    }

    void GoToPlayer(float velocity)
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Player");
        cameraPos.x = objectToFollow.transform.position.x;
        cameraPos.y = objectToFollow.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, cameraPos, velocity * Time.deltaTime);        
    }

    void GoToEnemy(float velocity)
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Enemy");
        cameraPos.x = objectToFollow.transform.position.x;
        cameraPos.y = objectToFollow.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, cameraPos, velocity * Time.deltaTime);
    }

    void FollowArrow()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Arrow");
        cameraPos.x = objectToFollow.transform.position.x;
        cameraPos.y = objectToFollow.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, cameraPos, 1);
    }
   
    IEnumerator InitialCameraMovement(float waitTime, float timeToTransform)
    {        
        float initialTime = waitTime;
        float animationTime = 0;

        while (waitTime > 2)
        {                       
            waitTime -= Time.deltaTime;            

            yield return null;
        }
        animationTime = 0;
        waitTime = initialTime;

        while (waitTime > -1)
        {
            GoToEnemy(animationTime);
            waitTime -= Time.deltaTime;
            animationTime += Time.deltaTime / timeToTransform;

            yield return null;
        }
        animationTime = 0;
        waitTime = initialTime;

        while (waitTime > 0)
        {
            GoToPlayer(animationTime);
            waitTime -= Time.deltaTime;
            animationTime += Time.deltaTime / timeToTransform;

            yield return null;
        }        
        endedCoroutine = true;
        yield return null;
    }

}
