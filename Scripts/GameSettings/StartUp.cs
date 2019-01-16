using UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour 
{

	void Start () 
	{
		if(LevelSettings.GC.GetCurrentLevel() == 0)
		{
			Sounds.GC.playSound("menu");
		}
		else
		{
			Sounds.GC.playSound("playing");
		}
		ColorFade.GC.fadeTime = 1f;
		ColorFade.GC.startColor = Color.white;
		ColorFade.GC.newColor = new Color(1,1,1,0);
		ColorFade.GC.startInv = false;
		ColorFade.GC.start = true;


	}


	void Update () 
	{
		
	}
}
