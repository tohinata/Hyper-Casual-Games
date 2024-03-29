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
	bool isPlayerMoving = false;
	float maxPlayerScale = 2.5f;
	float minPlayerScale = 0.8f;
	float diamondValue = 0.2f;
	float obstacleDamageValue = 0.3f;
	public GameObject diamondPartical;
	public GameObject obstaclePartical;
	private Animator PlayerAC;
	public AudioSource playerAudioSource;
	public AudioClip obstacleClip, diamondClip, congratesClip, failedClip;

	// Start is called before the first frame update
	void Start()
	{
		PlayerAC = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isPlayerMoving)
			return;


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

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "FinishLine")
		{
			isPlayerMoving = false;
			StartIdleAnimation();
			GameManager.instance.ShowSucessMenuPanel();
			PlayAudio(congratesClip);
		}

		if (other.tag == "CannonBall")
		{
			if (isPlayerMoving == true)
			{
				PlayerGotHurt();
				Destroy(other.gameObject);
			}
		}
	}

	public void TouchedToDiamond()
	{
		Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 1.5f);
		GameObject partical = Instantiate(diamondPartical, effectPosition, Quaternion.identity);
		Destroy(partical, 2f);
		GetBigger();
		PlayAudio(diamondClip);
	}

	public void TouchedToObstacle()
	{
		PlayerGotHurt();
	}

	public void PlayerGotHurt()
	{
		Vector3 obstalceEffectPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 1.5f);
		GameObject partical = Instantiate(obstaclePartical, obstalceEffectPos, Quaternion.identity);
		Destroy(partical, 2f);
		GetSmaller();
		PlayAudio(obstacleClip);
	}

	private void GetBigger()
	{
		transform.localScale = new Vector3(
			transform.localScale.x + diamondValue,
			transform.localScale.y + diamondValue,
			transform.localScale.z + diamondValue);

		if (transform.localScale.x > maxPlayerScale)
		{
			transform.localScale = new Vector3(maxPlayerScale, maxPlayerScale, maxPlayerScale);
		}
	}

	private void GetSmaller()
	{
		transform.localScale = new Vector3(
			transform.localScale.x - obstacleDamageValue,
			transform.localScale.y - obstacleDamageValue,
			transform.localScale.z - obstacleDamageValue);

		if (transform.localScale.x < minPlayerScale)
		{
			transform.localScale = new Vector3(minPlayerScale, minPlayerScale, minPlayerScale);
			GameManager.instance.ShowFailedMenuPanel();
			PlayAudio(failedClip);
			StartPlayerMoving();
		}
	}

	public void StartPlayerMoving()
	{
		isPlayerMoving = true;
		StartRunAnimation();
	}

	public void StopPlayerMoving()
	{
		isPlayerMoving = false;
		StartIdleAnimation();
	}

	private void StartRunAnimation()
	{
		PlayerAC.SetBool("isPlayerRunning", true);
	}

	private void StartIdleAnimation()
	{
		PlayerAC.SetBool("isPlayerRunning", false);
	}

	void PlayAudio(AudioClip audioClip)
	{
		if (playerAudioSource != null)
		{
			playerAudioSource.PlayOneShot(audioClip, 1);
		}
	}
}
