using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the color of the enemy used to ignore other colored bullets
public enum EEntityColor
{
    Red,
    Blue
}

// The size of the enemy
public enum EEnemySize
{
    Little,
    Medium,
    Large
}

// The state of the AI
public enum EEnemyState
{
    Wandering,
    Fleeing,
    Attacking,
    Fusioning
}

[System.Serializable]
public struct PhaseInfo
{
    public Mesh PhaseMesh;
    public GameObject BulletPrefab;
    public Material PhaseMaterialRed;
    public Material PhaseMaterialBlue;
    public int PhaseMaxHealth;
    //... for later stats ?
}

public class Enemy : Entity
{
    #region Members
    [SerializeField]
    private EEntityColor m_EnemyColor = EEntityColor.Red;

    [SerializeField]
    private EEnemySize m_EnemySize = EEnemySize.Little;

    [SerializeField]
    private EEnemyState m_EnemyState = EEnemyState.Wandering;

    private Vector2 m_GridPosition;

    private bool m_IsStunned = false;

    [SerializeField]
    private float m_StunTime = 5;

    private float m_CurrentStunTime = 0;

    [SerializeField]
    private PhaseInfo m_PhaseLittleInfo;

    [SerializeField]
    private PhaseInfo m_PhaseMediumInfo;

    [SerializeField]
    private PhaseInfo m_PhaseLargeInfo;

    private GameObject m_CurrentBulletPrefab = null;
    private Mesh m_CurrentMesh = null;
    private Material m_CurrentMaterial = null;

    private MeshFilter m_MeshFilter = null;
    private MeshRenderer m_MeshRender = null;

    [SerializeField]
    private Material m_TestMaterial = null;

    private int m_CurrentColumn;
    private int m_CurrentRow;

    #endregion

    #region Public Methods

    public bool CanBeStomped()
    {
        return m_IsStunned;
    }

    //public bool IsSmall()
    //{
    //    return m_EnemySize == EEnemySize.Little;
    //}

    #endregion

    #region Public Methods

    public void Stomp()
    {
        Die();
    }


    public /*override*/ void TryMove(int _DeltaRow, int _DeltaColumn)
    {
        int rowDest = m_CurrentRow + _DeltaRow;
        int columnDest = m_CurrentColumn + _DeltaColumn;
        if (IsValidDestination(rowDest, columnDest))
        {
            MoveTo(rowDest, columnDest);
        }
    }


    #endregion

    #region Protected Methods

    protected bool IsEnemySameSizeAndNotTooBig(Enemy _enemy)
    {
        switch (_enemy.m_EnemySize)
        {
            case EEnemySize.Little:
                if (m_EnemySize == EEnemySize.Little) //if enemy and this are both small
                    return true;
                else return false;

            case EEnemySize.Medium:
                if (m_EnemySize == EEnemySize.Medium)//if enemy and this are both medium
                    return true;
                else return false;

            case EEnemySize.Large: //doesn't matter, too big, return false
                return false;
            default:
                return false;
        }
    }

    protected void MoveTo(int _RowDestination, int _ColumnDestination)
    {
        GameGrid grid = GameManager.Instance.GameGrid;

        transform.position = (grid.GetGridCellAt(_RowDestination, _ColumnDestination).transform.position + new Vector3(0, 2, 0));
        //maybe put next lines in a function called on entering a new cell
        //works for now  as this is a teleport and it's instantaneous
        //DELETE THIS LATER IF PLAYER DOESNT TP TO OTHER CELLS


        grid.GetGridCellAt(_RowDestination, _ColumnDestination).OnCellEntered(this);
        // grid.GetGridCellAt(_RowDestination, _ColumnDestination).OnCellEntered(this);
        m_CurrentRow = _RowDestination;
        m_CurrentColumn = _ColumnDestination;
    }

    protected bool IsValidDestination(int _RowDestination, int _ColumnDestination)
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
        else
        {
            bool cellIsEmpty = grid.IsEmptyAt(_RowDestination, _ColumnDestination);
            bool cellIsCrossable = grid.GetGridCellAt(_RowDestination, _ColumnDestination).IsCharacterCrossable;
            Entity entity = grid.GetGridCellAt(_RowDestination, _ColumnDestination).Entity;
            bool canMergeWithCellEnemy = false;

            Enemy enemy = entity as Enemy;
            if (enemy)
            {
                canMergeWithCellEnemy = IsEnemySameSizeAndNotTooBig(enemy);
            }
            Merge(enemy);

            return ((cellIsEmpty && cellIsCrossable) || (cellIsCrossable && canMergeWithCellEnemy));
        }
    }
    protected void DecreaseSize()
    {
        switch (m_EnemySize)
        {
            case EEnemySize.Little:
                Stun();
                break;
            case EEnemySize.Medium:
                Debug.Log("TO DO spawn another little enemy on grid");
                m_EnemySize = EEnemySize.Little;
                SetVariablesForCurrentState();
                m_CurrentLifePoint = m_MaxLifePoint;
                break;
            case EEnemySize.Large:
                Debug.Log("TO DO spawn another medium enemy on grid");
                m_EnemySize = EEnemySize.Medium;
                SetVariablesForCurrentState();
                m_CurrentLifePoint = m_MaxLifePoint;
                break;
            default:
                break;
        }
    }

    //pretty much the opposite of the decrease size function
    protected void Merge(Enemy _enemy)
    {
        if(_enemy)
        {
            switch (m_EnemySize)
            {
                case EEnemySize.Little:
                    m_EnemySize = EEnemySize.Medium;
                    SetVariablesForCurrentState();
                    Destroy(_enemy.gameObject);
                    break;
                case EEnemySize.Medium:
                    m_EnemySize = EEnemySize.Large;
                    SetVariablesForCurrentState();
                    Destroy(_enemy.gameObject);
                    break;
                case EEnemySize.Large:
                    break;
                default:
                    break;
            }
        }
        
    }


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        m_MeshFilter = GetComponent<MeshFilter>();

        if (m_MeshFilter == null)
        {
            Debug.LogError("Couldn't get mesh render on enemy make sure it has one");
        }

        m_MeshRender = GetComponent<MeshRenderer>();

        if (m_MeshRender == null)
        {
            Debug.LogError("Couldn't get MeshRenderer on enemy make sure it has one");
        }

        SetVariablesForCurrentState();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        ManageStunTimer();

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TryMove(1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    override public void TakeDamage(int _Amount)
    {
        Debug.Log("Enemy " + name + " took " + _Amount + " of damage");
        m_CurrentLifePoint -= _Amount;

        if (m_CurrentLifePoint <= 0)
        {
            DecreaseSize();
        }
    }

    protected void Stun()
    {
        if (!m_IsStunned)
        {
            m_IsStunned = true;

            m_CurrentStunTime = m_StunTime;
        }
    }

    protected void ManageStunTimer()
    {
        if (m_IsStunned)
        {
            m_CurrentStunTime -= Time.deltaTime;

            if (m_CurrentStunTime <= 0)
            {
                m_IsStunned = false;

                m_CurrentLifePoint = m_MaxLifePoint;
            }
        }
    }

    protected void SetVariablesForCurrentState()
    {
        switch (m_EnemySize)
        {
            case EEnemySize.Little:
                m_CurrentBulletPrefab = m_PhaseLittleInfo.BulletPrefab;
                m_MeshFilter.mesh = m_PhaseLittleInfo.PhaseMesh;
                SetMaterialAccordingToColor(m_PhaseLittleInfo);
                m_MaxLifePoint = m_PhaseLittleInfo.PhaseMaxHealth;
                break;
            case EEnemySize.Medium:
                m_CurrentBulletPrefab = m_PhaseMediumInfo.BulletPrefab;
                m_MeshFilter.mesh = m_PhaseMediumInfo.PhaseMesh;
                SetMaterialAccordingToColor(m_PhaseMediumInfo);
                m_MaxLifePoint = m_PhaseMediumInfo.PhaseMaxHealth;
                break;
            case EEnemySize.Large:
                m_CurrentBulletPrefab = m_PhaseLargeInfo.BulletPrefab;
                m_MeshFilter.mesh = m_PhaseLargeInfo.PhaseMesh;
                SetMaterialAccordingToColor(m_PhaseLargeInfo);
                m_MaxLifePoint = m_PhaseLargeInfo.PhaseMaxHealth;
                break;
            default:
                break;
        }
    }

    protected void SetMaterialAccordingToColor(PhaseInfo _PhaseInfo)
    {
        if (m_EnemyColor == EEntityColor.Red)
        {
            m_MeshRender.material = _PhaseInfo.PhaseMaterialRed;
        }
        else if (m_EnemyColor == EEntityColor.Blue)
        {
            m_MeshRender.material = _PhaseInfo.PhaseMaterialBlue;
        }
    }

    #endregion

    #region MonoBehavior

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet != null && bullet.Color == m_EnemyColor)
        {
            TakeDamage(bullet.Damage);

            Destroy(bullet.gameObject);
        }
    }

    #endregion

}
