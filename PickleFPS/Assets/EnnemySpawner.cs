using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnnemySpawner : NetworkBehaviour
{

	public GameObject ennemyPrefab;

	public int numberOfEnnemies;

	public override void OnStartServer()
	{
		//Apparition du nombre d'ennemi lors du lancement du serveur
		for (int i = 0; i < numberOfEnnemies; i++)
		{
			var spawnPosition = new Vector3(
				Random.Range(-8.0f, 8.0f),
				0.0f,
				Random.Range(-8.0f, 8.0f));

			var spawnRotation = Quaternion.Euler(
				0.0f,
				Random.Range(0, 100),
				0.0f);

			var ennemy = (GameObject)Instantiate(ennemyPrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn(ennemy);
		}
	}
		
	
}
