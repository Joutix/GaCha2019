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

    public Page page;

    void Start()
    {
        m_PausePanel.SetActive(false);
        page = Page.None;
    }

    void Update()
    {
        // Change the key to match a controller input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (page.Equals(Page.None) || page.Equals(Page.Options))
                Pause();
            else if (page.Equals(Page.Pause))
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
        page = Page.Pause;
    }
    
    public void Resume()
    {
        Debug.Log("Resuming...");
        m_PauseUI.SetActive(false);
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        page = Page.None;
    }

    public void Options()
    {
        m_OptionsUI.SetActive(true);
        m_PauseUI.SetActive(false);
        page = Page.Options;
    }

    public void Back()
    {
        m_PauseUI.SetActive(true);
        m_OptionsUI.SetActive(false);
        page = Page.Pause;
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
