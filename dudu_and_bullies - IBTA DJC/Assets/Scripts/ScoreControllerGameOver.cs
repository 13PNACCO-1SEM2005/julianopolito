using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreControllerGameOver : MonoBehaviour {

	int Score = 0;

	// Use this for initialization
	void Start () {
		Text t = GetComponent<Text>();
		t.text = "Score: " + PlayerPrefs.GetInt("score");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
