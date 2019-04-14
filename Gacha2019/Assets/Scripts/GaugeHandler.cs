using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;

public class GaugeHandler : MonoBehaviour
{
    public enum Name { SFX, MUSIC };
    
    public Name e_Type;
    [SerializeField]
    private Color m_FillColor;
    [SerializeField]
    private Sprite m_VolumeBar;
    [SerializeField]
    private List<GameObject> m_Boxes;

    [SerializeField] private PlayerIndex m_ControllerInpause;
    private GamePadState m_PreviousState;
    private GamePadState m_CurrentState;

    private int m_CurrentIndex;

    private Color m_UnselectedColor;
    private bool m_IsSelected = false;
    private float m_Timer;

    void Start()
    {
        m_Timer = 0f;
        if (m_FillColor == default)
            Debug.Log(e_Type.ToString() + " has default fill color");
        Debug.Log(e_Type.ToString()+ " fill color: " + m_FillColor);
        m_UnselectedColor = m_FillColor;
        Debug.Log(e_Type.ToString() + " unselected color: " + m_UnselectedColor);
    }

    void Update()
    {
        if (m_IsSelected)
        {
            m_Timer += Time.unscaledDeltaTime;
            UpdateControllerState();
            if (m_Timer >= 0.2f && (m_CurrentState.ThumbSticks.Left.X > 0.8f || m_CurrentState.ThumbSticks.Left.X < -0.8f))
            {
                // Right
                if(m_CurrentState.ThumbSticks.Left.X > 0.8f)
                {
                    m_CurrentIndex += 1;
                    if (m_CurrentIndex >= m_Boxes.Count)
                        m_CurrentIndex = m_Boxes.Count - 1;
                }
                // Left
                else
                {
                    m_CurrentIndex -= 1;
                    if (m_CurrentIndex < 0)
                    {
                        Mute();
                        return;
                    }
                }
                FillBoxes(m_CurrentIndex, m_FillColor);
                EmptyBoxes(m_CurrentIndex);
                m_Timer = 0f;
                switch (e_Type)
                {
                    case Name.SFX:
                        AudioManager.Instance.s_playSFX = (m_CurrentIndex + 1) / 10f;
                        Debug.Log("Changed SFX volume to " + AudioManager.Instance.s_playSFX);
                        break;
                    case Name.MUSIC:
                        AudioManager.Instance.s_playMusic = (m_CurrentIndex + 1) / 10f;
                        Debug.Log("Changed Music volume to " + AudioManager.Instance.s_playMusic);
                        break;
                }
            }
        }
    }

    // Init values
    public void Init()
    {        
        if (e_Type.Equals(Name.SFX))
        {
            if (AudioManager.Instance.s_playSFX < 1f)
                m_CurrentIndex = (int)(AudioManager.Instance.s_playSFX * 10);
            else
                m_CurrentIndex = 9;
        }
        else if (e_Type.Equals(Name.MUSIC))
        {
            if (AudioManager.Instance.s_playMusic < 1f)
                m_CurrentIndex = (int)(AudioManager.Instance.s_playMusic * 10);
            else
                m_CurrentIndex = 9;
        }
        Debug.Log(e_Type.ToString() + " gauge with index at " + m_CurrentIndex);
    }
    
    public void Select(bool val)
    {
        Color newColor;
        // We just selected the gauge
        if (val)
        {
            // We update its color
            newColor = m_UnselectedColor;
            newColor.a = 1;
        }
        // We unselected the gauge
        else
        {
            // We update its color
            newColor = m_UnselectedColor;
        }
        m_FillColor = newColor;
        FillBoxes(m_CurrentIndex, m_FillColor);
        EmptyBoxes(m_CurrentIndex);
        m_IsSelected = val;
    }

    private void UpdateControllerState()
    {
        // Update the states
        m_PreviousState = m_CurrentState;
        m_CurrentState = GamePad.GetState(m_ControllerInpause);
    }

    // With keyboard controls
    public void OnPointerEnter(GameObject box)
    {
        int index = m_Boxes.IndexOf(box);
        if (index < m_CurrentIndex)
            index = m_CurrentIndex;
        FillBoxes(index, m_FillColor);
    }

    // With keyboard controls
    public void OnPointerExit(GameObject box)
    {
        EmptyBoxes(m_CurrentIndex);
    }

    // With keyboard controls
    public void OnPointerClick(GameObject box)
    {
        m_CurrentIndex = m_Boxes.IndexOf(box);
        FillBoxes(m_CurrentIndex, m_FillColor);
        EmptyBoxes(m_CurrentIndex);
        switch (e_Type)
        {
            case Name.SFX:
                AudioManager.Instance.s_playSFX = (m_CurrentIndex + 1)/10f;
                Debug.Log("Changed SFX volume to " + AudioManager.Instance.s_playSFX);
                break;
            case Name.MUSIC:
				AudioManager.Instance.s_playMusic = (m_CurrentIndex + 1) / 10f;
                Debug.Log("Changed Music volume to " + AudioManager.Instance.s_playMusic);
                break;
        }
    }

    // Fills all the boxes that have an index lower or equal to the given index
    private void FillBoxes(int index, Color color)
    {
        //Debug.Log("Filling "+e_Type.ToString()+" with index: " + index);
        for (int i = 0; i <= index; i++)
        {            
            m_Boxes[i].GetComponent<Image>().color = color;
            //m_Boxes[i].GetComponent<Image>().sprite = m_volumeBar;
        }
    }

    // Empties all the boxes that have an index higher than the given index
    private void EmptyBoxes(int index)
    {
        for (int i = index + 1; i < m_Boxes.Count; i++)
        {
            m_Boxes[i].GetComponent<Image>().color = Color.white;
            //m_Boxes[i].GetComponent<Image>().sprite = null;
        }
    }

    public void Mute()
    {
        m_CurrentIndex = -1;
        EmptyBoxes(m_CurrentIndex);
        switch (e_Type)
        {
            case Name.SFX:
				AudioManager.Instance.s_playSFX = 0f;
                break;
            case Name.MUSIC:
				AudioManager.Instance.s_playMusic = 0f;
                break;
        }
    }
}
