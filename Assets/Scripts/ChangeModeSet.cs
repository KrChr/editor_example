using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModeSet : MonoBehaviour {

	private GameObject changeMode;


	void Awake()
	{
		changeMode = GameObject.Find ("EditorManager");
	}


	// Use this for initialization
	void Start () 
	{
		changeMode.GetComponent<MainScript> ().ChangeMode(MainScript.Mode.Play);
	}
}
