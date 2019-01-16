using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour 
{
	#region variables
	LayerMask groundLayer;
	LayerMask ignoreLayer;
	LayerMask movingObjects;

	public float bias;
	public float inWallBias;
	public float stepHeight;

	float timer1, timer2;

	public static RaycastHit groundInfo, topInfo, leftInfo, rightInfo, movingInfo;
	public static bool ground, top, left, right, onMovingObj;
	public static bool inGround, inTop, inLeft, inRight;
	public static bool falling;


	Vector3 scaleX, scaleY;

	#endregion

	//----------------------------------------------------------------------

	void Awake () 
	{
		groundLayer = (1 << LayerMask.NameToLayer("moving") | 1 << LayerMask.NameToLayer("ground"));
		ignoreLayer = ~(1 << LayerMask.NameToLayer("player"));
		movingObjects = (1 << LayerMask.NameToLayer("moving"));
		scaleX = new Vector3(0, transform.localScale.y - stepHeight, transform.localScale.z/2);
		scaleY = new Vector3(transform.localScale.x, 0, transform.localScale.z/2);
	}

	//----------------------------------------------------------------------

	void FixedUpdate () 
	{
		BoxCasting();
		SpecialCases();

	}

	//----------------------------------------------------------------------

	void BoxCasting()
	{
		Quaternion orientation = Quaternion.identity;

		#region ground

		if(Physics.BoxCast(transform.position, scaleY/2, Vector3.down, out groundInfo, orientation, 1f, groundLayer))
		{
			if(groundInfo.distance <= bias)
			{
				ground = true;
			}
			else
			{
				ground = false;
			}

			if(groundInfo.distance < bias - inWallBias)
			{
				inGround = true;
			}
			else
			{
				inGround = false;
			}
				
		}
		else
		{
			ground = false;
			inGround = false;
		}

		#endregion

		#region top

		if(Physics.BoxCast(transform.position, scaleY/2, Vector3.up, out topInfo, orientation, 1f, ignoreLayer))
		{
			if(topInfo.distance <= bias)
			{
				top = true;
			}
			else
			{
				top = false;
			}

			if(topInfo.distance < bias - inWallBias)
			{
				inTop = true;
			}
			else
			{
				inTop = false;
			}
		}
		else
		{ 
			top = false;
			inTop = false;
		}

		#endregion

		#region left

		if(Physics.BoxCast(transform.position, scaleX/2, Vector3.left, out leftInfo, orientation, 1f, ignoreLayer))
		{
			if(leftInfo.distance <= bias)
			{
				left = true;
			}
			else
			{
				left = false;
			}

			if(leftInfo.distance < bias - inWallBias)
			{
				inLeft = true;
			}
			else
			{
				inLeft = false;
			}

		}
		else
		{
			left = false;
			inLeft = false;
		}
			
		#endregion

		#region right

		if(Physics.BoxCast(transform.position, scaleX/2, Vector3.right, out rightInfo, orientation, 1f, ignoreLayer))
		{
			if(rightInfo.distance <= bias)
			{
				right = true;
			}
			else
			{
				right = false;
			}

			if(rightInfo.distance < bias - inWallBias)
			{
				inRight = true;
			}
			else
			{
				inRight = false;
			}
		}
		else
		{
			right = false;
			inRight = false;
		}


		#endregion

	}

	//----------------------------------------------------------------------

	void SpecialCases()
	{

		#region On moving object check

		try
		{
			
			if(groundInfo.collider.tag == "moving") //&& Mathf.Abs(PlayerSettings.player - groundInfo.collider.transform) < 0.1f)
			{
				
				onMovingObj = true;
			}
		}
		catch{}

		#endregion

		#region fall check

		Quaternion orientation = Quaternion.identity;

		if(ground && !Physics.BoxCast(transform.position, (scaleY - new Vector3(0.4f,0,0))/2, Vector3.down, orientation, 1f))
		{
			timer1 += 1 * Time.deltaTime;
			if(timer1 >= 0.2)
			{
				falling = true;
				timer1 = 0;
			}
		}
		else
		{
				timer1 = 0;
		}
			
		//checks if died while falling

		if(PlayerSettings.dead)
		{
			falling = false;
			timer1 = 0;
			timer2 = 0;
		}

		#endregion
	
	}
		
}
