using UnityEngine;
using System.Collections;

public class smasher : MonoBehaviour 
{
	float delayP;
	float smashTimeP;
	float recessionTimeP;
	public float smashTime;
	public float recessionTime;
	public float delay;
	
	bool smash;
	
	int smashSpeed = 10;
	int recessionSpeed = 2;

	AudioSource smashSource;
	AudioClip smashClip;
	bool oneshot;
	
	public GameObject[] paths;

	void Start ()
	{
		smashClip = (AudioClip)Resources.Load("AudioClips/smash");
		smashSource = gameObject.AddComponent<AudioSource>();
		smashSource.outputAudioMixerGroup = Sounds.GC.soundEffects;
		smashSource.clip = smashClip;
		smashSource.loop = false;
		smashSource.playOnAwake = false;
		smashSource.spatialBlend = 0.9f;
	}

	void FixedUpdate () 
	{
		if(!Escape.paused)
		{
			delayP += 1 * Time.deltaTime;
			if(delayP >= delay)
			{
				Smash ();
			}
		}
	}
	void Smash()
	{
		if(transform.position == paths[0].transform.position)
		{
			smashTimeP += 1 * Time.deltaTime;
		}
		if(transform.position == paths[1].transform.position)
		{
			recessionTimeP += 1 * Time.deltaTime;
		}
		if(smashTimeP >= smashTime)
		{
			smash = true;
			smashTimeP = 0;
		}
		if(recessionTimeP >= recessionTime)
		{
			smash = false;
			recessionTimeP = 0;

		}
		
		if(smash)
		{
			transform.position = Vector3.MoveTowards(transform.position, paths[1].transform.position, smashSpeed * Time.deltaTime);
			if (transform.position == paths[1].transform.position && !oneshot)
			{
				smashSource.Play ();
				oneshot = true;
			}
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, paths[0].transform.position, recessionSpeed * Time.deltaTime);
			oneshot = false;
		}
	}
	/*void SequenceBased()
	{
		if(smash)
		{
			transform.position = Vector3.MoveTowards(transform.position, paths[1].transform.position, smashSpeed * Time.deltaTime);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, paths[0].transform.position, recessionSpeed * Time.deltaTime);
		}
	}
	*/
}


