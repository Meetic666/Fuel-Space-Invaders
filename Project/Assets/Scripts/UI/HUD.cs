using UnityEngine;
using System.Collections;

// HUD manages all displayed object to represent the game state (Splash/Game/End)
public class HUD : MonoBehaviour 
{
	#region Members Open For Design
	public int m_SingleEnemyScore;
	public float m_ScoreChangePerSecond;

	public GameObject m_WinMessage;
	public GameObject m_LoseMessage;
	public GameObject m_ScoreMessage;
	public GameObject m_TitleMessage;
	public GameObject m_StartMessage;

	public AudioSource m_ScoreIncreaseSound;
	#endregion

	#region Private Members
	DigitManager[] m_ScoreDisplay;

	WaveManager m_WaveManager;

	int m_Score;

	float m_CurrentDisplayedScore;

	bool m_GameLaunched;
	#endregion

	#region Unity Hooks
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

			// Checks if the score displayed needs to be updated on the HUD
			if(previousScore != m_CurrentDisplayedScore)
			{
				UpdateDisplay();

				if(!m_ScoreIncreaseSound.isPlaying)
				{
					m_ScoreIncreaseSound.Play ();
					m_ScoreIncreaseSound.loop = true;
				}
			}
			else
			{
				m_ScoreIncreaseSound.loop = false;
			}
		}
	}
	#endregion

	#region Public Methods
	// EndGame is used to set the HUD to display the proper Win/Lose message
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

	// LaunchGame is used to display the proper HUD for in game
	public void LaunchGame()
	{
		m_TitleMessage.SetActive(false);
		m_StartMessage.SetActive(false);
		m_ScoreMessage.SetActive (true);
		m_WinMessage.SetActive(false);
		m_LoseMessage.SetActive (false);

		m_GameLaunched = true;

		if(m_ScoreDisplay == null)
		{
			m_ScoreDisplay = GetComponentsInChildren<DigitManager>();
		}

		UpdateDisplay ();
		
		m_WaveManager = FindObjectOfType<WaveManager>();
	}
	#endregion

	#region Private Methods
	// SetSplashScreen sets the HUD to display the proper splash screen
	void SetSplashScreen()
	{
		m_TitleMessage.SetActive(true);
		m_StartMessage.SetActive(true);
		m_ScoreMessage.SetActive (false);
		m_WinMessage.SetActive(false);
		m_LoseMessage.SetActive (false);
	}

	// UpdateDisplay parses the score to display in order for the digits to be set properly
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
	#endregion
}
