using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    //adds the given enemy to the manager, if it isn't already in its list
    public void AddEnemyToManager(Enemy _enemyToAdd, GameGrid _grid)
    {
        if (_grid && _enemyToAdd)
        {

            if (!m_DictionnaryOfEnemies[_grid].Contains(_enemyToAdd))
                m_DictionnaryOfEnemies[_grid].Add(_enemyToAdd);
            //if (!m_ListOfEnemies.Contains(_enemyToAdd))
            //    m_ListOfEnemies.Add(_enemyToAdd);
            else { Debug.LogWarning("enemy to add is already in the list"); }
        }
    }

    //removes the given enemy from the manager, if it is inside its list
    public void RemoveEnemyFromManager(Enemy _enemyToRemove, GameGrid _grid)
    {
        if (_grid && _enemyToRemove)
        {
            if (m_DictionnaryOfEnemies[_grid].Contains(_enemyToRemove))
                // if (m_ListOfEnemies.Contains(_enemyToAdd))
                m_DictionnaryOfEnemies[_grid].Add(_enemyToRemove);
            else { Debug.LogWarning("enemy to remove isn't in the list yet"); }
        }
    }
    
    public GridCell ReturnClosestEnemy(Enemy _enemyToIgnore, int _maxManhattanDist, bool _careAboutWalls)
    {
        //GridCell cell = _enemyToIgnore.GetCell();
        //GameGrid grid = cell.GameGrid;



        //foreach (Enemy enemy in m_DictionnaryOfEnemies[grid])
        //{

        //}


        return null;
    }

    public int ManhattanDistance(int _x1, int _y1, int _x2, int _y2)
    {
        return Mathf.Abs(_x1 - _x2) + Mathf.Abs(_y1 - _y2);
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

    //private List<Enemy> m_ListOfEnemies = new List<Enemy>();
    private Dictionary<GameGrid, List<Enemy>> m_DictionnaryOfEnemies = new Dictionary<GameGrid, List<Enemy>>();

    #endregion
}
