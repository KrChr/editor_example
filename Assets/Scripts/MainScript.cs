using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	public GameObject cubePrefab;

	private GameObject cubeAnim;
	private List<GameObject> mylist = new List<GameObject>();
	private int myListPlayID = 0;

	public enum Mode
	{
		Editor,
		Play
	}

	public Mode CurrentMode = Mode.Editor;



	void Awake()
	{
		DontDestroyOnLoad (this);
	}


	void Update() 
	{
		if (CurrentMode == Mode.Editor) 
		{
			EditorOn ();
		}

		if (CurrentMode == Mode.Play) 
		{
			PlayOn ();
		}
	}


	public void ChangeMode(Mode myChoosenMode)
	{
		CurrentMode = myChoosenMode;
	}


	void PlayOn()
	{
		//Debug.Log ("PlayOn");

		if (cubeAnim == null && mylist.Count > 1) 
		{
			Debug.Log ("PlayOn(), mylist.Count = " + mylist.Count);
			cubeAnim = Instantiate (cubePrefab, mylist [0].transform.position, Quaternion.identity) as GameObject; 
		}

		if ((myListPlayID < mylist.Count+1) && (mylist.Count > 1)) 
		{
			cubeAnim.transform.position = Vector3.Lerp (cubeAnim.transform.position, mylist [myListPlayID].transform.position, 10f * Time.deltaTime);

			if (cubeAnim.transform.position == mylist [myListPlayID].transform.position) 
			{
				myListPlayID++;
			}
		} 
		if (myListPlayID == mylist.Count || mylist.Count <= 1)
		{
			Debug.Log ("Reset");
			CurrentMode = Mode.Editor;
			Application.LoadLevel (0);
			Destroy (gameObject);
		}
	}


	void EditorOn()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			Debug.Log ("Click");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast (ray, out hit))
			{
				PointCreate (hit);
				PointDestroy (hit);
			}
		}
	}


	void PointCreate(RaycastHit myHit)
	{
		if (myHit.rigidbody != null && myHit.collider.tag == "editorCanvas") 
		{
			GameObject newEditorPoint = Instantiate(cubePrefab, myHit.point, Quaternion.identity) as GameObject; 
			newEditorPoint.transform.parent = gameObject.transform;
			mylist.Add (newEditorPoint);
		}
	}


	void PointDestroy(RaycastHit myHit)
	{
		if (myHit.transform.tag == "editorPoint")
		{
			foreach (var element in mylist) 
			{
				if (element.transform.position == myHit.collider.gameObject.transform.position) 
				{
//					Debug.Log ("PointDestroy, foreach, if done");

					Destroy (myHit.collider.gameObject);
					mylist.Remove (element);
					return;
				}
			}
		}
	}


	public void PlayScene()
	{
		Debug.Log("You have clicked the button Start");
		Application.LoadLevel ("playScene");
	}


}