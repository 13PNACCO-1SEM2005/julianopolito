using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public GameObject Player;
	public GameObject Enemy;

	// Use this for initialization
	void Start () {
		Invoke("Spawn",Random.Range(1f,2f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn(){
		GameObject b = (GameObject) Instantiate (Enemy, new Vector3(transform.position.x,transform.position.y+1,0), Quaternion.identity);
		b.SetActive(true);
		b.rigidbody2D.velocity = new Vector2(-6f, b.rigidbody2D.velocity.y);
		Invoke("Spawn",Random.Range(10f,12f));
	}
}
