using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIManager : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField]
    private GameObject m_PausePanel;
    [SerializeField]
    private GameObject m_PauseUI;
    [SerializeField]
    private GameObject m_OptionsUI;

    public enum Page { None, Pause, Options };

    public Page e_Page;

    void Start()
    {
        m_PausePanel.SetActive(false);
        e_Page = Page.None;
    }

    void Update()
    {
        // Change the key to match a controller input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (e_Page.Equals(Page.None) || e_Page.Equals(Page.Options))
                Pause();
            else if (e_Page.Equals(Page.Pause))
                Resume();
        }
    }

    private void Pause()
    {
        // We freeze everything
        Time.timeScale = 0f;
        m_PausePanel.SetActive(true);
        m_PauseUI.SetActive(true);
        m_OptionsUI.SetActive(false);
        e_Page = Page.Pause;
    }
    
    public void Resume()
    {
        Debug.Log("Resuming...");
        m_PauseUI.SetActive(false);
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        e_Page = Page.None;
    }

    public void Options()
    {
        m_OptionsUI.SetActive(true);
        m_PauseUI.SetActive(false);
        e_Page = Page.Options;
    }

    public void Back()
    {
        m_PauseUI.SetActive(true);
        m_OptionsUI.SetActive(false);
        e_Page = Page.Pause;
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
