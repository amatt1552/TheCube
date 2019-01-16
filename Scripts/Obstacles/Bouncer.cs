using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour 
{
	float inc;
	GameObject player;
	public float launchTime;
	public int launchForce;
	public float gravity = 10;
	float launch;
	bool launched;
	public bool oneshot;
	//bool tossed;

	void Start () 
	{
		player = GameObject.Find ("player");
	}
	

	void FixedUpdate () 
	{

		if(inc > launchTime)
		{
			launch = launchForce;
			inc = 0;
			launched = true;
		}
		if(launched)
		{
			oneshot = true;
			Launch();
		}

	}
	void OnTriggerStay()
	{
		inc += 1 * Time.deltaTime;

	}
	void OnTriggerExit()
	{
		inc = 0;
	}

	void Launch()
	{
		PlayerMovement.usingGravity = false;

		Vector3 dir =  transform.InverseTransformDirection(Vector3.up);

		if(!Collisions.ground && !Collisions.top)
		{
			launch -= gravity * Time.deltaTime;



			if((launch <= 0 || Collisions.ground || Collisions.top ) && oneshot)
			{
				
				launch = 0;
				launched = false;
				PlayerMovement.usingGravity = true;
				oneshot = false;
			}
		}

		player.transform.Translate (dir * launch * Time.deltaTime);

	}
}
