using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class GuiDrawButtons : MonoBehaviour {
    
    public List<GuiButton> myButtons = new List<GuiButton>();
    
    protected Vector3 masterRef = new Vector3(0,0,1);
    private GuiButton masterBut = new GuiButton(0, 0, 400, 600, "Background", new Vector3(0,0,1));
    void Start() {
        myButtons.Add(new GuiButton(5, 40, 40, 100, "Button 1.", masterRef));
        myButtons.Add(new GuiButton(5, 70, 40, 100, "Button 2.", masterRef));
        myButtons.Add(new GuiButton(5, 100, 40, 100, "Button 3.", masterRef));
        myButtons.Add(new GuiButton(5, 140, 40, 100, "Button 4.", masterRef));
        myButtons.Add(new GuiButton(5, 180, 40, 100, "Button 5.", masterRef));
        myButtons.Add(new GuiButton(5, 220, 40, 100, "Button 6.", masterRef));
    }
    
    void OnGUI() {
        drawThisGui();
    }

    private float mouseWheelScale = -0.01f; // scale the mouse wheel input // +/- for direction
    public void drawThisGui() {
//        drawLines();
        // check if mouse up event has happend
        bool mouseUp = false;
        
        // zoom out / in
        if(Event.current.type == EventType.ScrollWheel 
           && masterBut.butRect.Contains(Event.current.mousePosition)) {
            masterRef.z += (Event.current.delta.y * mouseWheelScale);
            Event.current.Use();
        }else if(Event.current.type == EventType.MouseUp){
            mouseUp = true;
            Event.current.Use();
        }

        // move all buttons with background
        masterRef.x += masterBut.xPos;
        masterRef.y += masterBut.yPos;
        masterBut.xPos = 0;
        masterBut.yPos = 0;

        // draw buttons
        masterBut.drawButton();
        foreach(GuiButton button in myButtons) {
            button.masterRef = masterRef;
            button.drawButton();
            button.checkEvent(mouseUp);
        }
        masterBut.checkEvent(mouseUp);

    }

    void drawLines() {
        bool lastButton = false;
        for(int i = 0; i < myButtons.Count; ++i) {
            if(!lastButton && (i+1) < myButtons.Count){
                drawline(myButtons[i], myButtons[i+1]);
                lastButton = true;
            }else{
                lastButton = false;
            }
        }
    }

    void drawline(GuiButton button1, GuiButton button2) {
        DrawLine.drawLine(new Vector2((button1.xPos + masterRef.x) * masterRef.z,
                                      (button1.yPos + masterRef.y) * masterRef.z),

                          new Vector2((button2.xPos + masterRef.x) * masterRef.z,
                                      (button2.yPos + masterRef.y) * masterRef.z),
                          Color.black,
                          2);
    }
}
