using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour 
{
	public float m_MinHorizontalSpeed;
	public float m_MaxHorizontalSpeed;
	public float m_VerticalDrop;
	public float m_AnimationSwitchTime;
	public float m_GameOverDisappearanceTime;

	int m_Direction = -1;
	bool m_ChangingDirection;

	AnimationManager[] m_Enemies;

	int m_MaxEnemyNumber;

	float m_AnimationSwichTimer;
	float m_GameOverDisappearanceTimer;

	bool m_GameOver;

	int m_FinalNumberOfEnemies;

	Vector3 m_InitialPosition;

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
			m_ChangingDirection = false;

			int remainingEnemies = RemainingEnemies();

			if(remainingEnemies == 0.0f)
			{
				Stop ();
			}
			else
			{
				float actualSpeed = Mathf.Lerp (m_MaxHorizontalSpeed, m_MinHorizontalSpeed, (float) remainingEnemies / (float) m_MaxEnemyNumber);
				
				transform.position += Vector3.right * m_Direction * actualSpeed * Time.deltaTime;
				
				m_AnimationSwichTimer -= actualSpeed * Time.deltaTime;
				
				if(m_AnimationSwichTimer < 0.0f)
				{
					SwitchAnimation();
					
					m_AnimationSwichTimer = m_AnimationSwitchTime;
				}
			}
		}
		else
		{
			m_GameOverDisappearanceTimer -= Time.deltaTime;

			if(m_GameOverDisappearanceTimer < 0.0f)
			{
				DisableNextEnemy();

				m_GameOverDisappearanceTimer = m_GameOverDisappearanceTime;
			}
		}
	}

	public void Stop()
	{
		m_FinalNumberOfEnemies = RemainingEnemies();

		m_GameOver = true;
	}

	public void ChangeDirection()
	{
		if(!m_ChangingDirection)
		{
			m_Direction *= -1;

			m_ChangingDirection = true;

			transform.position -= Vector3.up * m_VerticalDrop;
		}
	}
	
	public int DeadEnemies()
	{
		return m_MaxEnemyNumber - RemainingEnemies();
	}

	public int RemainingEnemies()
	{
		int result = 0;

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

	public void Reset()
	{
		for(int i = 0; i < m_Enemies.Length; i++)
		{
			m_Enemies[i].gameObject.SetActive(true);
		}

		transform.position = m_InitialPosition;

		m_GameOver = false;
	}

	void SwitchAnimation()
	{
		for(int i = 0; i < m_Enemies.Length; i++)
		{
			m_Enemies[i].SwitchAnimation();
		}
	}

	void DisableNextEnemy()
	{
		bool objectFound = false;

		for(int i = 0; i < m_Enemies.Length; i++)
		{
			if(!objectFound && m_Enemies[i].gameObject.activeSelf)
			{
				m_Enemies[i].gameObject.SetActive(false);

				objectFound = true;
			}
		}
	}
}
