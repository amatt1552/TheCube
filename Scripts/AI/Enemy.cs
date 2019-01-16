using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float movespeed;
	float realSpeed;
	float rotateSpeed;
	public float pause = 0.5f;
	float pauseInc;
	public Transform[] paths;
	private int currentpoint;
	Transform enemyMesh;

	void Awake () 
	{
		enemyMesh = transform.FindChild("enemyMesh");
		transform.position = paths [0].position;
		currentpoint = 0;

	}

	void FixedUpdate() 
	{
		if(!Escape.paused)
		{
			rotateSpeed = Mathf.Sin(movespeed) * (realSpeed * 2);
			if(pauseInc >= pause)
			{
				if(Mathf.Abs(transform.position.x - paths[currentpoint].position.x) < 2)
				{
					if(realSpeed >= 0.5)
					{
						realSpeed -= 1 * Time.deltaTime;
					}
				}
				else
				{
					if(realSpeed <= movespeed)
					{
						realSpeed += 1 * Time.deltaTime;
					} 
				}

				if (transform.position == paths[currentpoint].position) 
				{
					currentpoint++;
					pauseInc = 0;
				}
				if (currentpoint >= paths.Length)
			    {
					currentpoint = 0;
					pauseInc = 0;
				}
				   
				transform.position = Vector3.MoveTowards(transform.position, paths[currentpoint].position, realSpeed * Time.deltaTime);

				if(currentpoint == 0)
				{
					enemyMesh.Rotate(Vector3.up * rotateSpeed);
				}
				else
				{
					enemyMesh.Rotate(Vector3.down * rotateSpeed);
				}
			}
			else
			{
				pauseInc += 1 * Time.deltaTime;
			}
		}
	}

}
