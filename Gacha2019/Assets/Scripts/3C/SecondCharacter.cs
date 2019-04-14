using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCharacter : MonoBehaviour
{
    [SerializeField] public float m_DistanceToBody = 1.2f;
    [SerializeField] private GameObject m_PrefabOrbital = null;
    [SerializeField] private Bullet m_PrefabBullet = null;
    [SerializeField] private float m_BulletSpawnOffset = 2;
	[SerializeField] private float timeBtwAttack;
	[SerializeField] private float reloadTime = 1f;

	private GameObject m_Orbital = null;


    private Vector3 m_OffsetVector = new Vector3(1, 0, 0);

    public void TurnAroundRoot(float _Right, float _Up)
    {
        m_OffsetVector = (_Right * Vector3.right + _Up * Vector3.forward).normalized;
        m_Orbital.transform.position = transform.position + m_OffsetVector * m_DistanceToBody;
    }

    public void ShootCall()
    {
        Shoot(m_OffsetVector);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Orbital = Instantiate<GameObject>(m_PrefabOrbital.gameObject);
        m_Orbital.transform.position = transform.position + m_OffsetVector * m_DistanceToBody;
    }

    private void ManageInputs()
    {
        if (Input.GetKey(KeyCode.O))
        {
            if (Input.GetKey(KeyCode.K))
            {
                TurnAroundRoot(-1, 1);
            }
            else if (Input.GetKey(KeyCode.M))
            {
                TurnAroundRoot(1, 1);
            }
            else
                TurnAroundRoot(0, 1);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            if (Input.GetKey(KeyCode.K))
            {
                TurnAroundRoot(-1, -1);
            }
            else if (Input.GetKey(KeyCode.M))
            {
                TurnAroundRoot(1, -1);
            }
            else
                TurnAroundRoot(0, -1);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            TurnAroundRoot(-1, 0);
        }
        else if (Input.GetKey(KeyCode.M))
        {
            TurnAroundRoot(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot(m_OffsetVector);
        }

		if(timeBtwAttack <= 0 && (MicrophoneLevel.getInstance().getMicLoudness() > MicrophoneLevel.getInstance().m_thresholdWeak))
		{
			Shoot(m_OffsetVector);
			timeBtwAttack = reloadTime;
		}
		else
		{
			timeBtwAttack -= Time.deltaTime;
		}
	}

    // Update is called once per frame
    void Update()
    {
        //update position of the orbital
        m_Orbital.transform.position = transform.position + m_OffsetVector * m_DistanceToBody;

        ManageInputs();
    }
   

    private void Shoot(Vector3 _ShootDirection)
    {
        Vector3 pos = transform.position;

        pos += _ShootDirection * m_BulletSpawnOffset;
        GameObject bulletClone = Instantiate(m_PrefabBullet.gameObject, pos, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody>().velocity = _ShootDirection * bulletClone.GetComponent<Bullet>().Speed;
    }
}
