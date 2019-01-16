using UnityEngine;
using System.Collections;

public class collisionignore : MonoBehaviour 
{

	public int layer1 = 0;
	public int layer2 = 1;

	void Start () 
	{
	
	}
	

	void Update () 
	{
		Physics.IgnoreLayerCollision(layer1, layer2, true);
	}
}
