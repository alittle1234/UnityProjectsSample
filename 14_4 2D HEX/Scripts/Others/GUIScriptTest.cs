using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScriptTest : MonoBehaviour {
	
	List<string> myWords = new List<string>();
	
	string[] activeLetters;
	
	
	public GUIStyle activeStyle; // property set in Unity UI, on object containing this script
	public GUIStyle inactiveStyle;
	GUIStyle letterStyle = new GUIStyle();
	// Use this for initialization
	void Start () {
		myWords.Add("house");
		myWords.Add("car");
		myWords.Add("abigail");
		myWords.Add("adam");
		myWords.Add("reggie");
		myWords.Add("asange");
		myWords.Add("renegade");
		
		activeLetters = new string[]{"_","_","_","_","_","_","_","_","_","_",};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	void OnGUI () {
		// username
		/*
		 * several names
		 * different lengths
		 * similar characters
		 * 
		 * blue
		 * red
		 * 
		 * response time indicate number of 
		 * valid characters in name,
		 * from begining
		 * 
		 * select letters bassed on time
		 * sort by each letter
		 * 
		 * 
		 * house
		 * car
		 * abigail
		 * adam
		 * reggie
		 * asange
		 * renegade
		 *
		 */
		
		for(int k = 0; k < myWords.Count; ++k){
			
			makeWordArea(myWords[k]);
		}
		string actltrs = "";
		for(int i = 0; i < activeLetters.Length; ++i){
			actltrs += activeLetters[i];
		}
		makeWordArea(actltrs);
		
		nextY = 10;		
	}
	
	int nextX = 10;
	int nextY = 10;
	int areaWidth = 23;
	int areaHeight = 20;
	int wordHGap = 2;
//	int baseLetX = 11;
//	int baseLetY = 11;
//	int nextLetX = 0;
//	int nextLetY = 0;
	void makeWordArea(string word_){
		GUILayout.BeginArea (new Rect (nextX, nextY, areaWidth * word_.Length, areaHeight));
		GUILayout.BeginHorizontal();
		for(int i = 0; i < word_.Length; ++i){
			
			if( activeLetters[i] == word_[i].ToString() ){
				letterStyle = activeStyle;
			}else{
				letterStyle = inactiveStyle;
			}
			if(GUILayout.Button(word_[i].ToString(), letterStyle, new GUILayoutOption[]{}) ){
				activeLetters[i] = word_[i].ToString();
			}
			
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
		nextY += areaHeight + wordHGap;
	}
}
