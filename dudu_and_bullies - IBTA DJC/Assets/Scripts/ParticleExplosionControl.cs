using UnityEngine;
using System.Collections;

public class ParticleExplosionControl : MonoBehaviour {

	public string sortingLayer;

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = sortingLayer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
