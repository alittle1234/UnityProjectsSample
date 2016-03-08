using UnityEngine;
using System.Collections;

public class s_GUIScript : MonoBehaviour {

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
	protected Texture texture;
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
		butRect.x = ( alignToCenter ?
		             (xPos - (areaDimn.x/2)) :
		             xPos );
		butRect.y = yPos;

		GUILayout.BeginArea (butRect);
		GUILayout.BeginHorizontal();

		bool clicked = false;
		if(texture != null){
			//Debug.Log("draw texture" + texture.height + " loc: " );
			clicked = GUI.Button(new Rect(10, 10, 50, 50), texture);
			//button = GUILayout.Button(texture, buttonStyle);
		}else{
			//Debug.Log("draw ui" + texture);
			clicked = GUILayout.Button(buttonText, buttonStyle, defaultOptions);
		}
		

		
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

		if(clicked){
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

	
}
