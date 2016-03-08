using UnityEngine;
using System.Collections;

public class BuildingGUI : GUIScript {

	public override void guiStart ()
	{
		print ("start a building gui");
		alignToCenter = true;
	}
//	public GUIStyle style; // property set in Unity UI, on object containing this script
//	GUILayoutOption[] defaultOptions;
//	// Use this for initialization
//	void Start () {
//		if(defaultOptions == null)
//			defaultOptions = new GUILayoutOption[]{};
//	}
//
//	void OnGUI () {
//		makeWordArea();
//	}
//	// "Player Wood: " + PlayerResources.Instance.playerWood.Amount
//	
//	public string word;
//	public int xPos;
//	public int yPos;
//	int areaWidth = 23;
//	int areaHeight = 20;
//	void makeWordArea(){
//		int area = areaWidth * word.Length;
//		GUILayout.BeginArea (new Rect (xPos - (area/4), yPos, area, areaHeight));
//		GUILayout.BeginHorizontal();
//
//		GUILayout.Button(word, style, defaultOptions);
//		
//		GUILayout.EndHorizontal();
//		GUILayout.EndArea();
//	}
}
