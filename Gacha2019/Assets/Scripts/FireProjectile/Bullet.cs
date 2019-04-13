using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] protected float m_SpeedBullet;


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public float getBulletSpeed()
	{
		return m_SpeedBullet;
	}

	public float BulletSpeed
	{
		get
		{
			return m_SpeedBullet;
		}
	}

	//Detecte la collision via le TAG de l'objet

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.SetActive(false);
			Destroy(this);

		}

		if (collision.gameObject.tag == "Wall")
		{
			this.gameObject.SetActive(false);
		}
	}
}
