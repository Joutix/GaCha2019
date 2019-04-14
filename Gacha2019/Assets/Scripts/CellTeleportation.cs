using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTeleportation : MonoBehaviour
{
	[SerializeField] private GridCell m_CellDestination = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Teleportation()
	{
		GameManager.Instance.Character.Teleport(m_CellDestination.GameGrid, m_CellDestination.Row, m_CellDestination.Column);
	}
}
