using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	int currentPoint;
	public Transform[] paths;

	//---------------------------------------------------

	void Awake () 
	{
		currentPoint = 0;
	}
	
	//---------------------------------------------------

	void FixedUpdate() 
	{
		menuCube.currentPoint = currentPoint;
		transform.position = Vector3.MoveTowards(transform.position, paths[currentPoint].transform.position, 400f * Time.deltaTime); 

	}

	//---------------------------------------------------

	public void ToMenu()
	{
		currentPoint = 0;
		menuCube.movingMenu = true;
	}

	//---------------------------------------------------

	public void ToSelection()
	{
		currentPoint = 1;
		menuCube.movingMenu = true;
	}

	//---------------------------------------------------

	public void ToSettings()
	{
		currentPoint = 2;
		menuCube.movingMenu = true;
	}
}
