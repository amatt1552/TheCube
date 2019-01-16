using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TheEnd : MonoBehaviour 
{

	bool next;
	int currentPoint;
	public Transform[] cameraPos;
	float timer;

	void Start () 
	{
		currentPoint = 0;
	}
	

	void FixedUpdate () 
	{

		timer += 1 * Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, cameraPos[currentPoint].position, 5);
		if(next)
		{
			print ("next");
			currentPoint++;
			if (currentPoint > 2)
			{
				SceneManager.LoadScene(0);
			}
			next = false;
		}
		if(timer >= (5 + currentPoint))
		{
			Next();
		}
	}
	void Next ()
	{
		next = true;
		timer = 0;
	}
}
