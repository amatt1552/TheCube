using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour 
{
	#region variables

	//inputs

	public static bool movingLeft;
	public static bool movingRight;

	//particles

	ParticleSystem.EmissionModule em;
	Transform slidingParticlesOrigin;
	ParticleSystem slidingPart;

	//sounds

	AudioSource movingSource;

	AudioSource hitSource;

	bool hitOneShotLeft, hitOneShotRight, hitOneShotTop;

	GameObject[] movingPlatforms;
	GameObject chosenMovingPlatform;
	Transform[] paths;

	//switches

	bool onMovingObject;
	bool oneShotFall;
	public static bool usingGravity; 
	public static bool moving;
	public static bool active;
	bool movingCamera;
	int maxDistance = 4;

	//collisions
	
	public static bool falling;
	public bool ground, top, left, right;
	public static bool touchingCollider;

	//speeds

	float stepSpeed = 0.5f;
	float leftMaxSpeed = 4, rightMaxSpeed = 4;
	float leftNormalMaxSpeed, rightNormalMaxSpeed;
	float leftSpeedBoost = 10, rightSpeedBoost = 10;
	float leftSpeed, rightSpeed, movingSpeed;
	int movingDirection;
	float leftSpeedRec, rightSpeedRec, groundSpeedRec, topSpeedRec;
	public float camSpeed;

	int cases;
	public float friction;

	//jump

	float jumpForce;
	float jumpAccel = 10;
	public bool jump;

	//gravity

	float fallGrav;
	float grav;
	float gravAccel = 10;
	public static Rigidbody rbPlayer;

	//timers

	float timer;
	bool oneShot;
	
	#endregion

	//------------------------------------------------------------------------

	void Start () 
	{

		hitSource = Sounds.GC.getSource("playerHit");
		movingSource = Sounds.GC.getSource("playerMoving");

		slidingParticlesOrigin = transform.Find("slidingParticlesOrigin");
		slidingPart = slidingParticlesOrigin.GetComponent<ParticleSystem>();
		em = slidingPart.emission;

		rbPlayer = GetComponent<Rigidbody>();
		usingGravity = true;
		leftNormalMaxSpeed = leftMaxSpeed;
		rightNormalMaxSpeed = rightMaxSpeed;

		active = true;

	}

	//------------------------------------------------------------------------
	void Update()
	{

		if(leftSpeed >= leftMaxSpeed - 1)
		{
			movingLeft = true;
		}
		else 
		{
			movingLeft = false;
		}

		if(rightSpeed >= rightMaxSpeed - 1)
		{
			movingRight = true;
		}
		else
		{
			movingRight = false;
		}
		if(movingLeft || movingRight)
		{
			moving = true;
		}
		else
		{
			moving = false;
		}

		if(!jump)
		{
			ground = Collisions.ground;
		}
		else
		{
			ground = false;
		}

		top = Collisions.top;
		left = Collisions.left;
		right = Collisions.right;
	}

	void FixedUpdate () 
	{
		
		if(!falling && !LevelSettings.GC.isLevelComplete() && !PlayerSettings.dead && !Escape.paused && !movingCamera)
		{
			
			if(ColorFade.GC.finished && active)
			{
				
				Movement();
				SpeedChecker();
				Limits();
				Jump();
				Boost();
				InBlocks();

				if(usingGravity)
				{
					Gravity ();
				}
				else
				{
					ground = false;
					grav = 0;
				}

			}

			HitSound();
			MovingSound();

		}
		else
		{
			jumpForce = 0;
			leftSpeed = 0;
			rightSpeed = 0;
			movingSource.Stop();
			em.enabled = false;
		}
		MoveCamera();
		OnMovingObj();
		Falling ();

	}

	//------------------------------------------------------------------------

	void Limits()
	{

		#region left
		if (leftSpeed > leftMaxSpeed) 
		{
			leftSpeed = leftMaxSpeed;
		}
		if (leftSpeed < 0) 
		{
			leftSpeed = 0;
		}
		#endregion

		#region right
		if (rightSpeed > rightMaxSpeed) 
		{
			rightSpeed = rightMaxSpeed;
		}
		if (rightSpeed < 0) 
		{
			rightSpeed = 0;
		}
		#endregion
	
	}

	//------------------------------------------------------------------------

	void Jump()
	{
		
		if (Settings.jump && ground) 
		{
			jumpForce = 5f;
			transform.Translate (Vector3.up * jumpForce * Time.deltaTime);
			jump = true;

		}
		
		if (jump) 
		{
			jumpForce -= jumpAccel * Time.deltaTime;
		}
		
		if (jumpForce <= 0 || top)
		{
			jump = false;
			jumpForce = 0;
		}
		if(!ground)
		{
			transform.Translate (Vector3.up * jumpForce * Time.deltaTime);
		}
	}

	//------------------------------------------------------------------------

	void Gravity()
	{
		
		if (!ground && !jump)
		{
			grav += gravAccel * Time.deltaTime;
			transform.Translate (Vector3.down * grav * Time.deltaTime);
		} 
		else 
		{
			grav = 0;	
		}

	}

	//------------------------------------------------------------------------

	void Movement ()
	{

		Vector3 dir = new Vector3 (movingDirection, 0, 0);

		transform.Translate(dir * movingSpeed * Time.deltaTime);

		#region left
		if (Settings.left && !movingRight && !left ) 
		{
			leftSpeed += friction * Time.deltaTime;

		} 
		
		else if(leftSpeed != 0)
		{
			leftSpeed -= friction * Time.deltaTime;
		}

		if (!left) 
		{
			transform.Translate(Vector3.left * leftSpeed * Time.deltaTime);
		}
		else
		{
			leftSpeed = 0;
		}
		#endregion

		#region right
		if (Settings.right && !movingLeft && !right) 
		{
			rightSpeed += friction * Time.deltaTime;

		} 
		
		else if(rightSpeed != 0)
		{
			rightSpeed -= friction * Time.deltaTime;
		}

		if (!right) 
		{
			transform.Translate(Vector3.right * rightSpeed * Time.deltaTime);
		}
		else
		{
			rightSpeed = 0;
		}
		#endregion
	
	}
	
	//---------------------------------------------------------------------------------s

	void Falling()
	{
		if((!Collisions.falling && !PlayerSettings.dead) || (timer >= 2 && touchingCollider) )
		{
			cases = 0;
		}

		else if(Collisions.falling && ground) 
		{
			cases = 1;

		}

		if(oneShot && touchingCollider)
		{
			timer += 1 * Time.deltaTime;
		}
		else 
		{
			timer = 0;
		}

		if(cases != 0)
		{
			oneShotFall = false;
		}
		print(cases);
		switch(cases)
		{

		case 0:

			if(!oneShotFall)
			{
				oneShot = false;

				rbPlayer.isKinematic = true;
				rbPlayer.useGravity = false;
				Recover();
				falling = false;
				touchingCollider = false;
				oneShotFall = true;
			}
			break;

		case 1:
		
			rbPlayer.isKinematic = false;
			rbPlayer.useGravity = true;
			//rbPlayer.AddForce (Vector3.left * leftSpeed);
			falling = true;
			oneShot = true;

			break;
		
		case 2:

			rbPlayer.isKinematic = false;
			rbPlayer.useGravity = true;
			rbPlayer.AddForce (Vector3.right * rightSpeed);
			falling = true;
			oneShot = true;

			break;
		
		default:

			break;
		
		}

	}

	//---------------------------------------------------------------------------------

	void InBlocks()
	{
		if(Collisions.inGround)
		{
			transform.Translate(Vector3.up * (stepSpeed + groundSpeedRec / 2) * Time.deltaTime);
			ground = true;
			print ("inGround");
		}
		if(Collisions.inTop)
		{
			transform.Translate(Vector3.down * (stepSpeed + topSpeedRec / 2) * Time.deltaTime);
			top = true;
			print ("inTop");
		}
		if(Collisions.inLeft)
		{
			transform.Translate(Vector3.right * (stepSpeed + leftSpeedRec / 2) * Time.deltaTime);
			left = true;
			print ("inLeft");
		}
		if(Collisions.inRight)
		{
			transform.Translate(Vector3.left * (stepSpeed + rightSpeedRec / 2) * Time.deltaTime);
			right = true;
			print ("inRight");
		}
	}

	//---------------------------------------------------------------------------------

	public void Recover()
	{
		if(!PlayerSettings.dead)
		{
			Vector3 pos = transform.rotation * Vector3.down;
			if(transform.eulerAngles != Vector3.zero)
			{
				transform.Translate (pos * 1 * Time.deltaTime);
			}
			transform.eulerAngles = new Vector3 (0,0,0);
			Collisions.falling = false;
			timer = 0;
		}
	}

	//---------------------------------------------------------------------------------

	void OnMovingObj()
	{
		GameObject[] movingObjs;
		if(GameObject.FindWithTag("moving") != null)
		{
			movingObjs = GameObject.FindGameObjectsWithTag("moving");
			for(int i = 0; i < movingObjs.Length ;i++)
			{
				if(Vector3.Distance(transform.position, movingObjs[i].transform.position) < 0.9f)
				{
					movingSpeed = movingObjs[i].GetComponent<MovingPlatform>().moveSpeed;
					movingDirection = movingObjs[i].GetComponent<MovingPlatform>().direction;
				}
				else if(ground)
				{
					movingSpeed = 0;
				}
			}
		}

	}

	//---------------------------------------------------------------------------------

	void OnCollisionStay(Collision col)
	{
		//if(col.gameObject.tag != "theVoid" && col.gameObject.tag != "ocean")
		//{
			touchingCollider = true;
			
		//}

	}

	void OnCollisionExit()
	{
		touchingCollider = false;
	}

	//---------------------------------------------------------------------------------

	void MoveCamera()
	{
		
		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");

		if(Settings.cameraMovement && ground)
		{
			movingCamera = true;
			camera_follow.follow = false;
			if(Camera.main.transform.position.x - transform.position.x <= maxDistance && x > 0)
			{
				Camera.main.transform.Translate(new Vector3(Mathf.Abs(x),0,0) * camSpeed * Time.deltaTime);
			}
			if(Camera.main.transform.position.x - transform.position.x >= -maxDistance && x < 0)
			{
				Camera.main.transform.Translate(new Vector3(Mathf.Abs(x),0,0) * -camSpeed * Time.deltaTime);
			}

			if(Camera.main.transform.position.y - transform.position.y <= maxDistance && y > 0)
			{
				Camera.main.transform.Translate(new Vector3(0, Mathf.Abs(y), 0) * camSpeed * Time.deltaTime);
			}
			if(Camera.main.transform.position.y - transform.position.y >= -maxDistance && y < 0)
			{
				Camera.main.transform.Translate(new Vector3(0, Mathf.Abs(y), 0) * -camSpeed * Time.deltaTime);
			}



		}
		else
		{
			movingCamera = false;
			camera_follow.follow = true;
		}
	}

	//----------------------------------------------------------------------------------

	void Boost()
	{
		if(SpeedBoost.speedIncrease)
		{
			leftMaxSpeed = leftSpeedBoost;
			rightMaxSpeed = rightSpeedBoost;

		}
		else if(leftMaxSpeed > leftNormalMaxSpeed && rightMaxSpeed > rightNormalMaxSpeed)
		{
			leftMaxSpeed -= leftSpeedBoost * Time.deltaTime;
			rightMaxSpeed -= rightSpeedBoost * Time.deltaTime;
		}
		else
		{
			leftMaxSpeed = leftNormalMaxSpeed;
			rightMaxSpeed = rightNormalMaxSpeed;
		}
	}

	//---------------------------------------------------------------------------------

	void MovingSound()
	{
		if(leftSpeed > 0.2)
		{
			movingSource.pitch = leftSpeed;
		}
		else if(rightSpeed > 0.2)
		{
			movingSource.pitch = rightSpeed;
		}
		else
		{
			movingSource.pitch = 0;
		}

		if (((leftSpeed > 0.2 && !left) || (rightSpeed > 0.2 && !right)) && ground)
		{
			if(!movingSource.isPlaying)
			{

				movingSource.Play();
				em.enabled = true;

			}
		}
		else
		{
			movingSource.Stop();
			em.enabled = false;
		}
	}

	//---------------------------------------------------------------------------------

	void HitSound()
	{

		//ground

		if(ground && groundSpeedRec > 1f)
		{
			hitSource.Play();
			groundSpeedRec = 0;
		}
		else if(!hitSource.isPlaying)
		{
			hitSource.Stop();

		}

		//top

		if(top && topSpeedRec > 2f)
		{
			hitSource.Play();
			topSpeedRec = 0;
		}
		else if(!hitSource.isPlaying)
		{
			hitSource.Stop();
		}
		
		//right

		if(right && rightSpeedRec > (friction / rightMaxSpeed))
		{
			hitSource.Play();
			rightSpeedRec = 0;
		}
		else if(!hitSource.isPlaying)
		{
			hitSource.Stop();

		}
		
		//left

		if(left && leftSpeedRec > (friction / leftMaxSpeed))
		{
			hitSource.Play();
			leftSpeedRec = 0;
		}
		else if(!hitSource.isPlaying)
		{
			hitSource.Stop();
		}

	}

	void SpeedChecker()
	{
		if(!Collisions.inTop && !top)
		{
			topSpeedRec = jumpForce;
		}
		if(!Collisions.inGround && !ground)
		{
			groundSpeedRec = grav; 
		}
		if(!Collisions.inLeft && !left)
		{
			leftSpeedRec = leftSpeed;
		}
		if(!Collisions.inRight && !right)
		{
			rightSpeedRec = rightSpeed;
		}
	}
}