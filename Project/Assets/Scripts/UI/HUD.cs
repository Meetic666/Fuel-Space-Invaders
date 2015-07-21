using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
	public int m_SingleEnemyScore;
	public float m_ScoreChangePerSecond;

	public GameObject m_WinMessage;
	public GameObject m_LoseMessage;
	public GameObject m_ScoreMessage;
	public GameObject m_TitleMessage;
	public GameObject m_StartMessage;

	PlayerMovement m_Player;

	DigitManager[] m_ScoreDisplay;

	WaveManager m_WaveManager;

	int m_Score;

	float m_CurrentDisplayedScore;

	bool m_GameLaunched;

	// Use this for initialization
	void Start () 
	{
		m_ScoreDisplay = GetComponentsInChildren<DigitManager>();

		m_WaveManager = FindObjectOfType<WaveManager>();

		m_Player = FindObjectOfType<PlayerMovement>();

		UpdateDisplay();

		SetSplashScreen();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_GameLaunched)
		{
			m_Score = m_WaveManager.DeadEnemies() * m_SingleEnemyScore;
			
			float previousScore = m_CurrentDisplayedScore;
			
			m_CurrentDisplayedScore += m_ScoreChangePerSecond * Time.deltaTime;
			
			m_CurrentDisplayedScore = Mathf.Clamp (m_CurrentDisplayedScore, 0.0f, m_Score);
			
			if(previousScore != m_CurrentDisplayedScore)
			{
				UpdateDisplay();
			}
			
			if(!m_Player.gameObject.activeSelf)
			{
				m_WaveManager.Stop();
			}
			
			if(m_WaveManager.IsGameOver())
			{
				if(m_WaveManager.RemainingEnemies() == 0)
				{
					EndGame (true);
				}
				else
				{
					EndGame (false);
				}
				
				if(Input.GetMouseButtonDown(1))
				{
					Reset ();
				}
			}
		}
		else
		{
			if(Input.GetMouseButtonDown(1))
			{
				LaunchGame();
			}
		}
	}

	public void EndGame(bool playerWon)
	{
		if(playerWon)
		{
			m_WinMessage.SetActive(true);
		}
		else
		{
			m_LoseMessage.SetActive (true);
		}

		m_StartMessage.SetActive(true);
	}

	void Reset()
	{
		m_WaveManager.Reset();

		m_StartMessage.SetActive (false);
		m_WinMessage.SetActive(false);
		m_LoseMessage.SetActive (false);

		m_Player.gameObject.SetActive(true);
	}

	void LaunchGame()
	{
		m_WaveManager.gameObject.SetActive (true);
		m_TitleMessage.SetActive(false);
		m_StartMessage.SetActive(false);
		m_ScoreMessage.SetActive (true);

		m_GameLaunched = true;
	}

	void SetSplashScreen()
	{
		m_WaveManager.gameObject.SetActive (false);
		m_ScoreMessage.SetActive(false);
	}

	void UpdateDisplay()
	{
		int scoreToDisplay = (int) m_CurrentDisplayedScore;

		for(int i = m_ScoreDisplay.Length - 1; i >= 0; i--)
		{
			int powerOfTen = (int) Mathf.Pow(10, i);

			int digit = scoreToDisplay / powerOfTen;

			m_ScoreDisplay[i].SetDigit(digit);

			scoreToDisplay -= digit * powerOfTen;
		}
	}
}
