using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour 
{
	bool duplicateCheck;

	void Update () 
	{
		if (!duplicateCheck)
		{ 
			Object.DontDestroyOnLoad(this);
			duplicateCheck = true;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}


}

