using UnityEngine;
using System.Collections;

// Game Manager takes care of setting the objects in the scene to act according
// to the state the game is in (Splash/Game/End)
public class GameManager : MonoBehaviour 
{		
	#region Private Members
	PlayerMovement m_Player;
	BaseManager[] m_PlayerBases;	
	WaveManager m_WaveManager;
	HUD m_HUD;

	bool m_GameLaunched;
	#endregion

	#region Unity Hooks
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
		// Checks whether the game is launched or still in splash
		if(m_GameLaunched)
		{			
			UpdateGame ();
		}
		else
		{
			UpdateSplash ();
		}
	}
	#endregion

	#region Private Methods
	// LaunchGame sets the object for the in-game state
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

	// Update Splash looks for the input to go in-game
	void UpdateSplash()
	{
		CheckStartInput();
    }

	// UpdateGae is run when in-game
	void UpdateGame()
	{
		// If the player dies
		// Then the wave manager is told to stop
		if(!m_Player.gameObject.activeSelf)
		{
			m_WaveManager.Stop();
		}

		// If wave manager is stopped (meaning game over)
		// Then the HUD is told to display the appropriate win/lose screen
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
            
			CheckStartInput();
        }
    }

	void CheckStartInput()
	{
		// Press right mouse button to start
		if(Input.GetMouseButtonDown(1))
		{
			LaunchGame ();
		}
	}
	#endregion
}
