using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeHandler : MonoBehaviour
{
    public enum Name { SFX, MUSIC };

    [SerializeField]
    private Name e_Type;
    [SerializeField]
    private Sprite m_volumeBar;
    [SerializeField]
    private List<GameObject> m_Boxes;

    private int m_currentIndex;

    void Start()
    {
        // At the start of the game the volume is to the max
        m_currentIndex = 9;
        FillBoxes(m_currentIndex);
    }

    public void OnPointerEnter(GameObject box)
    {
        int index = m_Boxes.IndexOf(box);
        if (index < m_currentIndex)
            index = m_currentIndex;
        FillBoxes(index);
    }

    public void OnPointerExit(GameObject box)
    {
        EmptyBoxes(m_currentIndex);
    }

    public void OnPointerClick(GameObject box)
    {
        m_currentIndex = m_Boxes.IndexOf(box);
        FillBoxes(m_currentIndex);
        EmptyBoxes(m_currentIndex);
        switch (e_Type)
        {
            case Name.SFX:
                AudioManager.s_playSFX = (m_currentIndex + 1)/10f;
                Debug.Log("Changed SFX volume to " + AudioManager.s_playSFX);
                break;
            case Name.MUSIC:
                AudioManager.s_playMusic = (m_currentIndex + 1) / 10f;
                Debug.Log("Changed Music volume to " + AudioManager.s_playMusic);
                break;
        }
    }

    // Fills all the boxes that have an index lower or equal to the given index
    private void FillBoxes(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            //m_Boxes[i].GetComponent<Image>().color = Color.red;
            m_Boxes[i].GetComponent<Image>().sprite = m_volumeBar;
        }
    }

    // Empties all the boxes that have an index higher than the given index
    private void EmptyBoxes(int index)
    {
        for (int i = index + 1; i < m_Boxes.Count; i++)
        {
            //m_Boxes[i].GetComponent<Image>().color = Color.white;
            m_Boxes[i].GetComponent<Image>().sprite = null;
        }
    }

    public void Mute()
    {
        m_currentIndex = -1;
        EmptyBoxes(m_currentIndex);
        switch (e_Type)
        {
            case Name.SFX:
                AudioManager.s_playSFX = 0f;
                break;
            case Name.MUSIC:
                AudioManager.s_playMusic = 0f;
                break;
        }
    }
}
