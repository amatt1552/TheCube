using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Elevator : MonoBehaviour 
{
	public bool flip;
	public int speed;
	int direction;
	public bool ending;
	bool oneShot, oneShot2;
	public Transform endingPoint;
	public Transform startingPoint;
	GameObject player;
	void Start ()
	{
		if(startingPoint == null && Application.isPlaying)
		{
			GameObject startingGO = new GameObject("startingGO");
			startingGO.transform.position = transform.position;
			startingPoint = startingGO.transform;

		}
		player = PlayerSettings.player;
	}

	void Update () 
	{
		if(Application.isPlaying)
		{
			if(Vector3.Distance(player.transform.position, transform.position) < 0.25 && ending)
			{
				PlayerMovement.active = false;
				PlayerMovement.usingGravity = false;
				transform.position = Vector3.MoveTowards(transform.position, endingPoint.position, speed * Time.deltaTime);
				player.transform.position = Vector3.MoveTowards(player.transform.position, transform.FindChild("eliftPoint").transform.position, (speed * 2) * Time.deltaTime);

				Invoke("LoadLater", 1);
			}
			else if(!ending)
			{
				if(transform.position != endingPoint.position)
				{
					PlayerMovement.active = false;
					PlayerMovement.usingGravity = false;
					player.transform.position = Vector3.MoveTowards(player.transform.position, transform.FindChild("eliftPoint").transform.position, 100 * Time.deltaTime);
					transform.position = Vector3.MoveTowards(transform.position, endingPoint.position, speed * Time.deltaTime);
				}
				else if(!oneShot2)
				{
					PlayerMovement.active = true;
					PlayerMovement.usingGravity = true;
					oneShot2 = true;
				}
			}

		}
		else
		{
			if(flip)
			{
				transform.parent.eulerAngles = new Vector3(0,180,0);
				direction = 1;
			}
			else
			{
				transform.parent.eulerAngles = new Vector3(0,0,0);
				direction = -1;
			}
					
		}
		Vector3 temp = new Vector3(0, 0.6f, 0);
		Debug.DrawLine(startingPoint.position - temp, endingPoint.position - temp, Color.green);
		Debug.DrawLine(endingPoint.position - temp, (endingPoint.position - temp)+ new Vector3(direction,0,0), Color.green);
	}

	void LoadLater()
	{
		if(!oneShot)
		{
			LevelSettings.GC.LevelComplete();
			oneShot = true;
		}
	}
}


