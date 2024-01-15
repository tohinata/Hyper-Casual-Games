using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerSpeed = 5f;
    float movingSiesSpeed = 0.2f;
    Vector3 firstPos;
    Vector3 lastPos;
    float maxXPosition = 4.35f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xValue = 0;
        if (Input.GetMouseButtonDown(0))
		{
            firstPos = Input.mousePosition;
		}
        else if (Input.GetMouseButton(0))
		{
            lastPos = Input.mousePosition;
            float differences = lastPos.x - firstPos.x;
            xValue = differences * movingSiesSpeed;
        }

        if (Input.GetMouseButtonUp(0))
		{
            firstPos = Vector3.zero;
            lastPos = Vector3.zero;
            xValue = 0;
		}
        
        transform.Translate(xValue * Time.deltaTime, 0, playerSpeed * Time.deltaTime);

        float newXValue = Mathf.Clamp(transform.position.x, -maxXPosition, maxXPosition);
        transform.position = new Vector3(newXValue, transform.position.y, transform.position.z);
    }
}
