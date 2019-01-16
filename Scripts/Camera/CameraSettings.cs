using UnityEngine;
using System.Collections;

public class CameraSettings : MonoBehaviour 
{
	public Camera camera;
	public float stretch;
	void Start () 
	{
		
	}

	void Update () 
	{
		camera.aspect = (Screen.currentResolution.width ) /(Screen.currentResolution.height * stretch);
	}
}
