using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField]
    private GameObject m_MenuUI;
    [SerializeField]
    private GameObject m_OptionsUI;
    [SerializeField]
    private GameObject m_CreditsUI;
    
    void Start()
    {
        m_MenuUI.SetActive(true);
        m_OptionsUI.SetActive(false);
        m_CreditsUI.SetActive(false);
    }

    public void HandleMenuButtonClick(string btnName)
    {
        switch (btnName)
        {
            case "play":
                Debug.Log("Playing...");
                break;
            case "options":
                m_OptionsUI.SetActive(true);
                m_MenuUI.SetActive(false);
                break;
            case "credits":
                m_CreditsUI.SetActive(true);
                m_MenuUI.SetActive(false);
                break;
            case "back":
                m_MenuUI.SetActive(true);
                m_CreditsUI.SetActive(false);
                m_OptionsUI.SetActive(false);
                break;
            case "quit":
                Debug.Log("Quitting");
                Application.Quit();
                break;
        }
    }
}
