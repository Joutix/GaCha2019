using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the color of the enemy used to ignore other colored bullets
public enum EEnemyColor
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

public class Enemy : Entity
{
    #region Members
    [SerializeField]
    private EEnemyColor m_EnemyColor = EEnemyColor.Red;

    [SerializeField]
    private EEnemySize m_EnemySize = EEnemySize.Little;

    [SerializeField]
    private EEnemyState m_EnemyState = EEnemyState.Wandering;

    private Vector2 m_GridPosition;

    private bool m_IsStunned = false;

    [SerializeField]
    private float m_StunTime = 5;

    private float m_CurrentStunTime = 0;

    #endregion

    #region Public Methods

    public bool CanBeStomped()
    {
        return m_IsStunned;
    }

    #endregion

    #region Public Methods

    public void Stomp()
    {
        Die();
    }

    #endregion

    #region Protected Methods

    protected void DecreaseSize()
    {
        switch (m_EnemySize)
        {
            case EEnemySize.Little:
                Stun();
                break;
            case EEnemySize.Medium:
                Debug.Log("TO DO spawn 2 little enemies");
                break;
            case EEnemySize.Large:
                Debug.Log("TO DO spawn 2 medium enemies");
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        // Decrease stun timer
        ManageStunTimer();


    }

    override public void TakeDamage(int _Amount)
    {
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
            }
        }
    }
    
    #endregion
}
