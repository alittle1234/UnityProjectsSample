using UnityEngine;
using System.Collections;

public class s_Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

	bool spawnCircle = true;
	// Update is called once per frame
	void Update () {
		if(spawnCircle){
			makeCircles();
		}
	}

	void makeCircles ()
	{
		spawnCircle = false;

		while(ringLevel<4){
			spawnNextCircle();
		}
	}
	public GameObject circleObj;
	float nextX = 10;
	float nextZ = 10;
	float ringLevel = 1;
	float rotationLevel = 0;
	void spawnNextCircle ()
	{
		float maxRotLvls = ringLevel * 4f;
		float angle = (rotationLevel/  maxRotLvls)  * 360f;
		Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
		transform.rotation = rot;
		Vector3 nextPos = Vector3.zero + transform.forward * ringLevel * 50;
		nextX = nextPos.x;
		nextZ = nextPos.z;
		rotationLevel++;

		Instantiate(circleObj, new Vector3(nextX, 0, nextZ), new Quaternion(0,0,0,0) );

		if(maxRotLvls == rotationLevel){
			rotationLevel = 0;
			ringLevel++;
		}
	}
}
