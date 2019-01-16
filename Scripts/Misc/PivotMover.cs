using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PivotMover : MonoBehaviour {
	GameObject pivot;
	GameObject moving;
	public bool pivotChange;
	bool isMoving;

	float distX, distY, distZ;
	Vector3 pos;
	/*
	void Start()
	{
		createPivot = true;
	}
	void Update()
	{
		if(createPivot)
		{
			pivot = new GameObject("pivot");
			pivot.transform.position = transform.position;
			transform.parent = pivot.transform;
			createPivot = false;

		}
		if(Input.GetKeyDown (KeyCode.D))
		{
			pivotChange = !pivotChange;
		}
		if(pivotChange)
		{
			MovePivot();
		}
	}
	void MovePivot()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			pivot.transform.Translate(Vector3.left * 0.5f);
			transform.Translate(Vector3.right * 0.5f);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			pivot.transform.Translate(Vector3.right * 0.5f);
			transform.Translate(Vector3.left * 0.5f);
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			pivot.transform.Translate(Vector3.up * 0.5f);
			transform.Translate(Vector3.down * 0.5f);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			pivot.transform.Translate(Vector3.down * 0.5f);
			transform.Translate(Vector3.up * 0.5f);
		}

	}
	*/
	void Start()
	{
		pivot = new GameObject("pivot");
		pivot.transform.position = transform.position;
		transform.parent = pivot.transform;

	}
	void Update()
	{
		if (pivotChange)
		{
			if(isMoving)
			{
				moving = new GameObject("Moving");
				moving.transform.position = transform.position;
				isMoving = false;
			}
			transform.position = moving.transform.position;

		}
		else
		{
			try
			{
			DestroyImmediate(moving);
			}
			catch{}
			isMoving = true;
		}
	}
}
