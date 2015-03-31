using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	Animator anim;
	int HP = 3;
	Material mat;
	void Start () {
		anim = GetComponent<Animator>();
		mat = renderer.material;
	}

	void FixedUpdate(){
		if(rigidbody2D)
			rigidbody2D.AddForce(new Vector2(-3.5f, 0),ForceMode2D.Force);
	}

	void Damage(){
		HP--;
		if(HP <= 0){
			Die ();
		}
	}

	void Die(){
		anim.SetTrigger("Die");
		this.gameObject.layer = 2;
		mat.color = new Color(1f,1f,1f,0.8f);
//		foreach (Collider2D item in a) {
//			item.enabled = false;
//		}
		if(rigidbody2D){
			CircleCollider2D cc = GetComponent<CircleCollider2D>();
			cc.enabled = false;
			Destroy (rigidbody2D);
		}
		Destroy (gameObject,2.0f);
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			//Debug.Log ("bullie collide player");
			//Destroy (this.gameObject);

		}
		if(other.gameObject.tag == "Bullet"){
			//Debug.Log ("bullie collide bullet");
			anim.SetTrigger("Die");
			collider.enabled = false;
			Destroy(gameObject,0.5f);
		}
	}
}
