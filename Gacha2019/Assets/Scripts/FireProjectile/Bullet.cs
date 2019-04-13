using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//Detecte la collision via le TAG de l'objet

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.SetActive(false);
			this.gameObject.SetActive(false);
		}

		if (collision.gameObject.tag == "Wall")
		{
			this.gameObject.SetActive(false);
		}
	}
}
