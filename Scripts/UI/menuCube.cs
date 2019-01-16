using UnityEngine;
using System.Collections;

public class menuCube : MonoBehaviour 
{
	public GameObject[] paths;
	public static int currentPoint;
	public static bool movingMenu;
	float timer1, timer2;
	bool oneShot;
	int i;
	float inc;

	AudioSource movingSource;
	AudioClip movingSound;

	ParticleSystem.EmissionModule em;
	Transform slidingParticlesOrigin;
	ParticleSystem slidingPart;

	void Start () 
	{
		
		oneShot = true;
		inc = Random.Range(0,10);

		movingSound = (AudioClip)Resources.Load("AudioClips/cubeMoving");
		movingSource = gameObject.AddComponent<AudioSource>();
		movingSource.clip = movingSound;
		movingSource.loop = true;
		movingSource.playOnAwake = false;

		slidingParticlesOrigin = transform.FindChild("slidingParticlesOrigin");
		slidingPart = slidingParticlesOrigin.GetComponent<ParticleSystem>();
	}

	void FixedUpdate () 
	{
		
		em = slidingPart.emission;
		if( transform.position == paths[currentPoint].transform.position)
		{
			movingMenu = false;
		}

		Random.seed = (int)inc;
		inc += 1 * Time.deltaTime;

		if(!movingMenu)
		{
			Idle();

		}
		else
		{
		//	transform.position = Vector3.MoveTowards(transform.position, paths[currentPoint].transform.position, 125 * Time.deltaTime);
		//	em.enabled = true;
		}
	}

	void Idle()
	{
		timer1 += 1 * Time.deltaTime;

		if (inc >= 10)
		{
			inc = 0;
		}

		if(timer1 >= 4)
		{
			
			if(oneShot)
			{

				if(Mathf.Abs(transform.position.x - paths[currentPoint].transform.position.x) <= 0.1f)
				{
					
					i = Random.Range(0,10);
					oneShot = false;
					print(i);
				}
				else 
				{
					i = 5;
				}
			}

			timer2 += 1 * Time.deltaTime;

			if(timer2 <= 2 && (i >= 0 && i < 5))
			{
				transform.Translate(Vector3.right * 1 * Time.deltaTime);
				em.enabled = true;
			}
			else if(timer2 <= 2 && (i > 5 && i <= 10))
			{
				transform.Translate(Vector3.left * 1 * Time.deltaTime);
				em.enabled = true;
			}
			else if(timer2 <= 2 && i == 5)
			{
				transform.position = Vector3.MoveTowards(transform.position, paths[currentPoint].transform.position, 1 * Time.deltaTime);
				em.enabled = true;
			}
			else
			{
				timer1 = 0;
				em.enabled = false;
			}
		}
		else
		{
			timer2 = 0;
			oneShot = true;
		}
	}
}
