using UnityEngine;
using UnityEngine.Events;

public class GridCell : MonoBehaviour
{
    #region Public Methods
    public void PlaceCell(int _Row, int _Depth)
    {
        m_Row = _Row;
        m_Depth = _Depth;
    }
    public void OnCellEntered()
    {
        /*
         * more code if needed
         */
        m_OnCellEnterEvents.Invoke();
    }

    public void OnCellExited()
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
            return m_Row;
        }
    }

    public int Depth
    {
        get
        {
            return m_Depth;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return m_Entity == null;
        }
    }

    public Entity Entity
    {
        get
        {
            return m_Entity;
        }
    }
    #endregion

    #region Attributes
    [SerializeField] private int m_Row = 0;
    [SerializeField] private int m_Depth = 0;

    [Header("Events")]
    [SerializeField] private UnityEvent m_OnCellEnterEvents = null;
    [SerializeField] private UnityEvent m_OnCellExitEvents = null;

    [SerializeField] private Entity m_Entity = null;
    #endregion


}
