using UnityEngine;

public class Character : Entity
{
    #region Public Methods
    public /*override*/ void TryMove(int _DeltaRow, int _DeltaColumn)
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

	public void Teleport(Grid grid, int _DeltaRow, int _DeltaColumn)
	{

	}

    #endregion

    #region Private Methods
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterCharacter(this);
    }

    private bool IsValidDestination(int _RowDestination, int _ColumnDestination)
    {
        //get grid
        GameGrid grid = GameManager.Instance.GameGrid;

        //check if the destination isn't out of grid
        bool cellExists = grid.IsValidDestination(_RowDestination, _ColumnDestination);

        //if it doesn't return false + debug error
        if (!cellExists)
        {
            //Debug.LogError("Cell doesn't exist/ destination not valid");
            return false;
        }

        //check if the cell is empty, and if it's not, check if the enemy is small
        else
        {
            bool cellIsEmpty = grid.IsEmptyAt(_RowDestination, _ColumnDestination);
            bool cellIsCrossable = grid.GetGridCellAt(_RowDestination, _ColumnDestination).IsCharacterCrossable;
            Entity entity = grid.GetGridCellAt(_RowDestination, _ColumnDestination).Entity;//.IsSmall;
            bool enemyInCellIsSmallAndCanBeStomped = false;

            Enemy enemy = entity as Enemy;
            if (enemy != null)
            {
                enemyInCellIsSmallAndCanBeStomped = enemy.CanBeStomped();
                if (enemyInCellIsSmallAndCanBeStomped)
                    enemy.Stomp();
            }

            return ((cellIsEmpty && cellIsCrossable) || enemyInCellIsSmallAndCanBeStomped);
        }
    }

    private void MoveTo(int _RowDestination, int _ColumnDestination)
    {
        GameGrid grid = GameManager.Instance.GameGrid;

        transform.position = (grid.GetGridCellAt(_RowDestination, _ColumnDestination).transform.position + new Vector3(0, 2, 0));
        //maybe put next lines in a function called on entering a new cell
        //works for now  as this is a teleport and it's instantaneous
        //DELETE THIS LATER IF PLAYER DOESNT TP TO OTHER CELLS

        grid.GetGridCellAt(m_CurrentRow, m_CurrentColumn).OnCellExited(this);
        grid.GetGridCellAt(_RowDestination, _ColumnDestination).OnCellEntered(this);

        m_CurrentRow = _RowDestination;
        m_CurrentColumn = _ColumnDestination;
    }
    #endregion

    #region Attributes
    private int m_CurrentRow = 0;
    private int m_CurrentColumn = 0;
    #endregion


    #region Mono

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TryMove(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TryMove(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            TryMove(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            TryMove(0, 1);
        }
    }

    #endregion
}
