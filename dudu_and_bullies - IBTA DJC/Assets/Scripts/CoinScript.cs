using UnityEngine;
using System.Collections;

public class CoinScript : GameObjectBase {

	public AudioClip pickupSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player" ){
			gameObject.GetComponent<Animator>().SetTrigger("CoinGet");
			PlaySound(pickupSound);
			Destroy (gameObject,1f);
		}
	}
}
