using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{		
	PlayerMovement m_Player;
	BaseManager[] m_PlayerBases;	
	WaveManager m_WaveManager;
	HUD m_HUD;

	bool m_GameLaunched;
	
	// Use this for initialization
	void Start () 
	{		
		m_HUD = FindObjectOfType<HUD>();

		m_WaveManager = FindObjectOfType<WaveManager>();		
		m_WaveManager.gameObject.SetActive (false);
		
		m_Player = FindObjectOfType<PlayerMovement>();
		
		m_PlayerBases = FindObjectsOfType<BaseManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_GameLaunched)
		{			
			if(!m_Player.gameObject.activeSelf)
			{
				m_WaveManager.Stop();
			}
			
			if(m_WaveManager.IsGameOver())
			{
				if(m_WaveManager.RemainingEnemies() == 0)
				{
					m_HUD.EndGame (true);
				}
				else
				{
					m_HUD.EndGame (false);
				}
				
				if(Input.GetMouseButtonDown(1))
				{
					LaunchGame ();
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
	
	void LaunchGame()
	{		
		m_GameLaunched = true;

		m_WaveManager.gameObject.SetActive (true);
		m_WaveManager.Reset();
		
		m_Player.gameObject.SetActive(true);
		
		for(int i = 0; i < m_PlayerBases.Length; i++)
		{
			m_PlayerBases[i].Reset();
		}

		m_HUD.LaunchGame();
	}
}
