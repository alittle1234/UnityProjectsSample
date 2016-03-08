using UnityEngine;
using System.Collections;

public class s_ExtraButton : s_GUIButtonScript {

	public MyButton button;

	public override void drawThisGui() {
		makeWordArea(button);
	}

	public override void doButtonAction() {
		print("CLICKED Extra BUTTON: " + button.text);
		
		if(button.invokerObj != null) {
			button.invokerObj.SendMessage(button.methodCall, button.text);
		} else {
			print("Invoker is null: " + button.invokerObj);
		}
	}
}
