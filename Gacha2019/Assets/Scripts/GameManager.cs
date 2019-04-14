using UnityEngine;
using MyUtilities.DesignPatterns;

public class GameManager : Singleton<GameManager>
{
    #region Public Methods
    public void RegisterGrid(GameGrid _GameGrid)
    {
        m_GameGrid = _GameGrid;
    }

    public void RegisterCharacter(Character _Character)
    {
        m_Character = _Character;
    }
    #endregion

    #region Getters / Setters
    public GameGrid GameGrid
    {
        get
        {
            return m_GameGrid;
        }
    }

    public Character Character
    {
        get
        {
            return m_Character;
        }
    }
    #endregion

    #region Attributes
    private GameGrid m_GameGrid = null;
    private Character m_Character = null;
    #endregion
}
