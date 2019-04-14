using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTeleportation : MonoBehaviour
{
	//[SerializeField]
	//private GameObject m_DirToCell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void teleportation(GameObject m_DirToCell)
	{
		Vector3 pos = new Vector3();
		pos.x = m_DirToCell.transform.position.x;
		pos.z = m_DirToCell.transform.position.z;
		pos.y = GameManager.Instance.Character.transform.localPosition.y;
		GameManager.Instance.Character.transform.localPosition = pos;
	}
}
