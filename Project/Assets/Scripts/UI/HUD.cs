using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
	public int m_SingleEnemyScore;
	public float m_ScoreChangePerSecond;

	public GameObject m_WinMessage;
	public GameObject m_LoseMessage;

	PlayerMovement m_Player;

	DigitManager[] m_ScoreDisplay;

	WaveManager m_WaveManager;

	int m_Score;

	float m_CurrentDisplayedScore;

	// Use this for initialization
	void Start () 
	{
		m_ScoreDisplay = GetComponentsInChildren<DigitManager>();

		m_WaveManager = FindObjectOfType<WaveManager>();

		m_Player = FindObjectOfType<PlayerMovement>();

		UpdateDisplay();
	}
	
	// Update is called once per frame
	void Update () 
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
				Application.LoadLevel(Application.loadedLevel);
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
