using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public GUISkin skin;
	
	void Start(){
		skin = Resources.Load("GuiSkinStart") as GUISkin;
	}
	
	void OnGUI(){
		GUI.skin = skin;
		if(GUI.Button(
			new Rect(0,0,Screen.width,Screen.height),
			""
			)){
			Debug.Log("Touch");
			Application.LoadLevel(1);
		}
	}
}
