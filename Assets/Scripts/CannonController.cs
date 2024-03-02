using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBallGO;
    public Transform cannonBallSpawnTransform;
    float cannonBallSpeed = 13f;
    bool isShootingOn;

    // Start is called before the first frame update
    void Start()
    {
        isShootingOn = true;
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shooting()
	{
        float delayTime = Random.Range(0.5f, 1.5f);
        while (isShootingOn)
		{
            yield return new WaitForSeconds(delayTime);
            Shoot();
            yield return new WaitForSeconds(1.5f);
		}
	}
    void Shoot()
	{
        GameObject cannonBall = Instantiate(cannonBallGO, cannonBallSpawnTransform.position, Quaternion.identity);
        Rigidbody cannonRB = cannonBall.GetComponent<Rigidbody>();
        cannonRB.velocity = -transform.forward * cannonBallSpeed;
	}
}
