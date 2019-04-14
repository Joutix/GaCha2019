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

	public void teleportation(Grid gridCell,GameObject dirToCell)
	{
		GameManager.Instance.Character.Teleport(gridCell, (int)dirToCell.transform.position.z, (int)dirToCell.transform.position.x);
	}
}
