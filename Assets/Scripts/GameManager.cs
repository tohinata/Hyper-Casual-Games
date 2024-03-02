using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject GameMenuPanel;
	public GameObject SuccessMenuPanel;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
            Destroy(this);
		}
        else
		{
            instance = this;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{

	}

	public void StartButtonTapped()
	{
		GameMenuPanel.SetActive(false);
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		PlayerController playerScript = playerGO.GetComponent<PlayerController>();
		playerScript.StartPlayerMoving();		
	}

	public void ShowSucessMenuPanel()
	{
		SuccessMenuPanel.SetActive(true);
	}

	public void RestartButtonTapped()
	{
		SceneManager.LoadScene("SampleScene");
	}
}
