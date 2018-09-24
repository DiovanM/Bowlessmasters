using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : MonoBehaviour {

    [SerializeField]
    private GameObject arrowPrefab;
    private GameObject arrow;

    public float shootForce;

    private void OnEnable()
    {
        arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
        arrow.transform.parent = transform;
        arrow.transform.rotation = Quaternion.Euler(0,0,180);
        StartCoroutine(RotateAndShoot());        
    }
   
    public void Shoot()
    {
        //Calcula a força necessária para acertar o player com um ângulo de 45 graus baseado na distância entre os dois
        float distanceBetween = transform.position.x - (GameObject.FindGameObjectWithTag("Player").transform.position.x);
        shootForce = 50 * Mathf.Sqrt(9.81f * distanceBetween);

        CalculateMiss();

        arrow.GetComponent<Rigidbody2D>().gravityScale = 1;
        arrow.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * shootForce);

        ArrowBehaviour.inTheAir = true;
    }

    private void CalculateMiss()
    {
        int chance = Random.Range(0, 3);
        if (chance == 0 || chance == 1)
        {
            shootForce += Random.Range(-250, 250);
        }
    }

    IEnumerator RotateAndShoot()
    {        
        while (arrow.transform.rotation.eulerAngles.z > 135)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, arrow.transform.eulerAngles.z - 30 * Time.deltaTime);
            yield return null;
        }
        CameraFollow.whoToFollow = 2;
        Shoot();
        yield return null;
    }

}
