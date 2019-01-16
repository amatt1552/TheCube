using UnityEngine;
using System.Collections;

public class cylinderEmitterRotate : MonoBehaviour 
{

	public bool rev;
	public float stopTime;
	public float rotateSpeed;
	public float blastRate;
	float timer;
	bool Switch;
	bool stop;
	AudioSource blastSource;
	AudioClip blastClip;

	Object blast;
	Transform blastPos;
//--------------------------------------------------------------
	void Start () 
	{
		if(rev)
		{
			Switch = true;
		}

		blast = Resources.Load ("Prefabs/Blast");
		blastPos = transform.FindChild("blastPos");
		InvokeRepeating ("shoot", 1, blastRate);
		blastClip = (AudioClip)Resources.Load("AudioClips/blast");
		blastSource = gameObject.AddComponent<AudioSource>();
		blastSource.outputAudioMixerGroup = Sounds.GC.soundEffects;
		blastSource.clip = blastClip;
		blastSource.loop = false;
		blastSource.playOnAwake = false;
		blastSource.pitch = 2;
		blastSource.spatialBlend = 0.9f;
	}
	
//--------------------------------------------------------------
	void FixedUpdate () 
	{
		if(!Escape.paused)
		{
			if((transform.eulerAngles.z >= 45 && transform.eulerAngles.z <= 50)||(transform.eulerAngles.z <= 315 && transform.eulerAngles.z >= 310))
			{
				timer += 1 * Time.deltaTime;
				if(timer >= stopTime)
				{
					Switch = !Switch;
					timer = 0;
					stop = false;
				}
				else
				{
					stop = true;
				}
			}
			if(Switch && !stop)
			{

				transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
		
			}
			else if(!stop)
			{
				transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
			}
		}
	}

	void shoot() 
	{
		if(!Escape.paused)
		{
			blastSource.Play();
			Instantiate (blast, blastPos.position, blastPos.rotation);
		}
	}
}
