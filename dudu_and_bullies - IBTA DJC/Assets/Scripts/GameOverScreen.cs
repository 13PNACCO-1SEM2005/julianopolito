using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

	public GUISkin skin;

	void Start(){
		skin = Resources.Load("GuiSkin") as GUISkin;
	}

	void OnGUI(){
		GUI.skin = skin;
		if(GUI.Button(
			new Rect(0,0,Screen.width,Screen.height),
			""
			)){
			Application.LoadLevel(0);
		}
	}
}
