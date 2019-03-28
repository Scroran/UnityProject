using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public bool destroyOnDeath;
	public const int maxHealth = 100;

    private NetworkStartPosition[] spawnPoints;
	[SyncVar(hook = "OnChangeHealth")]
	public int currenthealth = maxHealth;
	public RectTransform healthBar;

    private void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }
    //Prendre des dégâts
    public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}
		currenthealth -= amount;

		// Mort du personnage
		if (currenthealth <= 0)
		{
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {

            
			currenthealth = maxHealth;
			RpcRespawn();
            }
        }
	}
	void OnChangeHealth(int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
            //Le Spawn d'origine
            Vector3 spawnPoint = Vector3.zero;
            
            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                
                spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)].transform.position;
                
            }
            transform.position = spawnPoint;

		}

	}
}
