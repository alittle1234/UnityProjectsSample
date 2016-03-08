using UnityEngine;
using System.Collections;

public class s_ParticleEmitor : MonoBehaviour {

	public GameObject emitterObj;
	ParticleEmitter pe;
	// Use this for initialization
	void Start () {
	
		pe = emitterObj.particleEmitter;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void start(){
		pe.emit = true;
	}
	public void stop(){
		pe.emit = false;
	}
}
