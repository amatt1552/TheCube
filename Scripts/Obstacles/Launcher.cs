using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour 
{
	bool finished;
	bool oneShotFinish;
	bool oneShotTimer;
	public float pause;
	float pauseTimer;
	float revertTimer;
	float direction;
	float playerRotation;
	public float distance;
	public float x,y,z;
	GameObject player;
	Transform launchMesh;
	Transform launchPos;
	public float tossForce;
	public Collider col;

	void Awake () 
	{
		launchMesh = transform.FindChild("launchPivot");
		launchPos = transform.FindChild("launchPos");
		player = GameObject.Find("player");
		finished = true;
		launchPos.GetComponent<SpriteRenderer>().color = Color.clear;

	}

	void FixedUpdate () 
	{
		playerRotation = PlayerMovement.rbPlayer.velocity.z/2;

		float xDist, yDist, zDist;
		xDist = Mathf.Abs(player.transform.position.x - transform.position.x);
		yDist = Mathf.Abs(player.transform.position.y - transform.position.y);
		zDist = Mathf.Abs(player.transform.position.z - transform.position.z);

		if(Mathf.Abs(transform.position.z - player.transform.position.z) >= distance)
		{
			finished = true;
		}

		if(xDist < 1 && yDist < 0.7f && zDist < 1f)
		{
			pauseTimer += 1 * Time.deltaTime;
			if(pauseTimer >= pause)
			{	
				finished = false;
				oneShotFinish = false;
				revertTimer = 0;
				Launch();
			}
		
		}

		else
		{
			pauseTimer = 0;
		}

		if((finished || PlayerSettings.dead) && !oneShotFinish)
		{
			PlayerMovement.rbPlayer.useGravity = false;
			PlayerMovement.rbPlayer.isKinematic = true;
			//PlayerMovement.rbPlayer.constraints = RigidbodyConstraints.None;
			PlayerMovement.active = true;
			PlayerSettings.playerMesh.transform.localEulerAngles = new Vector3(-90,0,0);

			oneShotFinish = true;

		}

		if(!finished && !PlayerSettings.dead)
		{
			PlayerSettings.playerMesh.transform.Rotate((Vector3.right) * playerRotation);
		}

		if(PlayerMovement.touchingCollider)
		{
			finished = true;
		}

		revertTimer += 1 * Time.deltaTime;

		if(revertTimer <= 1)
		{
			col.enabled = false;

			launchMesh.transform.localEulerAngles = new Vector3(-30,0,0);

		}
		else
		{
			
			oneShotTimer = true;
			col.enabled = true;
			launchMesh.transform.localEulerAngles = new Vector3(0,0,0);

		}
	}

	void Launch()
	{
		
		//sets arc
		
		Vector3 direction = Vector3.zero;
		direction = new Vector3(x, y, z);
		
		//the tossing

		if(distance != 0)
		{
			PlayerMovement.active = false;
			//PlayerMovement.rbPlayer.constraints = RigidbodyConstraints.FreezeRotationZ;
			PlayerMovement.rbPlayer.useGravity = true;
			PlayerMovement.rbPlayer.isKinematic = false;
			PlayerMovement.rbPlayer.AddForce(direction * tossForce, ForceMode.Impulse);

		}
		
		else if(distance == 0)
		{
			Debug.LogWarning("Warning. Distance value for launcher is zero");
		}
	}

}


