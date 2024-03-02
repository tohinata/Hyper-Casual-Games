using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    GameObject playerGO;
    PlayerController playerScript;
    bool isObstacleUsed;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && isObstacleUsed == false)
		{
            isObstacleUsed = true;
            playerScript.TouchedToObstacle();
        }
	}
}
