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

	DigitManager[] m_ScoreDisplay;

	WaveManager m_WaveManager;

	int m_Score;

	float m_CurrentDisplayedScore;

	bool m_GameLaunched;

	// Use this for initialization
	void Start () 
	{
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

	public void LaunchGame()
	{
		m_TitleMessage.SetActive(false);
		m_StartMessage.SetActive(false);
		m_ScoreMessage.SetActive (true);
		m_WinMessage.SetActive(false);
		m_LoseMessage.SetActive (false);

		m_GameLaunched = true;

		m_ScoreDisplay = GetComponentsInChildren<DigitManager>();
		UpdateDisplay ();
		
		m_WaveManager = FindObjectOfType<WaveManager>();
	}

	void SetSplashScreen()
	{
		m_TitleMessage.SetActive(true);
		m_StartMessage.SetActive(true);
		m_ScoreMessage.SetActive (false);
		m_WinMessage.SetActive(false);
		m_LoseMessage.SetActive (false);
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
