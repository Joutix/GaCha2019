using UnityEngine;
using MyUtilities.DesignPatterns;

public class GameManager : Singleton<GameManager>
{
    #region Public Methods
    public void RegisterGrid(GameGrid _GameGrid)
    {

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
    #endregion

    #region Attributes
    private GameGrid m_GameGrid = null;
    #endregion
}
