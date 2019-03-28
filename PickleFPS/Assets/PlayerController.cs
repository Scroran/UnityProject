
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	public GameObject BrickPrefab;
	public Transform BrickSpawn;
	// Use this for initialization
	void Update ()
	{


		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}
	[Command]
	void CmdFire()
	{
		// Création du coup de feu Brick à partir de la prefab Brick

		var Brick = (GameObject)Instantiate(
			BrickPrefab,
			BrickSpawn.position,
			BrickSpawn.rotation);

		// Vélocité de la brick
		Brick.GetComponent<Rigidbody>().velocity = Brick.transform.forward * 6;

		// Apparition de la balle pour les clients
		NetworkServer.Spawn(Brick);

		// Destruction de la brick après 2 secondes
		Destroy(Brick, 2.0f);
	}
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
