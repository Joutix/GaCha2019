using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChaseState : State 
{
    #region Attributs

    private Enemy m_ControlledEnemy = null;

    private float m_MovementCooldown = 0;

    private float m_TimeTillNextMovement = 0;

	#endregion
	
	#region Constructor
	
    public ChaseState(Enemy _ControlledEnemy, float _MovementSpeed)
    {
        m_ControlledEnemy = _ControlledEnemy;
        m_MovementCooldown = _MovementSpeed;
        m_TimeTillNextMovement = m_MovementCooldown;
    }

    #endregion

    #region Accessor

    #endregion

    #region Public Methods
    public void SetTimeBetweeMovement(float _TimeBetweenMovement)
    {
        m_MovementCooldown = _TimeBetweenMovement;
    }
    #endregion

    #region Protected Methods

    #endregion

    #region Private Methods

    #endregion
}
