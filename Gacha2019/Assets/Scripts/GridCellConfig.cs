using UnityEngine;

[CreateAssetMenu(fileName = "New CellConfig", menuName = "Game/Grid/Cell Config")]
public class GridCellConfig : ScriptableObject
{
    #region Getters
    public Mesh CellMesh
    {
        get
        {
            return m_CellMesh;
        }
    }

    public Material CellMaterial
    {
        get
        {
            return m_CellMaterial;
        }
    }

    public bool CellCrossable
    {
        get
        {
            return m_CellCrossable;
        }
    }
    #endregion
    
    #region Attributes
    [SerializeField] private Mesh m_CellMesh = null;
    [SerializeField] private Material m_CellMaterial = null;
    [SerializeField] private bool m_CellCrossable = true;
    #endregion
}
