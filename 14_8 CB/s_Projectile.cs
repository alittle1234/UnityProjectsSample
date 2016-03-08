using UnityEngine;
using System.Collections;

public class s_Projectile : MonoBehaviour {

	// Use this for initialization
	public float startForce = 500.0f;
	public int maxDist = 500;
	public int maxAliveTime = 5000;
	public float currentAliveTime = 0;
	public Vector3 startPos;
	void Start () {
		startPos = transform.position;
		applyForce(rigidbody.transform.forward, startForce);
	}
	
	// Update is called once per frame
	void Update () {
		currentAliveTime += Time.deltaTime;
		if(currentAliveTime > maxAliveTime){
			killMe();
		}
		if(Vector3.Distance(transform.position, startPos) > maxDist){
			killMe();
		}
	}

	void killMe () {
		//throw new System.NotImplementedException ();
		//Debug.Log("Destroyd Proj");
		GameObject.Destroy(this.gameObject);
	}






	void applyForce (Vector3 direction, float amount)
	{
		Vector3 forceVec = direction * amount;
		
		rigidbody.AddForce(forceVec, ForceMode.Impulse);
	}
}
