using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FloorGenerator : MonoBehaviour {

	GameObject debug;
	public GameObject floor;
	public Camera cam;
	List<GameObject> floors;

	GameObject lastBlock;


	// Use this for initialization
	void Awake () {
		int poolSize = 10;
		floors = new List<GameObject>(poolSize);
		Vector2 initPosFloor = new Vector2(-6.5f,-1.7f);
		GameObject f = null;
		for (int i = 0; i < poolSize; i++) {
			f = Instantiate(floor) as GameObject;
			f.transform.position = new Vector2(-initPosFloor.x*(i),initPosFloor.y);
			f.SetActive(true);
			floors.Add(f);
			lastBlock = f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		GameObject p = getFromPool();
		if(p){
			//Debug.Log("found p");
			p.transform.position = new Vector2(lastBlock.transform.position.x+lastBlock.collider2D.bounds.size.x,lastBlock.transform.position.y);
			lastBlock = p;
		}
	}

	GameObject getFromPool(){
		foreach (GameObject item in floors) {
			if(!item.activeInHierarchy){
				item.SetActive(true);
				return item;
			}
		}
		return null;
	}


}


