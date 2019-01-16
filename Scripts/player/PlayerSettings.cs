using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour 
{
	public static GameObject playerMesh;
	public Image warning;
	GameObject spawn;
	float spawnSpeed;
	float drownSpeed;
	float explodeSpeed;

	String cases;
	AudioSource deathSource;
	AudioClip deathSound;
	bool deathOneShot;
	bool fixOneShot;
	bool disOneShot;

	Sleep sleep = new Sleep();

	Transform deathParticlesOrigin;
	ParticleSystem deathPart;

	Transform disintegrateParticlesOrigin;
	ParticleSystem disintegratePart;

	Transform disintegrateParticlesOrigin2;
	ParticleSystem disintegratePart2;

	public static GameObject player;
	public static bool dead;
	public float UVscalex, UVscaley;
	public static bool colorReverted;

	//player color

	Color playerColor;

	public static Material playerMaterial;
	Material noTex;
	public static Texture chosenTexture;

	//Vector3 colPosition;

	//-------------------------------------------------------------------------------- 

	void Awake () 
	{
		playerMaterial = (Material)Resources.Load("Materials/playerMaterial");
		noTex = (Material)Resources.Load("Materials/playerMaterialNoTex");
		player = this.gameObject;
		playerMesh = GameObject.Find ("playerMesh");//make this static later and use that for other classes maybe.
		spawn = GameObject.Find ("spawn");
		dead = false;
		try
		{
			deathParticlesOrigin = transform.FindChild("deathParticlesOrigin");
			deathPart = deathParticlesOrigin.GetComponent<ParticleSystem>();

			disintegrateParticlesOrigin = transform.FindChild("blasterDeathA");
			disintegratePart = disintegrateParticlesOrigin.GetComponent<ParticleSystem>();

			disintegrateParticlesOrigin2 = transform.FindChild("blasterDeathB");
			disintegratePart2 = disintegrateParticlesOrigin2.GetComponent<ParticleSystem>();
			deathOneShot = true;
			fixOneShot = true;
		}
		catch
		{

		}
	}
	void Start () 
	{
		deathSource = Sounds.GC.getSource("death");
	}
	
	//-------------------------------------------------------------------------------- 

	void FixedUpdate () 
	{
		if(!dead)
		{
			if(Settings.selfDestruct)
			{
				explodeSpeed += 0.3f * Time.deltaTime;
				warning.fillAmount = explodeSpeed;
				if(explodeSpeed >= 1)
				{
					dead = true;
					cases = "hit";
					explodeSpeed = 1;
				}

			}
			else 
			{
				explodeSpeed -= 1 * Time.deltaTime;	
				warning.fillAmount = explodeSpeed;

			}
		}
		else
		{
			explodeSpeed = 0;
			warning.fillAmount = 0;
		}

		if(explodeSpeed < 0)
		{
			explodeSpeed = 0;
		}
		try
		{
			if (dead) 
			{
				fixOneShot = true;
				sleep.Wait(2);
				Deaths();

				if(sleep.timeEnd)
				{
					transform.position = spawn.transform.position;
					PlayerMovement.rbPlayer.isKinematic = true;
					PlayerMovement.rbPlayer.useGravity = false;
					//transform.eulerAngles = new Vector3(0,0,0);
				}
			}
			else if(fixOneShot)
			{
				PlayerMovement.rbPlayer.isKinematic = true;
				PlayerMovement.rbPlayer.useGravity = false;
				PlayerMovement.rbPlayer.angularDrag = 0.0f;
				transform.eulerAngles = new Vector3(0,0,0);
				disOneShot = true;
				deathOneShot = true;
				playerMesh.GetComponent<MeshRenderer>().enabled = true;
				player.GetComponent<BoxCollider>().enabled = true;
				camera_follow.follow = true;
				fixOneShot = false;
			}
				
			if(transform.position == spawn.transform.position)
			{
				dead = false;
			}

		}
		catch
		{
			
		}


		ColorChange();
		ChangeTexture();
		//UV();
	}

	//-------------------------------------------------------------------------------- 

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "enemy") 
		{
			dead = true;
			cases = "hit";
		}
		if (other.tag == "blaster") 
		{
			dead = true;
			cases = "disintegrated";
		}
		if (other.tag == "ocean")
		{
			dead = true;
			cases = "drowned";
			drownSpeed = 4;
		}
		if (other.tag == "theVoid")
		{
			dead = true;
			cases = "fallen";
		}
		if (other.tag == "pusher")
		{
			dead = true;
			cases = "pusher";
		}

		if (other.tag == "smasher")
		{
			dead = true;
			cases = "hit";
		}
		if (other.tag == "hammer")
		{
			dead = true;
			cases = "hammer";
			//colPosition = other.transform.position;
		}

	}

	//-------------------------------------------------------------------------------- 

	void ColorChange()
	{

		playerMaterial.color = playerColor;
		noTex.color = playerColor;

		if(LevelSettings.GC.GetCurrentLevel() != 0)
		{
			if(SpeedBoost.speedIncrease && !dead && PlayerMovement.moving && Settings.normalPlayerColor.r <= 1)
			{
				colorReverted = false;
				playerColor.r += 1 * Time.deltaTime;
			}
			else if(SpeedBoost.speedIncrease && !dead && PlayerMovement.moving)
			{
				colorReverted = false;
				if(Settings.normalPlayerColor.g >= 1.0f)
				{
					playerColor.g -= 1 * Time.deltaTime;
				}
				else if(Settings.normalPlayerColor.b > 0)
				{
					playerColor.b -= 1 * Time.deltaTime;
				}
			}
			else if(playerColor.r > Settings.normalPlayerColor.r)
			{
				playerColor.r -= 1 * Time.deltaTime;
			}
			else if(playerColor.g > Settings.normalPlayerColor.g)
			{
				playerColor.g -= 1 * Time.deltaTime;
			}
			else if(playerColor.b > Settings.normalPlayerColor.b)
			{
				playerColor.b -= 1 * Time.deltaTime;
			}
			else
			{
				colorReverted = true;
				playerColor = Settings.normalPlayerColor;
			}
		}
		else
		{
			colorReverted = true;
			playerColor = Settings.normalPlayerColor;
		}

	}

	//-------------------------------------------------------------------------------- 

	void Deaths()
	{
		Vector3 dir =  Vector3.down;//transform.InverseTransformDirection(Vector3.down);
		Vector3 dirUp =  Vector3.up;//transform.InverseTransformDirection(Vector3.up);

		switch(cases)
		{
		
		case "hit":

			camera_follow.follow = false;
			playerMesh.GetComponent<MeshRenderer>().enabled = false;
			player.GetComponent<BoxCollider>().enabled = false;
			deathPart.Emit(40);

			if(deathOneShot)
			{
				deathSource.Play();
				deathOneShot = false;
			}
			else if(!deathSource.isPlaying)
			{
				deathSource.Stop();
			}
			
			break;

		case "drowned":

			if(PlayerMovement.rbPlayer.isKinematic)
			{
				
				camera_follow.follow = false;
				player.GetComponent<BoxCollider>().enabled = false;
				if(drownSpeed >= 1)
				{
					drownSpeed -= 4 * Time.deltaTime;
				}

				transform.Translate(dir * Time.deltaTime, Space.World);

			}
			else 
			{
				camera_follow.follow = false;
				PlayerMovement.rbPlayer.angularDrag = 2f;
				player.GetComponent<BoxCollider>().enabled = false;
				//PlayerMovement.rbPlayer.useGravity = true;

				if(PlayerMovement.rbPlayer.velocity.y >= -1)
				{
					PlayerMovement.rbPlayer.AddForce(dir * (Mathf.Abs(PlayerMovement.rbPlayer.velocity.y) / 1.5f), ForceMode.Impulse);
				}
				if(PlayerMovement.rbPlayer.velocity.y < -1)
				{
					PlayerMovement.rbPlayer.AddForce(dirUp * (Mathf.Abs(PlayerMovement.rbPlayer.velocity.y) / 1.5f), ForceMode.Impulse);
				}

				if(PlayerMovement.rbPlayer.velocity.z >= -1)
				{
					PlayerMovement.rbPlayer.AddForce(Vector3.back * (Mathf.Abs(PlayerMovement.rbPlayer.velocity.z) / 1.5f), ForceMode.Impulse);
				}
				if(PlayerMovement.rbPlayer.velocity.z < -1)
				{
					PlayerMovement.rbPlayer.AddForce(Vector3.forward * (Mathf.Abs(PlayerMovement.rbPlayer.velocity.z) / 1.5f), ForceMode.Impulse);
				}

				if(PlayerMovement.rbPlayer.velocity.x >= -1)
				{
					PlayerMovement.rbPlayer.AddForce(Vector3.left * (Mathf.Abs(PlayerMovement.rbPlayer.velocity.x) / 1.5f), ForceMode.Impulse);
				}
				if(PlayerMovement.rbPlayer.velocity.x < -1)
				{
					PlayerMovement.rbPlayer.AddForce(Vector3.right * (Mathf.Abs(PlayerMovement.rbPlayer.velocity.x) / 1.5f), ForceMode.Impulse);
				}
			}

			break;

		case "smashed":

			//playerMesh.transform.localScale = new Vector3(1,.01,1);
			break;
		
		case "hammer":

			PlayerMovement.rbPlayer.isKinematic = false;
			PlayerMovement.rbPlayer.useGravity = true;
			if(deathOneShot)
			{
				//PlayerMovement.rbPlayer.AddForceAtPosition(Vector3.forward * 2, colPosition, ForceMode.Impulse);

				deathOneShot = false;
			}
			break;

		case "pusher":

			PlayerMovement.rbPlayer.isKinematic = false;
			PlayerMovement.rbPlayer.useGravity = true;
			if(deathOneShot)
			{
				PlayerMovement.rbPlayer.AddForce(Vector3.back * 2, ForceMode.Impulse);
				deathOneShot = false;
			}
			break;

		case "disintegrated":

			playerMesh.GetComponent<MeshRenderer>().enabled = false;
			player.GetComponent<BoxCollider>().enabled = false;

			if(disOneShot)
			{
				disintegratePart.Emit(20);
				disintegratePart2.Emit(500);
				Sounds.GC.playSound("sizzle");
				disOneShot = false;

			}
			break;

		case "melted":

			break;

		case "fallen":
			
			camera_follow.follow = false;
			if(PlayerMovement.rbPlayer.isKinematic)
			{
				player.transform.Translate(dir * 6 * Time.deltaTime);
			}

			break;

		default:

			break;
		}
	}

	//-------------------------------------------------------------------------------- 

	void ChangeTexture()
	{
		PlayerSettings.playerMaterial.SetTexture("_MainTex", chosenTexture);
	}

	//-------------------------------------------------------------------------------- 

	/*void UV()
	{
		Vector2 UVscale = new Vector2(UVscalex, UVscaley);
		playerMaterial.SetVector("Tiling", UVscale);
		//playerMaterial.mainTextureScale = UVscale;
	}*/
		
}
