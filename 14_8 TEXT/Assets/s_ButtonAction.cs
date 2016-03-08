using UnityEngine;
using System.Collections;

public class s_ButtonAction : MonoBehaviour {

	// Use this for initialization
	//void Start () { }
	
	// Update is called once per frame
	//void Update () { }

	public void doAnAction(object param){
		Debug.Log("button Action performed." + param );
		setUpScene(param);
	}

	void setUpScene (object param) {
		s_Scene.output = "You did an action";
		s_Scene.options = getOptionButtons();

//		s_Scene.mapPane = getMapPane();
//		s_Scene.infoPane = getInfoPane();

		s_Scene.refreshUI();
	}
//	public string id;
//	public string text; 
//	public int x; 
//	public int y;
//	public int value;
//	
//	public string methodCall;
//	public GameObject invokerObj;
	MyButton[] getOptionButtons () {
		MyButton[] myButtons = new MyButton[3];
		myButtons[0] = new MyButton();
		myButtons[0].id = "someId1";
		myButtons[0].text = "New Button 1";
		myButtons[0].x = 100;
		myButtons[0].y = 240;
		myButtons[0].value = 1;
		myButtons[0].methodCall = "doAnAction";
		myButtons[0].invokerObj = null;

		myButtons[1] = new MyButton();
		myButtons[1].id = "someId2";
		myButtons[1].text = "NB 2";
		myButtons[1].x = 100;
		myButtons[1].y = 260;
		myButtons[1].value = 1;
		myButtons[1].methodCall = "doAnAction";
		myButtons[1].invokerObj = null;

		myButtons[2] = new MyButton();
		myButtons[2].id = "someId3";
		myButtons[2].text = "NB 3";
		myButtons[2].x = 100;
		myButtons[2].y = 280;
		myButtons[2].value = 1;
		myButtons[2].methodCall = "doAnAction";
		myButtons[2].invokerObj = null;

		return myButtons;
	}

	object getMapPane () {
		throw new System.NotImplementedException ();
	}

	object getInfoPane () {
		throw new System.NotImplementedException ();
	}
}
