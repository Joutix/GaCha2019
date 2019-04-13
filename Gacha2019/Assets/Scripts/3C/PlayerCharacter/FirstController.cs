using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : Player
{

    //this player controls the character's movement

    #region  Attributes

    #region  public



    #endregion

    #region protected  

    [SerializeField] KeyCode m_Up;
    [SerializeField] KeyCode m_Down;
    [SerializeField] KeyCode m_Left;
    [SerializeField] KeyCode m_Right;

    //last valid forward the player had => last direction he could move along onto the next tile
    Vector2 m_LastValidForward;


    // [SerializeField] KeyCode m_Shoot;

    #endregion
    #endregion


    #region function
    #region public functions




    #endregion
    #endregion



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
