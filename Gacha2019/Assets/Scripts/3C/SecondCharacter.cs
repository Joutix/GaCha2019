using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCharacter : MonoBehaviour
{

    [SerializeField] public float m_DistanceToBody = 1.2f;
    [SerializeField] private GameObject m_OrbitalPrefab = null;

    private GameObject m_Orbital = null;


    private Vector3 m_OffsetVector = new Vector3(1, 0, 0);

    public void TurnAroundRoot(float m_Right, float m_Up)
    {
        m_OffsetVector = (m_Right * Vector3.right + m_Up * Vector3.forward).normalized;
        m_Orbital.transform.position = transform.position + m_OffsetVector * m_DistanceToBody;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Orbital = Instantiate<GameObject>(m_OrbitalPrefab.gameObject);
        m_Orbital.transform.position = transform.position + m_OffsetVector * m_DistanceToBody;
    }

    // Update is called once per frame
    void Update()
    {
        //update position of the orbital
        m_Orbital.transform.position = transform.position + m_OffsetVector * m_DistanceToBody;

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
