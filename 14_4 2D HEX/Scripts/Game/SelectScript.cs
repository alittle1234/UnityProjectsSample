using UnityEngine;
using System.Collections;

public class SelectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkSelectKey();
	}

	void checkSelectKey(){
		if ( Input.GetMouseButtonDown(0))
		{
			//Debug.Log("Mouse Down", this);

			selectWithMouse();
		}
	}

	void selectWithMouse(){
		RaycastHit hitInfo = new RaycastHit();
		bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
		if (hit)
		{
			selectObj(hitInfo);

		} else {
			deselectAll();
		}
	}

	void selectObj(RaycastHit hitInfo){
		//Debug.Log("Hit an Object" + hitInfo.transform.gameObject.name);

		hitInfo.collider.gameObject.SendMessage("SelectMe", null, SendMessageOptions.DontRequireReceiver);
	}

	void deselectAll( ){
		//Debug.Log("No hit, DeSelecting All.");

		Selectable[] selectables = (Selectable[])Resources.FindObjectsOfTypeAll(typeof(Selectable));
		for(var i = 0; i<selectables.Length; i++){
			selectables[i].SendMessage("DeSelectMe", null, SendMessageOptions.DontRequireReceiver);
		}
	}
}
