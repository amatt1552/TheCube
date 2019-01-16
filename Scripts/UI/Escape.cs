using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Escape : MonoBehaviour 
{
	GameObject chosenObject;
	public static bool paused;
	public static bool usingMenu;
	bool escPressed, escPressed2;
	public static bool usingSettings;
	public GameObject settings, menu;

	bool continueGame;
	bool pressed;


//------------------------------------------------------
	void Awake () 
	{
		usingMenu = false;
		usingSettings = false;

	}
	
//------------------------------------------------------
	void Update () 
	{
		paused = usingMenu || usingSettings;

		Menu();
		EscCheck();

	}

	void EscCheck()
	{
		if(Input.GetKey(KeyCode.Escape) && Collisions.ground && !usingSettings)
		{
			escPressed = true;
		}
		if(Input.GetKeyUp(KeyCode.Escape) && escPressed && !usingSettings)
		{
			usingMenu = !usingMenu;
			escPressed = false;
		}

		if(Input.GetKey(KeyCode.Escape) && Collisions.ground && usingSettings)
		{
			escPressed2 = true;
		}
		if(Input.GetKeyUp(KeyCode.Escape) && escPressed2 && usingSettings)
		{
			usingMenu = true;
			usingSettings = false;
			escPressed2 = false;
		}
	}

	void Menu()
	{
		if(usingMenu)
		{
			On(menu);
		}
		else
		{
			Off(menu);
		}

		if(usingSettings)
		{
			On(settings);
		}
		else
		{
			Off(settings);
		}
	}

	public void Continue()
	{
		usingMenu = false;

	}
	public void inSettings()
	{
		usingSettings = true;
		usingMenu = false;
	}
	public void back()
	{
		usingMenu = true;
		usingSettings = false;
	}
	void Off(GameObject chosenObject)
	{
		chosenObject.SetActive(false);
	}
	void On(GameObject chosenObject)
	{
		chosenObject.SetActive(true);
	}


}
