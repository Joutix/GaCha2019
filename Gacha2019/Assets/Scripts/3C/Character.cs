using UnityEngine;
using System.Collections.Generic;

public class Character : Entity
{
    #region Public Methods
    public /*override*/ void TryMove(int _DeltaRow, int _DeltaColumn)
    {
        //int rowDest = m_CurrentRow + _DeltaRow;
        //int columnDest = m_CurrentColumn + _DeltaColumn;

        int rowDest = m_CurrentCell.Row + _DeltaRow;
        int columnDest = m_CurrentCell.Column + _DeltaColumn;

        if (m_CanMove && IsValidDestination(rowDest, columnDest))
        {
            MoveTo(m_CurrentCell.GameGrid, rowDest, columnDest);
        }
    }
    public override void TakeDamage(int _Amount)
    {
        base.TakeDamage(_Amount);

        if (m_CurrentLifePoint == 2)
        {
            AkSoundEngine.SetState("Player_Lives", "MidLife");
        }

        if (m_CurrentLifePoint == 1)
        {
            AkSoundEngine.SetState("Player_Lives", "LowLife");
        }


    }

    public void Teleport(GameGrid grid, int _DeltaRow, int _DeltaColumn)
    {
        MoveTo(grid, _DeltaRow, _DeltaColumn);
    }

    public override void Die()
    {
        base.Die();
        AkSoundEngine.PostEvent("Stop_Music", gameObject);
        panelGameOver.SetActive(true);
    }

    #endregion
    #region Private Methods

    void UpdateTimer()
    {
        //m_CanMove = (Time.time - m_MovementTimer) > m_TimeNeededToMoveAgain;
        m_MovementTimer += Time.deltaTime;
        m_CanMove = m_MovementTimer >= m_TimeNeededToMoveAgain;
    }

    private bool IsValidDestination(int _RowDestination, int _ColumnDestination)
    {
        //get grid
        //GameGrid currentGrid = GameManager.Instance.GameGrid;
        GameGrid currentGrid = m_CurrentCell.GameGrid;

        //check if the destination isn't out of grid
        bool cellExists = currentGrid.IsValidDestination(_RowDestination, _ColumnDestination);

        //if it doesn't return false + debug error
        if (!cellExists)
        {
            //Debug.LogError("Cell doesn't exist/ destination not valid");
            return false;
        }

        //check if the cell is empty, and if it's not, check if the enemy is small
        else
        {
            bool cellIsEmpty = currentGrid.IsEmptyAt(_RowDestination, _ColumnDestination);
            bool cellIsCrossable = currentGrid.GetGridCellAt(_RowDestination, _ColumnDestination).IsCharacterCrossable;
            Entity entity = currentGrid.GetGridCellAt(_RowDestination, _ColumnDestination).Entity;//.IsSmall;
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

    private void MoveTo(GameGrid _GridDestination, int _RowDestination, int _ColumnDestination)
    {
        //GameGrid currentGrid = GameManager.Instance.GameGrid;
        //GameGrid currentGrid = m_CurrentCell.GameGrid;

        GridCell previousGridCell = m_CurrentCell;
        m_CurrentCell = _GridDestination.GetGridCellAt(_RowDestination, _ColumnDestination);
        transform.position = (m_CurrentCell.transform.position + new Vector3(0, 0, 0));

        //currentGrid.GetGridCellAt(_RowDestination, _ColumnDestination).OnCellEntered(this);
        m_CurrentCell.OnCellEntered(this);

        //m_CurrentRow = _RowDestination;
        //m_CurrentColumn = _ColumnDestination;

        //m_CanMove = false;
        //m_MovementTimer = Time.time;
        m_MovementTimer = 0.0f;

        //currentGrid.GetGridCellAt(m_CurrentRow, m_CurrentColumn).OnCellExited(this);
        if (previousGridCell)
        {
            previousGridCell.OnCellExited(this);
        }

        AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
    }

	private void ResetWallOcclusion()
	{
		foreach (GridCell cell in m_OccludingWalls)
		{
			Color color = cell.GetComponent<MeshRenderer>().material.color;
			color.a = 1f;
			cell.GetComponent<MeshRenderer>().material.color = color;
		}
		m_OccludingWalls.Clear();
	}

	private void CheckForWallOcclusion()
	{
		//Debug.Log("Current cell: " + m_CurrentCell + " Game grid: " + m_CurrentCell.GameGrid);
		if ((m_CurrentCell.GameGrid.IsValidDestination(m_CurrentCell.Row - 1, m_CurrentCell.Column)
			&& m_CurrentCell.GameGrid.GetGridCellAt(m_CurrentCell.Row - 1, m_CurrentCell.Column).CompareTag("Wall"))
			|| (m_CurrentCell.GameGrid.IsValidDestination(m_CurrentCell.Row - 2, m_CurrentCell.Column)
				&& m_CurrentCell.GameGrid.GetGridCellAt(m_CurrentCell.Row - 2, m_CurrentCell.Column).CompareTag("Wall")))
		{
			for (int r = -3; r < 0; r++)
			{
				for (int c = -1; c < 2; c++)
				{
					if (m_CurrentCell.GameGrid.IsValidDestination(m_CurrentCell.Row + r, m_CurrentCell.Column + c)
						&& m_CurrentCell.GameGrid.GetGridCellAt(m_CurrentCell.Row + r, m_CurrentCell.Column + c).CompareTag("Wall"))
					{
						GridCell cell = m_CurrentCell.GameGrid.GetGridCellAt(m_CurrentCell.Row + r, m_CurrentCell.Column + c);
						Color color = cell.GetComponent<MeshRenderer>().material.color;
						color.a = 0.5f;
						cell.GetComponent<MeshRenderer>().material.color = color;
						m_OccludingWalls.Add(cell);
					}
				}
			}
		}
	}
	#endregion

	#region Attributes
	[SerializeField] private float m_TimeNeededToMoveAgain = 1.0f;
    private float m_MovementTimer = 0f;

    [SerializeField] private bool m_DebugInputsKeyboard = false;
    private bool m_CanMove = false;

    private GridCell m_CurrentCell = null;


    [SerializeField] private VFX_Manager m_VfxManager = null;

    [SerializeField] private GameObject panelGameOver;

	private List<GridCell> m_OccludingWalls;


	//private int m_CurrentRow = 0;
	//private int m_CurrentColumn = 0;
	#endregion

	#region accessors
	public int Row
    {
        get
        {
            //return m_CurrentRow;
            return m_CurrentCell.Row;
        }
    }

    public int Column
    {
        get
        {
            //return m_CurrentColumn;
            return m_CurrentCell.Column;
        }
    }

    public GridCell CurrentCell
    {
        get
        {
            return m_CurrentCell;
        }
    }
    #endregion

    #region Mono
    private void Awake()
    {
        GameManager.Instance.RegisterCharacter(this);
    }

    protected override void Start()
    {
        base.Start();
		m_OccludingWalls = new List<GridCell>();
		AkSoundEngine.SetState("Player_Lives", "FullLife");
        m_MovementTimer = m_TimeNeededToMoveAgain;
        m_VfxManager = GameObject.Find("VFX_Manager").GetComponent<VFX_Manager>();
    }

    protected override void Update()
    {
        UpdateTimer();


		ResetWallOcclusion();
		CheckForWallOcclusion();


		if (m_DebugInputsKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //m_VfxManager.PlayWalk(true);
                m_VfxManager.StartCoroutine("PlayWalk");
                TryMove(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //m_VfxManager.PlayWalk(true);
                m_VfxManager.StartCoroutine("PlayWalk");
                TryMove(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                //m_VfxManager.PlayWalk(true);
                m_VfxManager.StartCoroutine("PlayWalk");
                TryMove(0, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                //m_VfxManager.PlayWalk(true);
                m_VfxManager.StartCoroutine("PlayWalk");
                TryMove(0, 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TakeDamage(1);
        }
    }

    #endregion
}