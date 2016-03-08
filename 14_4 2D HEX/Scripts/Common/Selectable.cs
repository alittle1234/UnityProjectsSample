using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

	public bool isSelectable = false;
	public bool isSelected = false;

	void SelectMe () {

		if(isSelected)
		{
			DeSelectMe();
		}else{
			//Debug.Log("I am Selected!", this);

			isSelected = true;
			// alter state for selection
			showSelected();
		}
	}

	void showSelected () {
		gameObject.transform.localScale = new Vector3(2,2,2);
	}

	void showUnSelected () {
		gameObject.transform.localScale = new Vector3(1,1,1);
	}
	
	void DeSelectMe () {
		//Debug.Log("I Was Deselected!", this);

		
		// reverse state for un-selection
		if(isSelected)
			showUnSelected(); // add TRY - CATCH incase failure. still unselect?

		isSelected = false;
	}
}
