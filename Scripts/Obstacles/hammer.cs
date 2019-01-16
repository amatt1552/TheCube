using System.Collections;
using UnityEngine;
public class hammer : MonoBehaviour
{
	Rigidbody rb;
	Quaternion newRotation;
	public float speed;
	float hammerRotation;
	float hammerSpeed;
	public int maxSpeed;
	int maxRotation = 90;
	public bool flip;
	bool slowingDown;
	bool oneShot;
	AudioSource swooshSource;
	bool started;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		swooshSource = Sounds.GC.AddSource(gameObject,"swoosh",1);
		swooshSource.spatialBlend = 0.9f;
	}

	void Update()
	{
		if(!Escape.paused)
		{
			ChangeRotation();
			ChangeSpeed();
			PlaySwoosh();
		}
	}

	void FixedUpdate()
	{
		newRotation.eulerAngles = new Vector3(hammerRotation,transform.parent.eulerAngles.y,0);
		rb.MoveRotation(newRotation);
	}

	void ChangeRotation()
	{
		if(flip)
		{
			hammerRotation -= hammerSpeed * Time.deltaTime;
		}
		else
		{
			hammerRotation += hammerSpeed * Time.deltaTime;
		}
	}
	void PlaySwoosh()
	{
		
		if(newRotation.eulerAngles.x > 0 && newRotation.eulerAngles.x < 20 && started)
		{
			swooshSource.Play();
		}
	}
	void ChangeSpeed()
	{
		if(hammerSpeed < maxSpeed && !slowingDown)
		{
			hammerSpeed += speed * Time.deltaTime;

		}

		if((newRotation.eulerAngles.x < maxRotation && newRotation.eulerAngles.x > maxRotation - 30) || (newRotation.eulerAngles.x > 360 - maxRotation && newRotation.eulerAngles.x < 360 - maxRotation + 30) || newRotation.eulerAngles.y == 180)
		{
			if(!oneShot)
			{
				slowingDown = true;
				oneShot = true;
			}

		}
		else
		{
			oneShot = false;
		}


		if(slowingDown)
		{
			FlipSlow();

		}

	}

	void FlipSlow()
	{
		started = true;
		if(hammerSpeed > 0)
		{
			hammerSpeed -= (speed * 2 * Time.deltaTime);
		}

		if(hammerSpeed <= 0)
		{
			hammerSpeed = 0;
			flip = !flip;
			slowingDown = false;
		}
	}

	void FlipNow()
	{
		started = true;
		flip = !flip;
	}
}