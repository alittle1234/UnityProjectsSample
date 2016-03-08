using UnityEngine;
using System.Collections;

public class Test1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("started.");
	}
	
	// Update is called once per frame
	void Update () {
        bool k = Input.GetKey("k");
        if(k) { 
            Debug.Log("Pressed K.");
        }
        if (Event.current != null) {

            Debug.Log("Event." + Event.current);
        }
        if (Event.current != null && Event.current.keyCode == KeyCode.K) {
            Debug.Log("Pressed K.");
            Debug.Log("Loading From Objects.");
        }
	}
}
