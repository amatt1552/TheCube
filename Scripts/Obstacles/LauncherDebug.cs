using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class LauncherDebug : MonoBehaviour
{
	Transform launchPos;
	Launcher launcher;
	void Awake()
	{
		launchPos = transform.FindChild("launchPos");
		launcher = GetComponent<Launcher>();
	}
	void Update()
	{
		if(transform.eulerAngles.y <= 185 && transform.eulerAngles.y >= 175)
		{
			launchPos.position = transform.position + new Vector3(0, 0, launcher.distance);

		}
		else
		{
			launchPos.position = transform.position + new Vector3(0, 0, -launcher.distance);
		}

	}
}
