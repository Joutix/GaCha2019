using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class GameOverUiManager : MonoBehaviour
{
	private float m_Timer;
	private int m_MenuButtonIndex;

	[SerializeField]
	private GameObject[] m_MenuButtons;

	private GamePadState m_PreviousState;
	private GamePadState m_CurrentState;
	[SerializeField] private PlayerIndex m_ControllerInpause;

	private void Start()
	{
		m_MenuButtons[m_MenuButtonIndex].GetComponent<Image>().color = Color.red; 
	}

	private void UpdateControllerState()
	{
		// Update the states
		m_PreviousState = m_CurrentState;
		m_CurrentState = GamePad.GetState(m_ControllerInpause);
	}

	private bool IsStartPressed()
	{
		return m_PreviousState.Buttons.Start == ButtonState.Released && m_CurrentState.Buttons.Start == ButtonState.Pressed;
	}

	private bool IsAPressed()
	{
		return m_PreviousState.Buttons.A == ButtonState.Released && m_CurrentState.Buttons.A == ButtonState.Pressed;
	}

	private bool IsBPressed()
	{
		return m_PreviousState.Buttons.B == ButtonState.Released && m_CurrentState.Buttons.B == ButtonState.Pressed;
	}


	

	private void Update()
	{
		m_Timer += Time.deltaTime;

		UpdateControllerState();
		
		if (IsAPressed() && m_MenuButtonIndex == 0)
		{
			Quit();
		}
	}

	public void Quit()
	{
		Debug.Log("Quitting...");
		SceneManager.LoadScene("MenuScene");
	}
}
