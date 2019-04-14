using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootState : State 
{
    #region Attributs

    private Enemy m_ControlledEnemy;

    private float m_ShootCoolDown = 0;

    private float m_TimeTillNextShoot = 0;

	#endregion
	
	#region Constructor
	
    public ShootState(Enemy _ControlledEnemy, float _ShootCoolDown)
    {
        m_ControlledEnemy = _ControlledEnemy;
        m_ShootCoolDown = _ShootCoolDown;
        m_TimeTillNextShoot = m_ShootCoolDown;
    }

    protected override void OnUpdate()
    {
        m_TimeTillNextShoot -= Time.deltaTime;

        if (m_TimeTillNextShoot <= 0)
        {
            //m_ControlledEnemy.ShootPlayer();

            m_TimeTillNextShoot = m_ShootCoolDown;
        }
    }
    #endregion

    #region Accessor

    #endregion

    #region Public Methods

    #endregion

    #region Protected Methods

    #endregion

    #region Private Methods

    #endregion
}
