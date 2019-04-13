using UnityEngine;

public class GameGrid : MonoBehaviour
{
    #region Public Methods
    public bool IsEmptyAt(int _Row, int _Column)
    {
        int index = GetIndex(_Row, _Column);
        return m_GridCells[index].IsEmpty;
    }

    public bool IsValidDestination(int _RowDest, int _ColumnDest)
    {
        return GetIndex(_RowDest, _ColumnDest) != -1;
    }

    public GridCell GetGridCellAt(int _Row, int _Column)
    {
        int index = GetIndex(_Row, _Column);
        return m_GridCells[index];
    }

    //public void OnCellEntered(GridCell _CellDestination)
    //{
    //    if (m_CurrentCell)
    //    {
    //        m_CurrentCell.OnCellExited();
    //    }
    //    m_CurrentCell = _CellDestination;
    //    m_CurrentCell.OnCellEntered();
    //}
    #endregion

    #region Private Methods
    private void Awake()
    {
        GameManager.Instance.RegisterGrid(this);
    }

    private void Start()
    {
        //CreateGrid();
        //OnCellEntered(m_GridCells[0, 0]);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(m_Forward))
        //{
        //    TryMove(0, 0, 1);
        //}
        //else if (Input.GetKeyDown(m_Back))
        //{
        //    TryMove(0, 0, -1);
        //}
        //else if (Input.GetKeyDown(m_Left))
        //{
        //    TryMove(-1, 0, 0);
        //}
        //else if (Input.GetKeyDown(m_Right))
        //{
        //    TryMove(1, 0, 0);
        //}
    }

    private void CreateGrid()
    {
        //m_GridCells = new GridCell[m_Rows, m_Columns];
        m_GridCells = new GridCell[m_Rows * m_Columns];
        for (int i = 0; i < m_Rows; i++)
        {
            for (int j = 0; j < m_Columns; j++)
            {
                CreateCell(i, j);
            }
        }
    }

    private void CreateCell(int _Row, int _Column)
    {
        GridCell cell = Instantiate<GameObject>(m_PrefabGridCell.gameObject, new Vector3(_Row, 0, _Column), Quaternion.identity).GetComponent<GridCell>();
        cell.name = "Cell [" + _Row + ";" + _Column + "]";
        cell.PlaceCell(_Row, _Column);
        //m_GridCells[_Row, _Column] = cell;

        int index = GetIndex(_Row, _Column);
        m_GridCells[index] = cell;
    }

    private int GetIndex(int _Row, int _Column)
    {
        int index;
        if (_Row < 0 || _Row >= m_Rows || _Column < 0 || _Column >= m_Columns)
        {
            index = -1;
        }
        else
        {
            index = _Row * m_Rows + _Column;
        }
        return index;
    }

    //private void TryMove(int _OffsetRow, int _OffsetColumn, int _OffsetDepth)
    //{
    //    int rowDest = m_CurrentCell.Row + _OffsetRow;
    //    int depthDest = m_CurrentCell.Depth + _OffsetDepth;
    //    if (IsValidDestination(rowDest, depthDest))
    //    {
    //        GridCell destination = m_GridCells[rowDest, depthDest];
    //        OnCellEntered(destination);
    //    }
    //}
    #endregion

    #region Attributes
    [Header("Grid dimensions")]
    [SerializeField] private int m_Rows = 1;
    [SerializeField] private int m_Columns = 1;

    [SerializeField] private GridCell[] m_GridCells = null;
    //private GridCell m_CurrentCell = null;

    [Header("Prefab")]
    [SerializeField] private GridCell m_PrefabGridCell = null;

    //[Header("Inputs")]
    //[SerializeField] private KeyCode m_Forward = default;
    //[SerializeField] private KeyCode m_Back = default;
    //[SerializeField] private KeyCode m_Left = default;
    //[SerializeField] private KeyCode m_Right = default;
    #endregion
}
