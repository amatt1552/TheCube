using UnityEngine;
using System.Collections;

public class OceanSettings : MonoBehaviour 
{
	Material ocean;
	public float oceanSpeedX = 1;
	public float oceanSpeedY = 0.5f;
	float x;
	float y;
	Object oceanSplash;
	Object initialSplash;
	//Vector3 instPoint;
	GameObject impactingObject;
	bool impacted;

	void Start ()
	{
		//impactingObject = PlayerSettings.player;

		x = Random.Range(0,5);
		y = Random.Range(0,5);
		ocean = this.GetComponent<MeshRenderer>().material;
		oceanSplash = Resources.Load("Prefabs/OceanSplashGO");
		initialSplash = Resources.Load("Prefabs/splashAnim");

	}
	

	void Update () 
	{
		
		x += oceanSpeedX * Time.deltaTime;
		y += oceanSpeedY * Time.deltaTime;
		ocean.mainTextureOffset = new Vector2(x, y);
        ocean.SetTextureOffset("_DispTex", new Vector2(x * 2, y * 2));

    }

	void OnTriggerEnter(Collider other)
	{
		
		impactingObject = other.gameObject;

		StartCoroutine("Splash");
	}

	IEnumerator Splash()
	{
		GameObject[] oceanSplashes;
		oceanSplashes = new GameObject[4];
		Sounds.GC.playSound("splash");
		Vector3 instPoint = new Vector3(impactingObject.transform.position.x, transform.position.y, impactingObject.transform.position.z);


		oceanSplashes[0] = (GameObject)Instantiate(initialSplash, instPoint, Quaternion.Euler( new Vector3(0,0,0)));

		yield return new WaitForSeconds(0.5f);

		oceanSplashes[0].SetActive(false);
		oceanSplashes[1] = (GameObject)Instantiate(oceanSplash, instPoint, Quaternion.Euler( new Vector3(270,0,0)));

		yield return new WaitForSeconds(0.5f);

		oceanSplashes[2] = (GameObject)Instantiate(oceanSplash, instPoint, Quaternion.Euler( new Vector3(270,0,0)));

		yield return new WaitForSeconds(0.5f);

		oceanSplashes[3] = (GameObject)Instantiate(oceanSplash, instPoint, Quaternion.Euler( new Vector3(270,0,0)));

		yield return new WaitForSeconds(1f);

		for(int i = 0; i < oceanSplashes.Length;i++)
		{
			Destroy(oceanSplashes[i]);
		}

	}
}
