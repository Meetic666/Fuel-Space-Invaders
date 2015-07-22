using UnityEngine;
using System.Collections;

// WaveManager handles the behaviour of the enemy wave during the game and when the game is over
public class WaveManager : MonoBehaviour 
{
	#region Members Open For Design
	public float m_MinHorizontalSpeed;
	public float m_MaxHorizontalSpeed;
	public float m_VerticalDrop;
	public float m_AnimationSwitchTime;
	public float m_GameOverDisappearanceTime;
	#endregion

	#region Private Members
	int m_Direction = -1;
	bool m_ChangingDirection;

	AnimationManager[] m_Enemies;

	int m_MaxEnemyNumber;

	float m_AnimationSwichTimer;
	float m_GameOverDisappearanceTimer;

	bool m_GameOver;

	int m_FinalNumberOfEnemies;

	Vector3 m_InitialPosition;
	#endregion

	#region Unity Hooks
	void Awake()
	{
		m_Enemies = GetComponentsInChildren<AnimationManager>();
		
		m_MaxEnemyNumber = m_Enemies.Length;
		
		m_AnimationSwichTimer = m_AnimationSwitchTime;
		
		m_GameOverDisappearanceTimer = m_GameOverDisappearanceTime;
		
		m_InitialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_GameOver)
		{
			UpdateWaveForGame();
		}
		else
		{
			UpdateWaveForGameOver();
		}
	}
	#endregion

	#region Public Methods
	public void Stop()
	{
		// Sets final number of enemies to current number of enemies
		// Kept trac of for score
		m_FinalNumberOfEnemies = RemainingEnemies();

		// Sets game to be over :-(
		m_GameOver = true;
	}

	// ChangeDirection is called when an enemy crossed the bounds of the screen
	public void ChangeDirection()
	{
		// Checks to see if it's not already changing direction
		// Prevents changing direction multiple times at once, making it useless
		if(!m_ChangingDirection)
		{
			m_Direction *= -1;

			m_ChangingDirection = true;

			// Makes the wave drop one level, to simulate the relentless approach of the invaders
			transform.position -= Vector3.up * m_VerticalDrop;
		}
	}

	// Returns number of dead enemies
	public int DeadEnemies()
	{
		return m_MaxEnemyNumber - RemainingEnemies();
	}

	// Returns number of remaining enemies
	public int RemainingEnemies()
	{
		int result = 0;

		// If game is not over
		// Then it calculates the number of active enemies
		// Else it returns the final number of enemies before game was over
		if(!m_GameOver)
		{
			for(int i = 0; i < m_Enemies.Length; i++)
			{
				if(m_Enemies[i].gameObject.activeSelf)
                {
                    result++;
                }
			}
		}
		else
		{
			result = m_FinalNumberOfEnemies;
		}

		return result;
	}

	public bool IsGameOver()
	{
		return m_GameOver;
	}

	// Resets the enemies to be active and the position of the wave to the initial position
	public void Reset()
	{
		for(int i = 0; i < m_Enemies.Length; i++)
		{
			m_Enemies[i].gameObject.SetActive(true);
		}

		transform.position = m_InitialPosition;

		m_GameOver = false;
	}
	#endregion

	#region Private Methods
	void UpdateWaveForGame()
	{
		// Resets m_ChangingDirection to false so that it may change direction again
		m_ChangingDirection = false;
		
		int remainingEnemies = RemainingEnemies();
		
		// If remaining enemies is null, then the wave is complete and needs to stop
		// Else it goes on
		if(remainingEnemies == 0.0f)
		{
			Stop ();
		}
		else
		{
			// Calculates speed to use according to the number of remaining enemies
			// All enemies present means min speed
			// None means max speed
			float actualSpeed = Mathf.Lerp (m_MaxHorizontalSpeed, m_MinHorizontalSpeed, (float) remainingEnemies / (float) m_MaxEnemyNumber);
			
			transform.position += Vector3.right * m_Direction * actualSpeed * Time.deltaTime;
			
			// Updates the animation switch timer using the actual speed used
			// The enemies animate faster when they are faster
			m_AnimationSwichTimer -= actualSpeed * Time.deltaTime;
            
            if(m_AnimationSwichTimer < 0.0f)
            {
                SwitchAnimation();
                
                m_AnimationSwichTimer = m_AnimationSwitchTime;
            }
        }
    }

	void UpdateWaveForGameOver()
	{
		// If game is over, enemies disappear at a certain rate
		// in order to free the screen
		m_GameOverDisappearanceTimer -= Time.deltaTime;
		
		if(m_GameOverDisappearanceTimer < 0.0f)
		{
			DisableNextEnemy();
			
			m_GameOverDisappearanceTimer = m_GameOverDisappearanceTime;
        }
    }
    
    void SwitchAnimation()
	{
		// Switches animation for all enemies
		for(int i = 0; i < m_Enemies.Length; i++)
		{
			m_Enemies[i].SwitchAnimation();
		}
	}

	void DisableNextEnemy()
	{
		bool objectFound = false;

		// Searches for next enemy to disable
		// Stops to look after one is found
		for(int i = 0; i < m_Enemies.Length; i++)
		{
			if(!objectFound && m_Enemies[i].gameObject.activeSelf)
			{
				m_Enemies[i].gameObject.SetActive(false);

				objectFound = true;
			}
		}
	}
	#endregion
}
