using UnityEngine;

public class GameGrid : MonoBehaviour
{
    #region Public Methods
    public void IsEmptyAt(int _Row, int _Column)
    {
        throw new System.NotImplementedException();
    }

    public void OnCellEntered(GridCell _CellDestination)
    {
        if (m_CurrentCell)
        {
            m_CurrentCell.OnCellExited();
        }
        m_CurrentCell = _CellDestination;
        m_CurrentCell.OnCellEntered();
    }

    public void PrintValue(int _Value)
    {
        Debug.Log(_Value);
    }
    #endregion

    #region Private Methods
    private void Start()
    {
        GameManager.Instance.RegisterGrid(this);
        CreateGrid();
        OnCellEntered(m_GridCells[0, 0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_Forward))
        {
            TryMove(0, 0, 1);
        }
        else if (Input.GetKeyDown(m_Back))
        {
            TryMove(0, 0, -1);
        }
        else if (Input.GetKeyDown(m_Left))
        {
            TryMove(-1, 0, 0);
        }
        else if (Input.GetKeyDown(m_Right))
        {
            TryMove(1, 0, 0);
        }
    }

    private void CreateGrid()
    {
        m_GridCells = new GridCell[m_Width, m_Depth];
        for (int i = 0; i < m_Width; i++)
        {
            for (int k = 0; k < m_Depth; k++)
            {
                CreateCell(i, k);
            }
        }
    }

    private void CreateCell(int _PosX, int _PosZ)
    {
        GridCell cell = Instantiate<GameObject>(m_PrefabGridCell.gameObject, new Vector3(_PosX, _PosZ), Quaternion.identity).GetComponent<GridCell>();
        cell.name = "Cell [" + _PosX + ";" + _PosZ + "]";
        cell.PlaceCell(_PosX, _PosZ);
        m_GridCells[_PosX, _PosZ] = cell;
    }

    private void TryMove(int _OffsetRow, int _OffsetColumn, int _OffsetDepth)
    {
        int rowDest = m_CurrentCell.Row + _OffsetRow;
        int depthDest = m_CurrentCell.Depth + _OffsetDepth;
        if (IsValidDestination(rowDest, depthDest))
        {
            GridCell destination = m_GridCells[rowDest, depthDest];
            OnCellEntered(destination);
        }
    }

    private bool IsValidDestination(int _RowDest, int _DepthDest)
    {
        bool isValid = _RowDest >= 0 && _RowDest < m_Width;
        isValid = isValid && _DepthDest >= 0 && _DepthDest < m_Depth;
        return isValid;
    }
    #endregion

    #region Attributes
    [Header("Grid dimensions")]
    [SerializeField] private int m_Width = 1;
    [SerializeField] private int m_Depth = 1;

    private GridCell[,] m_GridCells = null;
    private GridCell m_CurrentCell = null;

    [Header("Prefab")]
    [SerializeField] private GridCell m_PrefabGridCell = null;

    [Header("Inputs")]
    [SerializeField] private KeyCode m_Forward = default;
    [SerializeField] private KeyCode m_Back = default;
    [SerializeField] private KeyCode m_Left = default;
    [SerializeField] private KeyCode m_Right = default;
    #endregion
}
