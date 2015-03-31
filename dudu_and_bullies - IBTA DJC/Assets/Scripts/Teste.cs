using UnityEngine;
using System.Collections;

public class Teste : MonoBehaviour {

	public Vector2 speedVector = Vector2.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rigidbody2D.velocity = speedVector;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Item"){
			Debug.Log ("Enter trigger");
		}
	}

	
	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Item"){
			Debug.Log ("Stay trigger");
		}
	}

	
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Item"){
			Debug.Log ("Exit trigger");
		}
	}
}
