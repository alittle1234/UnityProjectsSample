using UnityEngine;
using System.Collections;
using AssemblyCSharp.Level;

public class s_UIDraw : s_GUIScript {
	void Start() { 
		print("Started UI.");
		if(s_Scene.uiControler != null) {
			throw new UnityException("UI controller already created.");
		}
		s_Scene.uiControler = this;
		oldButtons = myButtons;
	}

	/// 
	/// Restores the old buttons.
	/// 
	public void restoreOld() {
		myButtons = oldButtons;
		Debug.ClearDeveloperConsole(); 
		UsefulShortcuts.consoleClear();
	}

	private MyButton[] oldButtons;
	public MyButton[] myButtons;
	public override void drawThisGui() {
		foreach(MyButton button in myButtons) {
			currentButton = button;
			setGuiValues(button);
			makeWordArea();
		}
	}
	
	void setGuiValues(MyButton button) {
		//print("uIDraw: button: " + button);

		this.buttonText = button.text;
		this.texture = button.texture;
		this.xPos = button.x;
		this.yPos = button.y;
	}
	
	public override void doButtonAction() {
		Debug.Log("CLICKED CUSTOM BUTTON: " + buttonText);

		if(currentButton.invokerObj != null) {
			currentButton.invokerObj.SendMessage(currentButton.methodCall, currentButton.text);
		} else {
			Debug.Log("Invoker is null: " + currentButton.invokerObj);
		}

        if(currentButton.script != null) {
            ScriptHandler.process(currentButton.script);
        } 
	}
	
	//private int activeValue = 0;
	private MyButton currentButton;
	
	public override bool isActive() {
		//activeValue = 1;
		return true; //currentButton.value == activeValue;
	}
	
	public override void setActive(bool act) {
		//activeValue = ( !act ) ? -1 : currentButton.value;
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

	public int h;
	public int w;

	public int value;
	
	public string methodCall;
	public GameObject invokerObj;

	public string activityVarName;
    public Script script;

	public Texture texture;

	public override string ToString() {
		return id + text;
	}
}
