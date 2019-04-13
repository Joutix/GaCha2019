using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyColor
{
    ERed,
    EBlue
}

public class Enemy : Entity
{
    #region Members

    private EEnemyColor m_EnemyColor = EEnemyColor.ERed;

    #endregion

    // Start is called before the first frame update
    override protected void Start()
    {
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        
    }

}
