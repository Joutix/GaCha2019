using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCharacter : MonoBehaviour
{

    [SerializeField] public float m_DistanceToBody = 1.2f;

    [SerializeField] private GameObject m_Prefab = null;


    private Vector3 m_Offset = new Vector3(1, 0, 0);

    public void TurnAroundRoot(float m_Right, float m_Up)
    {
        m_Offset = (m_Right * Vector3.right + m_Up * Vector3.forward).normalized;
        m_Prefab.transform.position = transform.position + m_Offset *m_DistanceToBody;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //update position of the orbital
        m_Prefab.transform.position = transform.position + m_Offset * m_DistanceToBody;

        if (Input.GetKeyDown(KeyCode.O))
        {
            TurnAroundRoot(0, 1);
        }

        else if (Input.GetKeyDown(KeyCode.L))
        {
            TurnAroundRoot(0, -1);
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            TurnAroundRoot(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            TurnAroundRoot(1, 0);
        }
    }
}
