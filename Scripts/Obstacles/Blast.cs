using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour 
{

	Rigidbody blastrb;
	Transform deathParticlesOrigin;
	ParticleSystem deathPart;
	float timer;

//------------------------------------------------------------------ 
	void Start () 
	{
		deathParticlesOrigin = transform.FindChild("deathParticleOrigin");
		deathPart = deathParticlesOrigin.GetComponent<ParticleSystem>();

		blastrb = GetComponent<Rigidbody> ();
		blastrb.AddRelativeForce (Vector3.forward * 250f);
		Destroy (this.gameObject, 10f);
	}
	
//------------------------------------------------------------------ 
	void OnTriggerEnter (Collider other) 
	{
		
		if(other.tag != "enemy" && other.tag != "emitter")
		{
			try
			{
				deathPart.Emit(10);
			}
			catch{}


			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<Collider>().enabled = false;
			blastrb.isKinematic = true;
			transform.FindChild("inner").GetComponent<MeshRenderer>().enabled = false;

			Destroy (this.gameObject, 1);

		}
	}
}
