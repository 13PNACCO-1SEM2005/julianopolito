using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log("Bateu no :" + other.tag);

		if(other.gameObject.tag == "Player"){
			//Debug.Break();
			return;
		}

		if(other.gameObject.transform.parent){
			//Debug.Log("Destroy parent:" + other.gameObject.tag);
			other.gameObject.transform.parent.gameObject.SetActive(false);
		}else{
			//Debug.Log("Destroy:" + other.gameObject.tag);
			other.gameObject.SetActive(false);
		}

	}
}
