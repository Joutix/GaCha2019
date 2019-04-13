using UnityEngine;

public class Character : MonoBehaviour
{
    #region Public Methods
    public void TryMove(int _DeltaRow, int _DeltaColumn)
    {
        int rowDest = m_CurrentRow + _DeltaRow;
        int columnDest = m_CurrentColumn + _DeltaColumn;
        if (IsValidDestination(rowDest, columnDest))
        {
            MoveTo(rowDest, columnDest);
        }
    }

    public void Shoot()
    {

    }
    #endregion

    #region Private Methods
    private bool IsValidDestination(int _RowDestination, int _ColumnDest)
    {
        //get grid
        GameGrid grid = GameManager.Instance.GameGrid;

        //check if the destination isn't out of grid
        bool cellExists = grid.IsValidDestination(_RowDestination, _ColumnDest);

        //if it doesn't return false + debug error
        if (!cellExists)
        {
            Debug.LogError("Cell doesn't exist/ destination not valid");
            return false;
        }

        //check if the cell is empty, and if it's not, check if the enemy is small
        else
        {
            bool cellIsEmpty = grid.IsEmptyAt(_RowDestination, _ColumnDest);
            Entity entity = grid.GetGridCellAt(_RowDestination, _ColumnDest).Entity;//.IsSmall;
            bool enemyInCellIsSmall = false;

            Enemy enemy = entity as Enemy;
            if (enemy != null)
            {
                ////////////////// TODO Add/decomment when isSmall exist
                //////////////////   enemyInCellIsSmall = enemy.IsSmall;
            }

            return (cellIsEmpty || enemyInCellIsSmall);
        }
    }

    private void MoveTo(int _RowDestination, int _ColumnDestination)
    {
        GameGrid grid = GameManager.Instance.GameGrid;

        transform.position = (grid.GetGridCellAt(_RowDestination, _ColumnDestination).transform.position + new Vector3(0, 2, 0));
        //maybe put next lines in a function called on entering a new cell
        //works for now  as this is a teleport and it's instantaneous
        //DELETE THIS LATER IF PLAYER DOESNT TP TO OTHER CELLS
        m_CurrentRow = _RowDestination;
        m_CurrentColumn = _ColumnDestination;

    }
    #endregion

    #region Attributes
    private int m_CurrentRow = 0;
    private int m_CurrentColumn = 0;
    #endregion
}
