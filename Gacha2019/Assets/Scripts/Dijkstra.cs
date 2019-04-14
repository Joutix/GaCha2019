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

        Path firstPath = new Path(_Grid.GetGridCellAt(_RowStart, _ColumnStart));
        pathsToExpand.Add(firstPath);


        while (pathsToExpand.Count > 0)
        {
            Path toExpand = pathsToExpand[0];
            pathsAlreadyExpanded.Add(toExpand);
            pathsToExpand.RemoveAt(0);

            List<GridCell> neighbors = GetNeighborsOfLast(toExpand, _Grid);
            foreach (GridCell cell in neighbors)
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
        return null;
    }
    #endregion

    #region Private Methods
    private static List<GridCell> GetNeighborsOfLast(Path _ToExpand, GameGrid _GameGrid)
    {
        List<GridCell> validNeighbors = new List<GridCell>();
        GridCell last = _ToExpand.GetLast();

        if (IsValidCellToEnemy(_GameGrid, last.Row + 1, last.Column))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row + 1, last.Column);
            validNeighbors.Add(cell);
        }

        if (IsValidCellToEnemy(_GameGrid, last.Row - 1, last.Column))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row - 1, last.Column);
            validNeighbors.Add(cell);
        }

        if (IsValidCellToEnemy(_GameGrid, last.Row, last.Column + 1))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row, last.Column + 1);
            validNeighbors.Add(cell);
        }

        if (IsValidCellToEnemy(_GameGrid, last.Row, last.Column - 1))
        {
            GridCell cell = _GameGrid.GetGridCellAt(last.Row, last.Column - 1);
            validNeighbors.Add(cell);
        }
        return validNeighbors;
    }

    private static bool IsValidCellToEnemy(GameGrid _GameGrid, int _Row, int _Column)
    {
        bool isValidToEnemy = _GameGrid.IsValidDestination(_Row, _Column);
        isValidToEnemy = isValidToEnemy && _GameGrid.GetGridCellAt(_Row, _Column).IsEnemyCrossable;
        isValidToEnemy = isValidToEnemy && (_GameGrid.IsEmptyAt(_Row, _Column) || _GameGrid.GetGridCellAt(_Row, _Column).Entity is Character);
        return isValidToEnemy;
    }

    private static bool IsNotVisited(GridCell _Cell, List<Path> _PathsToTest)
    {
        return _PathsToTest.Find(path => path.GetLast() == _Cell) == null;
    }
    #endregion
}
