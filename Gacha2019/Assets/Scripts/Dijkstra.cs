using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Dijkstra
{
    private class Path
    {
        public Path(GridCell _CellToAdd)
        {
            m_Path.Add(_CellToAdd);
        }

        public Path(Path _ToCopy, GridCell _ToAdd)
        {
            m_Path.AddRange(_ToCopy.m_Path);
            m_Path.Add(_ToAdd);
        }

        public bool Contains(GridCell _ToFind)
        {
            return m_Path.Contains(_ToFind);
        }

        public GridCell GetLast()
        {
            return m_Path.LastOrDefault();
        }

        public List<GridCell> CellsPath
        {
            get
            {
                return m_Path;
            }
        }

        private List<GridCell> m_Path = new List<GridCell>();
    }

    #region Public Methods
    public static List<GridCell> ComputeEnemyDijkstraPath(GameGrid _Grid, int _RowStart, int _ColumnStart, int _RowDest, int _ColumnDest)
    {
        List<Path> pathsToExpand = new List<Path>();
        List<Path> pathsAlreadyExpanded = new List<Path>();
        Enemy enemyRequest = _Grid.GetGridCellAt(_RowStart, _ColumnStart).Entity as Enemy;
        if (enemyRequest == null) { Debug.LogError("dsvfsd"); }
        Path firstPath = new Path(_Grid.GetGridCellAt(_RowStart, _ColumnStart));
        pathsToExpand.Add(firstPath);

        while (pathsToExpand.Count > 0)
        {
            Path toExpand = pathsToExpand[0];
            pathsAlreadyExpanded.Add(toExpand);
            pathsToExpand.RemoveAt(0);

            List<GridCell> neighbors = GetNeighborsOfLast(toExpand, _Grid, enemyRequest);
            foreach (GridCell cell in neighbors)
            {
                if (!toExpand.Contains(cell))
                {
                    Path toAdd = new Path(toExpand, cell);
                    if (cell.Row == _RowDest && cell.Column == _ColumnDest)
                    {
                        return toAdd.CellsPath;
                    }
                    else if (IsNotVisited(cell, pathsAlreadyExpanded))
                    {
                        pathsToExpand.Add(toAdd);
                    }
                }
            }
        }
        return null;
    }

    public static List<GridCell> ComputeEnemyDijkstraPath(GameGrid _Grid, GridCell _StartingCell, GridCell _EndCell)
    {
        List<Path> openList = new List<Path>();
        List<GridCell> closedList = new List<GridCell>();

        Enemy enemyRequest = _StartingCell.Entity as Enemy;

        Path startPath = new Path(_StartingCell);

        openList.Add(startPath);

        while (openList.Count > 0 && !(openList[0].GetLast() == _EndCell))
        {
            Path currentShortest = openList[0];

            openList.RemoveAt(0);

            GridCell pathEndNode = currentShortest.GetLast();

            if (!closedList.Contains(pathEndNode))
            {
                List<GridCell> neighbors = GetNeighborsOfLast(currentShortest, _Grid, enemyRequest);

                List<Path> extandedPath = new List<Path>();

                for (int i = 0; i < neighbors.Count; i++)
                {
                    if (!closedList.Contains(neighbors[i]))
                    {
                        extandedPath.Add(new Path(currentShortest, neighbors[i]));
                    }
                }

                for (int i = 0; i < extandedPath.Count; i++)
                {
                    openList.Add(extandedPath[i]);
                }

                closedList.Add(pathEndNode);
            }
        }

        if (openList.Count <= 0)
        {
            return null;
        }

        return openList[0].CellsPath;
    }



    #endregion

    #region Private Methods
    private static List<GridCell> GetNeighborsOfLast(Path _ToExpand, GameGrid _GameGrid, Enemy _EnemyRequest)
    {
        List<GridCell> validNeighbors = new List<GridCell>();
        GridCell last = _ToExpand.GetLast();

        if (IsValidCellToEnemy(_GameGrid, last.Row + 1, last.Column, _EnemyRequest))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row + 1, last.Column);
            validNeighbors.Add(cell);
        }

        if (IsValidCellToEnemy(_GameGrid, last.Row - 1, last.Column, _EnemyRequest))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row - 1, last.Column);
            validNeighbors.Add(cell);
        }

        if (IsValidCellToEnemy(_GameGrid, last.Row, last.Column + 1, _EnemyRequest))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row, last.Column + 1);
            validNeighbors.Add(cell);
        }

        if (IsValidCellToEnemy(_GameGrid, last.Row, last.Column - 1, _EnemyRequest))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row, last.Column - 1);
            validNeighbors.Add(cell);
        }
        return validNeighbors;
    }

    private static bool IsValidCellToEnemy(GameGrid _GameGrid, int _Row, int _Column, Enemy _EnemyRequest)
    {
        if (_GameGrid.IsValidDestination(_Row, _Column))
        {
            GridCell currentCell = _GameGrid.GetGridCellAt(_Row, _Column);

            if (currentCell.IsEnemyCrossable)
            {
                if (_GameGrid.IsEmptyAt(_Row, _Column)) // Is empty
                {
                    return true;
                }

                if (currentCell.Entity is Character)
                {
                    return true;
                }

                Enemy enemyOnCell = currentCell.Entity as Enemy;

                if (enemyOnCell != null)
                {
                    if (enemyOnCell.CanMerge(_EnemyRequest))
                    {
                        return true;
                    }
                }

            }
        }

        return false;
        //bool isValidToEnemy = _GameGrid.IsValidDestination(_Row, _Column);
        //isValidToEnemy = isValidToEnemy && _GameGrid.GetGridCellAt(_Row, _Column).IsEnemyCrossable;
        //
        //if (isValidToEnemy)
        //{            
        //    bool cellAllowed = _GameGrid.IsEmptyAt(_Row, _Column);
        //    cellAllowed = cellAllowed || (_GameGrid.GetGridCellAt(_Row, _Column).Entity != null && _GameGrid.GetGridCellAt(_Row, _Column).Entity is Character);
        //    cellAllowed = cellAllowed || (_GameGrid.GetGridCellAt(_Row, _Column).Entity != null 
        //                                && _GameGrid.GetGridCellAt(_Row, _Column).Entity is Enemy
        //                                && (_GameGrid.GetGridCellAt(_Row, _Column).Entity as Enemy).CanMerge(_EnemyRequest));
        //    isValidToEnemy = isValidToEnemy && cellAllowed;
        //}
        //return isValidToEnemy;
    }

    private static bool IsNotVisited(GridCell _Cell, List<Path> _PathsToTest)
    {
        return _PathsToTest.Find(path => path.GetLast() == _Cell) == null;
    }
    #endregion
}
