﻿using UnityEngine;
using System.Collections;

public class HadoukenControl : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Destroy(gameObject,10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Enemy"){
			anim.SetTrigger("Collide");
			rigidbody2D.velocity = Vector2.zero;
			PersonaController.instance.Score += 25;
			Destroy (gameObject,0.5f);
			other.SendMessage("Die");
		}
	}
}
