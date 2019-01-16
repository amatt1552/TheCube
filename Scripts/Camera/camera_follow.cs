using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour 
{
	public Vector3 cameraPosition;
	public Vector3 cameraRotationIdle;
	public Vector3 cameraRotationLeft;
	public Vector3 cameraRotationRight;
	public Vector3 cameraRotationFalling;
	public float rotateSpeed;
	public float movementSpeed;

	float zoom;

	float realRotateSpeed;
	float x,y,z;
	GameObject player;
	public static bool follow;
	GameObject[] editPoints;
	GameObject currentEditPoint;
	bool oneShotLeft, oneShotRight;

	void Awake ()
	{
		editPoints = GameObject.FindGameObjectsWithTag("cameraEdit");
		if(editPoints.Length > 0)
			currentEditPoint = editPoints[0];
		
		player = GameObject.Find ("player");
		follow = true;
		zoom = 0;
	}
	

	void FixedUpdate () 
	{
		
		Vector3 cameraPos =  new Vector3(player.transform.position.x + cameraPosition.x, player.transform.position.y + cameraPosition.y, player.transform.position.z + cameraPosition.z + zoom);
		transform.eulerAngles = new Vector3(x,y,z);
		realRotateSpeed = 2 * Mathf.Exp(2);

		if(follow)
		{
			transform.position = Vector3.Lerp(transform.position, cameraPos, movementSpeed * Time.deltaTime);
		}

		if(PlayerSettings.dead || !PlayerMovement.active)
		{
			return;
		}
		else if(PlayerMovement.movingLeft)
		{

			if(y > cameraRotationLeft.y)
			{
				y -= realRotateSpeed * Time.deltaTime;
			}
			else
			{
				y = cameraRotationLeft.y;
			}

		}
		else if(PlayerMovement.movingRight)
		{

			if(y < cameraRotationRight.y)
			{
				y += realRotateSpeed * Time.deltaTime;
			}
			else
			{
				y = cameraRotationRight.y;
			}

		}
		else
		{
			if(Mathf.Abs(cameraRotationIdle.y - y) < 0.1f)
			{
				y = cameraRotationIdle.y;
			}
			else if(y > cameraRotationIdle.y)
			{
				y -= realRotateSpeed * Time.deltaTime;
			}
			else if (y < cameraRotationIdle.y)
			{
				y += realRotateSpeed * Time.deltaTime;
			}

		}

		if(!Collisions.ground)
		{
			if(x < cameraRotationFalling.x)
			{
				x += realRotateSpeed * Time.deltaTime;
			}
			else
			{
				x = cameraRotationFalling.x;
			}
		}
		else
		{
			if(Mathf.Abs(cameraRotationIdle.x - x) < 0.1f)
			{
				x = cameraRotationIdle.x;
			}
			else if(x > cameraRotationIdle.x)
			{
				x -= realRotateSpeed * Time.deltaTime;
			}
			else if (x < cameraRotationIdle.x)
			{
				x += realRotateSpeed * Time.deltaTime;
			}
		}

		if(editPoints.Length > 0)
			ChangeSettings();
	}
	void ChangeSettings()
	{
		for(int i = 0; i < editPoints.Length;)
		{
			if(Vector3.Distance(currentEditPoint.transform.position, player.transform.position) > Vector3.Distance(editPoints[i].transform.position, player.transform.position))
			{
				currentEditPoint = editPoints[i];
			}
			/*if(Vector3.Distance(editPoints[i].transform.position, player.transform.position) < 20)
			{
				currentEditPoint = editPoints[i];
			}
			*/
			i++;
		}

		if(currentEditPoint != null)
		{
			if(Vector3.Distance(currentEditPoint.transform.position, player.transform.position) <= currentEditPoint.GetComponent<CameraEdit>().distance)
			{
				zoom = currentEditPoint.GetComponent<CameraEdit>().newPosition.z;
			}
			else
			{
				zoom = 0;
			}
		}

	}
}
