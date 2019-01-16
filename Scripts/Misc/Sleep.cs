using UnityEngine;
using System.Collections;

public class Sleep 
{
	public bool timeEnd;
	public bool countUp;

	float time;
	float timer;

	void Start () 
	{

	}

	public void Wait(float newTime)
	{
		time = newTime;
		timeEnd = false;
		countUp = true;
		Count();
	}
	public void Count()
	{
		if (countUp)
		{
			timer += 0.02f;

		}
		if (timer >= time)
		{
			timeEnd = true;
			countUp = false;
			timer = 0;
		}

	}
}
