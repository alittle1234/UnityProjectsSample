using UnityEngine;
using System.Collections;

public class s_UIDraw : s_GUIScript {

	// Use this for initialization
	void Start () {
		
	}
	
//	PlayerResourceTypes selectedBuilding = null;
//	public PlayerResourceTypes Selected {
//		get{
//			return selectedBuilding;
//		}
//		set{
//			selectedBuilding = value;
//		}
//	}
	public MyButton[] myButtons;
	public override void drawThisGui ()
	{
		foreach (MyButton button in myButtons){
			currentButton = button;
			setGuiValues(button);
			makeWordArea();
		}
	}
	
	void setGuiValues (MyButton button)
	{
		this.buttonTet = button.text;
		this.texture = button.texture;
		this.xPos = button.x;
		this.yPos = button.y;
	}
	
	public override void doButtonAction(){
		
		print("CLICKED CUSTOM BUTTON: " + buttonText);
	}
	
	private int activeValue = 0;
	private MyButton currentButton;
	
	public override bool isActive(){
		return currentButton.value == activeValue;
	}
	
	public override void setActive(bool act){
		activeValue = ( !act ) ? -1 : currentButton.value;
//		if(activeValue != -1){
//			Selected = currentButton.getResource();
//		}else{
//			Selected = null;
//		}
		
	}
	

}

[System.Serializable]
public class MyButton {
	public string id;
	public string text; 
	public int x; 
	public int y;
	public int value;
	public Texture texture;
}
