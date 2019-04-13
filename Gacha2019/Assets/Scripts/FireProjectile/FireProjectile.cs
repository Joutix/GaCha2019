using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
	public Rigidbody Bullet;
	//public GameObject Bullet;
	public float BulletSpeed = 50;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			Shoot();
		}
	}

	void Shoot()
	{
		Rigidbody BulletClone = (Rigidbody)Instantiate(Bullet, new Vector3(this.gameObject.transform.position.x + 2, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);

		BulletClone.velocity = transform.right * BulletSpeed;
	}


}
