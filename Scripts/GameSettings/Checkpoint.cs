using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	public MeshRenderer[] mat;
	GameObject spawn;
	public bool active;
	void Update () 
	{
		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");
		spawn = GameObject.Find("spawn");
			
		if(Mathf.Abs(PlayerSettings.player.transform.position.x - transform.position.x) < 1 && Mathf.Abs(PlayerSettings.player.transform.position.y - transform.position.y) < 1 && Mathf.Abs(PlayerSettings.player.transform.position.z - transform.position.z) < 1)
		{
			spawn.transform.position = transform.position;
		}

		if(spawn.transform.position == transform.position)
		{
			for(int i = 0; i < mat.Length;i++)
			{
				mat[i].material.color = new Color(0, 1, 0, 0.4f);
			}

			active = true;
		}
		else		{
			for(int i = 0; i < mat.Length;i++)
			{
				mat[i].material.color = new Color(1, 0, 0, 0.4f);
			}

			active = false;
		}

	}
}
