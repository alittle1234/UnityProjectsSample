using UnityEngine;
using System.Collections;

public class BaseComponent : MonoBehaviour {


	//public GameObject thisObject; // public property, must be set by Unity.  Is this neccessary anyMore?

	protected Transform thisTransform;
	protected Selectable selectable;

	void Start () {
		//thisObject = gameObject;
		//thisTransform = thisObject.transform;

		selectable = GetComponent<Selectable>();
		//Debug.Log("Base Component Start", this);

		myStart ();
	}
	public void setTransform(Transform t){
		thisTransform = t;
	}

	protected virtual  void myStart () { }

	static Camera mainCamera;
	static Camera getMainCamera(){
		if(mainCamera== null){
			mainCamera = (Camera.main);
		}
		return mainCamera;
	}
	static public Vector3 getScreenPosition(Vector3 worldPosition){
		return getMainCamera().WorldToScreenPoint(worldPosition);
	}

	static public Vector3 getTransScreenPos(Vector3 worldPosition){
		Vector3 pos = getScreenPosition(worldPosition);
		pos.y = (int)getMainCamera().pixelHeight - (int)pos.y;
		return pos;
	}
}
