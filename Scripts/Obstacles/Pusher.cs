using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour 
{

	float pushTimeP;
	float recessionTimeP;
	public float pushTime;
	public float recessionTime;
	public Collider killer;

	bool push;

	int pushSpeed = 4;
	int recessionSpeed = 2;

	public GameObject[] paths;

	void FixedUpdate () 
	{
		if(!Escape.paused)
		{
			pushTimeP += 1 * Time.deltaTime;
			if(pushTimeP >= pushTime)
			{
				push = true;
				recessionTimeP += 1 * Time.deltaTime;
			}
			if(recessionTimeP >= recessionTime)
			{
				push = false;
				recessionTimeP = 0;
				pushTimeP = 0;
			}

			if(push)
			{
				killer.enabled = true;
				GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, paths[1].transform.position, pushSpeed * Time.deltaTime));
			}
			else
			{
				killer.enabled = false;
				GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, paths[0].transform.position, recessionSpeed * Time.deltaTime));
			}
		}
	}
}
