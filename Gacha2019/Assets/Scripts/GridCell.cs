using UnityEngine;
using UnityEngine.Events;

public class GridCell : MonoBehaviour
{
    #region Public Methods
    public void PlaceCell(int _Row, int _Depth)
    {
        m_RowPos = _Row;
        m_ColumnPos = _Depth;
    }
    public void OnCellEntered(Entity _EnteredEntity)
    {
        /*
         * more code if needed
         */
        
        m_OnCellEnterEvents.Invoke();
    }

    public void OnCellExited(Entity _ExitedEntity)
    {
        /*
         * more code if needed
         */

        m_OnCellExitEvents.Invoke();
    }
    #endregion

    #region Private Methods
    #endregion

    #region Getters / Setters
    public int Row
    {
        get
        {
            return m_RowPos;
        }
    }

    public int Column
    {
        get
        {
            return m_ColumnPos;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return m_CurrentEntity == null;
        }
    }

    public bool IsCrossable
    {
        get
        {
            return m_IsCrossable;
        }
    }

    public Entity Entity
    {
        get
        {
            return m_CurrentEntity;
        }
    }
    #endregion

    #region Attributes
    [Header("Cell State")]
    [SerializeField] private bool m_IsCrossable = true;

    [Header("Cell Config")]
    [SerializeField] private GridCellConfig m_CellConfig = null;

    [Header("Events")]
    [SerializeField] private UnityEvent m_OnCellEnterEvents = null;
    [SerializeField] private UnityEvent m_OnCellExitEvents = null;

    private Entity m_CurrentEntity = null;
    private int m_RowPos = 0;
    private int m_ColumnPos = 0;
    #endregion


}
