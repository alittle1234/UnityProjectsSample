using UnityEngine;
using System.Collections;

public class s_Gun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public float reloadSpeed = 30f;
	public float lastShot = 0f;
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("f")) 
		{
			shoot();
		}

		if(lastShot != 0){
			lastShot--;
			if(lastShot<= 0){
				lastShot = 0;
			}
		}
	}
	public GameObject projectile;
	void shoot ()
	{
		if(lastShot<= 0f){
			GameObject OBJ = (GameObject) Instantiate(projectile, transform.position, transform.rotation);
			lastShot = reloadSpeed;
		}
	}
}
