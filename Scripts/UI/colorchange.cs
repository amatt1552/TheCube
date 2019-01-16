using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class colorchange : MonoBehaviour {
	public Text buttontext;

	void Start () 
	{
		buttontext = gameObject.GetComponent<Text> ();
	}
	

	void Update () 
	{
	
	}
	void OnMouseEnter()
	{
		buttontext.color = Color.white;
		Debug.Log ("white");
	}
	void OnMouseExit()
	{
		buttontext.color = Color.black;
		Debug.Log ("black");
	}
}
