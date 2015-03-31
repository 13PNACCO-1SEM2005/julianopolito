using UnityEngine;
using System.Collections;

public class ParticleDustController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!PersonaController.instance.IsGrounded){
			particleSystem.enableEmission = false;
		}else{
			particleSystem.enableEmission = true;
		}
	}
}
