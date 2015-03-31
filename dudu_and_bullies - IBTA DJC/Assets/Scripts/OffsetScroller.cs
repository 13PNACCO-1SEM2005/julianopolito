using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	public float scrollSpeed = 2.3f;
	public PersonaController persona;

	// Use this for initialization
	void Start () {
		persona = PersonaController.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameControl.isPaused){
			//Debug.Log("Persona velocity: "+persona.rigidbody2D.velocity.x);
			float x = Mathf.Repeat(Time.time*scrollSpeed, 1);
			Vector2 offset = new Vector2(x,0);
			renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
		}
	}
}
