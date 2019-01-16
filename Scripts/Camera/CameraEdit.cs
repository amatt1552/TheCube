using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class CameraEdit : MonoBehaviour 
{
	public bool showBounds;
	SphereCollider col;
	public Vector3 newPosition;
	public int distance;

	void Start()
	{
		if(gameObject.GetComponent<SphereCollider>() != null)
		{
			col = gameObject.GetComponent<SphereCollider>();
		}
		else
		{
			gameObject.AddComponent<SphereCollider>();
			col = gameObject.GetComponent<SphereCollider>();
		}
	}

	void Update()
	{
		if(Application.isPlaying)
		{
			col.enabled = false;
		}
		else
		{
			
			draw(Vector3.zero,distance,showBounds);
		}

	}
	public void draw(Vector3 pos, int dist, bool show)
	{
		if(show)
		{
			col.enabled = true;
			col.isTrigger = true;
			col.center = pos;
			col.radius = dist;
		}
		else
		{
			col.enabled = false;
		}
		
	}
}


