using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		// Récupération de l'objet touché par la balle
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();
		if (health != null)
		{
			//Application dégât
			health.TakeDamage(10);
		}
		



		Destroy(gameObject);
	}
}
