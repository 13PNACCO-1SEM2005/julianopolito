using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHUD : MonoBehaviour {

	public PersonaController persona;

	public GameObject[] hearts;
	public GameObject[] hadouken;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		int index = 0;
		Material m;
		for (int j = 0; j < hearts.Length; j++) {
			m = hearts[j].renderer.material;
			if(j < persona.HP){
				//hearts[j].SetActive(true);
				m.color = Color.white;
			}else{
				//hearts[j].SetActive(false);
				m.color = new Color(1f,1f,1f,0.3f);
			}
		}
		for (int j = 0; j < hadouken.Length; j++) {
			m = hadouken[j].renderer.material;
			if(j < persona.HadoukenCounter){
				//hadouken[j].SetActive(true);
				m.color = Color.white;
			}else{
				//hadouken[j].SetActive(false);
				m.color = new Color(1f,1f,1f,0.3f);
			}
		} 
	}
}
