﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyUtilities.DesignPatterns;

public class GameManager : Singleton<GameManager>
{
    #region Public Methods
    public void RegisterGrid(GameGrid _GameGrid)
    {
        if (!m_DictionnaryOfEnemies.ContainsKey(_GameGrid))
        {
            m_DictionnaryOfEnemies.Add(_GameGrid, new List<Enemy>());
        }
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
            {
                m_DictionnaryOfEnemies[_grid].Add(_enemyToAdd);
            }
            else
            {
                Debug.LogWarning("enemy to add is already in the list");
            }
        }
    }

    //removes the given enemy from the manager, if it is inside its list
    public void RemoveEnemyFromManager(Enemy _enemyToRemove, GameGrid _grid)
    {
        if (_grid && _enemyToRemove)
        {
            if (m_DictionnaryOfEnemies[_grid].Contains(_enemyToRemove))
            {
                m_DictionnaryOfEnemies[_grid].Remove(_enemyToRemove);
            }
            else
            {
                Debug.LogWarning("enemy to remove isn't in the list yet");
            }
        }
    }

    //returns the path of the closest enemy present in a range calculated with manhattan distance
    public List<GridCell> ReturnClosestEnemyPath(Enemy _enemyToIgnore, int _maxManhattanDist/*, bool _careAboutWalls*/)
    {
        GridCell startCell = _enemyToIgnore.CurrentCell;
        GameGrid grid = startCell.GameGrid;
        List<GridCell> bestPath = new List<GridCell>();

        List<Enemy> enemiesInManDist = new List<Enemy>();

        foreach (Enemy enemy in m_DictionnaryOfEnemies[grid])
        {
            if (enemy != _enemyToIgnore)
            {
                int dist = ManhattanDistance(startCell.Row, startCell.Column, enemy.CurrentCell.Row, enemy.CurrentCell.Column);
                //if enemy in the grid is close enough add them at the key where they belong
                if ((enemy && dist <= _maxManhattanDist))
                {
                    enemiesInManDist.Add(enemy);
                }
            }
        }

        if (enemiesInManDist.Count == 0)
        {
            return bestPath; // empty
        }

        for (int i = 0; i < enemiesInManDist.Count; i++)
        {
            List<GridCell> currentDijkstra = Dijkstra.ComputeEnemyDijkstraPath(startCell.GameGrid, startCell, enemiesInManDist[i].CurrentCell);

            if (bestPath.Count == 0 && currentDijkstra != null)
            {
                bestPath = currentDijkstra;
            }

            if (bestPath != null && currentDijkstra != null && currentDijkstra.Count < bestPath.Count)
            {
                bestPath = currentDijkstra;
            }
        }

        return bestPath;
    }

    public int ManhattanDistance(int _x1, int _y1, int _x2, int _y2)
    {
        return Mathf.Abs(_x2 - _x1) + Mathf.Abs(_y2 - _y1);
    }

    #endregion

    #region Getters / Setters

    public Character Character
    {
        get
        {
            return m_Character;
        }
    }
    #endregion

    #region Attributes
    private Character m_Character = null;

    //private List<Enemy> m_ListOfEnemies = new List<Enemy>();
    private Dictionary<GameGrid, List<Enemy>> m_DictionnaryOfEnemies = new Dictionary<GameGrid, List<Enemy>>();

    #endregion
}
