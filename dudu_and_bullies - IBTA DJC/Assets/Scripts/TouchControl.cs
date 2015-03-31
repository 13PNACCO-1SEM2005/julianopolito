using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {

	Vector2 fp;
	Vector2 lp;
	Direction current;
	float moveThreshold = 50f;
	public static TouchControl instance;

	public enum Direction{
		NONE,
		TOUCH,
		SWIPE_UP,
		SWIPE_DOWN,
		SWIPE_LEFT,
		SWIPE_RIGHT,
		DOUBLE_TAP,
		TRIPLE_TAP
	}

	// Use this for initialization
	void Awake () {

		current = Direction.NONE;
		if(!instance){
			instance = this;
		}else{
			Destroy (this.gameObject);
		}
	}

	public Direction GetCurrent(){
		return current;
	}

	// Update is called once per frame
	void Update () {
		Direction swipe = Direction.NONE;

		foreach (Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began)
			{
				//Debug.Log ("Touch Began");
				fp = touch.position;
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Moved)
			{
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended)
			{

				float deltaX = Mathf.Abs (lp.x-fp.x);
				float deltaY = Mathf.Abs (lp.y-fp.y);
				//Verifica se o movimento e mais lateral ou vertical
				//pois o controle e de apenas um movimento por vez, nao existe diagonal
				if(deltaX > deltaY){
					if((fp.x - lp.x) > moveThreshold)
					{
						//Left Swipe
						//Debug.Log ("Swiped Left!");
						swipe = Direction.SWIPE_LEFT;
					}else if((lp.x - fp.x) > moveThreshold)
					{
						//Right Swipe
						//Debug.Log ("Swiped Right!");
						swipe = Direction.SWIPE_RIGHT;
					}
				}else{
					if((fp.y - lp.y) > moveThreshold)
					{
						//Down Swipe
						//Debug.Log ("Swiped Down!");
						swipe = Direction.SWIPE_DOWN;
					}else if((lp.y - fp.y) > moveThreshold)
					{
						//Up Swipe
						//Debug.Log ("Swiped Up!");
						swipe = Direction.SWIPE_UP;
					}
				}

				//Caso o movimento nao atinja o threshold, considera um toque
				if(deltaX < moveThreshold && deltaY < moveThreshold ){
					//Debug.Log ("Touch!");
					swipe = Direction.TOUCH;
					if(touch.tapCount == 2){
						//Debug.Log ("Double tap!");
						swipe = Direction.DOUBLE_TAP;
					}else if (touch.tapCount >= 3){
						//Debug.Log ("Triple tap!");
						swipe = Direction.TRIPLE_TAP;
					}
				}
			}
			break;
		}
		current = swipe;
	}
}
