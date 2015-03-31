using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform character;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(character && !GameControl.isPaused){
			Vector3 pos = new Vector3(character.transform.position.x+1.8f, transform.position.y,transform.position.z);
			transform.position = pos;	
		}
	}
	void OnGUI(){

	}
}
