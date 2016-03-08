using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private GUIStyle buttonStyle; // property set in Unity UI, on object containing this script
	public GUIStyle activeStyle; // property set in Unity UI, on object containing this script
	public GUIStyle inactiveStyle; // property set in Unity UI, on object containing this script
	GUILayoutOption[] defaultOptions;


	void Start () { // Use this for initialization
		if(defaultOptions == null)
			defaultOptions = new GUILayoutOption[]{};
		guiStart();
	}

	public virtual void guiStart ()	{	}


	void OnGUI () {
		drawThisGui();
	}

	public virtual void drawThisGui ()
	{
		makeWordArea();
	}

	public string buttonText = "";

	public int xPos = 10;
	public int yPos = 10;

	private Vector2 areaDimn;

	public bool alignToCenter = false;
	private Rect butRect =  new Rect();

	protected void makeWordArea(){
		buttonStyle = (isActive()) ? activeStyle : inactiveStyle;

		areaDimn =  buttonStyle.CalcSize(new GUIContent(buttonText));

		butRect.height = areaDimn.y;
		butRect.width = areaDimn.x;
		butRect.x = ( alignToCenter ? xPos - (areaDimn.x/2) : xPos );
		butRect.y = yPos;

		GUILayout.BeginArea (butRect);
		GUILayout.BeginHorizontal();

		bool button =  GUILayout.Button(buttonText, buttonStyle, defaultOptions);
		
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

		if(button){
			
			setActive( (isActive()) ? false : true );  // maybe should move this up?
			doButtonAction();
		}
	}


	private bool activated = false;
	public virtual void doButtonAction(){
		//activated = (activated) ? false : true;
		print("selcted something: " + buttonText);
	}

	public virtual bool isActive(){
		return activated;
	}

	public virtual void setActive(bool act){
		activated = act;
	}


	// investigate gui buttons

	// make another area
	// building selection two or more
	// hex selection

	// build a building requrires a (hex selection & building selection)
		// place types 1, 2, or 3 with sprite 1,2,3
		// building type is a component? some enum?
		// starting with a component will be easier?
		// add component at build time?
		// a prefab can have a sprite object. do not go down rabit hole of instanceing sprites, pulling from resources, xml etcetera

	// building a resource costs concrete, wood, and money
	// building costs time, will only be built on next two turns.

	
}
