using UnityEngine;
using System.Collections;

public class GuiButton : MonoBehaviour {
    
    private GUIStyle buttonStyle;                         // property set in Unity UI, on object containing this script
    public  GUIStyle activeStyle = new GUIStyle();        // property set in Unity UI, on object containing this script
    public  GUIStyle inactiveStyle = new GUIStyle();      // property set in Unity UI, on object containing this script
    GUILayoutOption[] defaultOptions;

    public string buttonText = "";
    public Texture texture = null;
    public float xPos = 10;
    public float getXPos(){
        return (xPos + masterRef.x) * masterRef.z;
    }
    public float yPos = 10;
    public float getYPos(){
        return (yPos + masterRef.y) * masterRef.z;
    }
    public float height = 10;
    public float width = 10;
    public Color color;

    float scale = 1;

    public Vector3 masterRef = new Vector3(0,0,1);

    private Vector2 areaDimn;
    public Rect butRect = new Rect();
    
    public bool alignToCenter = false;
    
    public bool isClicked = false;
    public bool textIsSize = true;

    public GuiButton(int xPos, int yPos, string buttonText) {
        this.xPos = (float)xPos;
        this.yPos = (float)yPos;
        this.buttonText = buttonText;
    }
    public GuiButton(int xPos, int yPos, string buttonText, Vector3 masterRef) {
        this.xPos = (float)xPos;
        this.yPos = (float)yPos;
        this.buttonText = buttonText;
        this.masterRef = masterRef;
    }
    
    public GuiButton(float xPos, float yPos, float height, float width, string buttonText, Vector3 masterRef) {
        this.xPos = xPos;
        this.yPos = yPos;
        this.buttonText = buttonText;
        this.masterRef = masterRef;
        this.height = height;
        this.width = width;
        textIsSize = false;
    }

    public GuiButton(float xPos, float yPos, float height, float width, string buttonText, Color backColor, Vector3 masterRef) {
        this.xPos = xPos;
        this.yPos = yPos;
        this.buttonText = buttonText;
        this.masterRef = masterRef;
        this.height = height;
        this.width = width;
        textIsSize = false;
        color = backColor;
    }

    static Texture2D staticTexture;
    void Start() { // Use this for initialization
        if(defaultOptions == null) {
            defaultOptions = new GUILayoutOption[]{};
        }

        Texture2D img = Resources.Load<Texture2D>("black1x2i");
        
        activeStyle = new GUIStyle();
        activeStyle.border = new RectOffset(2,2,2,2);
        GUIStyleState slate = new GUIStyleState();
        slate.background = img;
        
        activeStyle.normal = slate;
        activeStyle.active = slate;
        activeStyle.focused = slate;

        inactiveStyle = activeStyle;
    }
    
    void OnGUI() {
        drawButton();
    }
    
    /// <summary>
    /// draw the buton
    /// </summary>
    public void drawButton() {
        buttonStyle = (isClicked) ? activeStyle : inactiveStyle;
        
        if(textIsSize){
            areaDimn = buttonStyle.CalcSize(new GUIContent(buttonText));
            butRect.height = areaDimn.y;
            butRect.width = areaDimn.x;
        }else{
            butRect.height = height * masterRef.z;
            butRect.width = width * masterRef.z;
        }
        
        // position scales with zoom level
        butRect.x = (xPos + masterRef.x) * masterRef.z;
        butRect.y = (yPos + masterRef.y) * masterRef.z;

        if(color != null){
            if (staticTexture == null) {
                staticTexture = new Texture2D(1, 1);
            }
            staticTexture.SetPixel(0, 0, color);
            staticTexture.Apply();
            GUI.DrawTexture(butRect, staticTexture);
            GUI.Label(butRect, buttonText);
        }else{
            GUI.Box(butRect, buttonText);
        }

        // draw text in new button / label
        // scale font with z
    }

    /// <summary>
    /// Trigger the checks for mouse events on this button.
    /// </summary>
    /// <param name="mouseUp">If set to <c>true</c> isClicked is false.</param>
    public void checkEvent(bool mouseUp){
        if(mouseUp) {
            isClicked = false;
            return;
        }
        if(Event.current.type == EventType.Used){
            return;
        }

        if(Event.current.type == EventType.MouseDown 
                && butRect.Contains(Event.current.mousePosition)) {

            isClicked = true;
            Event.current.Use();
        } 
        else if(Event.current.type == EventType.MouseDrag 
                && isClicked) {
            if(masterRef.z != 0){
                // drag scales with zoom level
                xPos += (Event.current.delta.x) / masterRef.z;
                yPos += (Event.current.delta.y) / masterRef.z;
            }
        }
    }
}
