using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {

	public static InputControl instance;

	TouchControl.Direction current = TouchControl.Direction.NONE;

	// Use this for initialization
	void Start () {
		instance = this;
	}

	public TouchControl.Direction GetCurrent(){
		#if UNITY_EDITOR
		return KeyboardControl.instance.GetCurrent();
		#endif
		#if UNITY_ANDROID
		return TouchControl.instance.GetCurrent();
		#endif
	}
}
