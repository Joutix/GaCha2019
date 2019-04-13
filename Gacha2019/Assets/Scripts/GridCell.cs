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
        m_MeshRenderer.material = m_OccupiedMaterial;
        m_OnCellEnterEvents.Invoke();
    }

    public void OnCellExited()
    {
        /*
         * more code if needed
         */

        m_MeshRenderer.material = m_EmptyMaterial;
        m_OnCellExitEvents.Invoke();
    }
    #endregion

    #region Private Methods
    #endregion

    #region Getters / Setters
    public GameGrid GameGrid
    {
        set
        {
            m_GameGrid = value;
        }
    }

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
    #endregion

    #region Attributes
    [SerializeField] private int m_Row = 0;
    [SerializeField] private int m_Depth = 0;

    [Header("Events")]
    [SerializeField] private UnityEvent m_OnCellEnterEvents = null;
    [SerializeField] private UnityEvent m_OnCellExitEvents = null;


    [Header("Materials")]
    [SerializeField] private MeshRenderer m_MeshRenderer = null;
    [SerializeField] private Material m_EmptyMaterial = null;
    [SerializeField] private Material m_OccupiedMaterial = null;

    [Header("Debug")]
    [SerializeField] private GameGrid m_GameGrid = null;
    #endregion


}
