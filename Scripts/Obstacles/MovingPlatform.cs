using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour 
{
	GameObject player;

	public int direction;
	public float stopTime;
	public float moveSpeed;
	public Transform[] paths;
	public bool rev;
	float dist = 0.75f;

	int currentPoint;
	float accel;
	float timer;
	float normalSpeed;
	
	void Awake () 
	{
		normalSpeed = moveSpeed;
		if(rev)
		{
			currentPoint = 1;
			direction = 1;
		}
		else
		{
			currentPoint = 0;
			direction = -1;
		}
		transform.position = paths[currentPoint].position;
	}
	
	
	void FixedUpdate () 
	{
		if(!Escape.paused)
		{
			player = PlayerSettings.player;

			accel = normalSpeed/2;
			if (Vector3.Distance(transform.position, paths[currentPoint].position) <= 3 && moveSpeed >= 0.1f) 
			{
				moveSpeed -= accel * Time.deltaTime;
			}
			else
			{
				moveSpeed += accel * Time.deltaTime;
			}

			if (transform.position == paths[currentPoint].position)
			{
				moveSpeed = 0;
				timer += 1 * Time.deltaTime;
			}

			if (timer >= stopTime)
			{
				currentPoint++;
				timer = 0;
				direction *= -1;
			}

			if (currentPoint >= paths.Length)
			{
				currentPoint = 0;
				
			}

			if(moveSpeed <= 0)
			{
				moveSpeed = 0;
			}

			if(moveSpeed >= normalSpeed)
			{
				moveSpeed = normalSpeed;
			}
			
			transform.position = Vector3.MoveTowards(transform.position, paths[currentPoint].position, moveSpeed * Time.deltaTime);
			/*if(Mathf.Abs (player.transform.position.x - transform.position.x) < 1 && (player.transform.position.y - transform.position.y < dist && player.transform.position.y - transform.position.y > 0) && Collisions.onMovingObj)
			{
				if(currentPoint == 0)
				{
					
					player.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
				}
				if(currentPoint == 1)
				{
					
					player.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
				}
			}
			*/
		}
	}
}