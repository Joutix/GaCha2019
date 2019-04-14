using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Update()
    {
        // Change the key to match a controller input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_MenuUI.SetActive(true);
            m_OptionsUI.SetActive(false);
            m_CreditsUI.SetActive(false);
        }
    }

    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("LevelScene");
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
