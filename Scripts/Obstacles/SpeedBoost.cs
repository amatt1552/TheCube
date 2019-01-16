using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour	
{	
	#region public variables

	public static bool speedIncrease;

	#endregion	

	#region private variables

	#endregion	
		
	void Update()
	{
		if(PlayerSettings.dead)
		{
			speedIncrease = false;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if(other.tag == "player" && !PlayerSettings.dead)
		{
			speedIncrease = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "player")
		{
			speedIncrease = false;
		}
	}
}