using UnityEngine;
using System.Collections;

public class KeyboardControl : MonoBehaviour {

	public static KeyboardControl instance;

	TouchControl.Direction current = TouchControl.Direction.NONE;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			current = TouchControl.Direction.TOUCH;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			current = TouchControl.Direction.SWIPE_RIGHT;
		}else if(Input.GetKey(KeyCode.LeftArrow)){
			current = TouchControl.Direction.SWIPE_LEFT;
		}else if(Input.GetKey(KeyCode.UpArrow)){
			current = TouchControl.Direction.SWIPE_UP;
		}else if(Input.GetKey(KeyCode.DownArrow)){
			current = TouchControl.Direction.SWIPE_DOWN;
		}else{
			current = TouchControl.Direction.NONE;
		}
	}

	public TouchControl.Direction GetCurrent(){
		//Debug.Log("K Current: "+current);
		return current;
	}
}
