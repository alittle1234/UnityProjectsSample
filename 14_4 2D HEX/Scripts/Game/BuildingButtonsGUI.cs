using UnityEngine;
using System.Collections;

public class BuildingButtonsGUI : GUIScript {

	// Use this for initialization
	void Start () {
	
	}

	PlayerResourceTypes selectedBuilding = null;
	public PlayerResourceTypes Selected {
		get{
			return selectedBuilding;
		}
		set{
			selectedBuilding = value;
		}
	}

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
		this.buttonText = button.text;
		
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
		if(activeValue != -1){
			Selected = currentButton.getResource();
		}else{
			Selected = null;
		}

	}
}

[System.Serializable]
public class MyButton {
	public string text; 
//	{
//		get;
//		set;
//	}
	
	public int x; 
//	{
//		get;
//		set;
//	}

	public int y;
//	{
//		get;
//		set;
//	}
	public int value;

	public int resValue;
	public PlayerResourceTypes getResource(){
		switch(resValue){
		case 1:
			return PlayerResourceTypes.WOOD;
		case 2:
			return PlayerResourceTypes.BRICK;
		case 3:
			return PlayerResourceTypes.STEEL;
		case 4:
			return PlayerResourceTypes.MONEY;
		case 5:
			return PlayerResourceTypes.CONCRETE;
		default:
			return PlayerResourceTypes.BRICK;
		}
	}
}
