using UnityEngine;

public class ButtonPromt : MonoBehaviour {

    /// <summary>
    ///  has a promt area
    ///  writes promts to screen
    /// </summary>
    /// 

    public GUI promptGui;
    static private string buttonText = "";
    /// <summary>
    /// should write the message on the screen
    /// </summary>
    /// <param name="mesg"></param>
    public static void promt(string mesg) {
        buttonText = mesg;
    }

    void OnGUI() {
        drawButton();

        buttonText = "";
    }

    public  GUIStyle activeStyle = new GUIStyle();        // property set in Unity UI, on object containing this script
    public  GUIStyle inactiveStyle = new GUIStyle();      // property set in Unity UI, on object containing this script

    private GUIStyle buttonStyle;
    public Rect butRect = new Rect();
    public float height = 10;
    public float width = 10;
    public float xPos = 10;
    public float yPos = 10;


    public void drawButton() {
        buttonStyle = activeStyle;

        butRect.height = height;
        butRect.width = width;

        butRect.x = ( xPos  ) ;
        butRect.y = ( yPos  ) ;


        GUI.Box( butRect, buttonText );
    }
}
