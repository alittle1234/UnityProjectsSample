using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class s_SceneLoader : MonoBehaviour {

	// Use this for initialization
	//void Start () { }
	
	// Update is called once per frame
	//void Update () { }

	//
	// COMPONENT SERVICE
	//
	
	Dictionary<object,object> sceneMap ;
	string playerKey = "player";
	string panelMapKey = "panelMap";

	public void loadScene(string name){
		sceneMap = getData();
		Dictionary<string, Panel> panelMap = (Dictionary<string, Panel>)sceneMap[panelMapKey];
		// create panel array?
		// for the scene objects
		// create location map
		// create object map
		// create diaolog map
		Player player = getPlayer();
		ArrayList roomPanelItems = getStagePanels(player);
		foreach(PanelItem k in roomPanelItems){
			Panel panel = panelMap[ k.getPanelName() ];
			panel.setOutput(k.getOutput());
		}
		// panels are ready.....



		// one room
		// player object
		// get player starting point
			// compare to palyer save
		// add room description to panel
		// add room options to panel
	}
	//<summary>
	/// should return a player with populated stats
	/// either from a save file or from the level
	///</summary>
	Player getPlayer () {
		//if( PersistanceService.hasPlayer() ){
		//  return PersistanceService.getPlayer();
		// }
		Player player = new Player(sceneMap[playerKey]);
		// make sure if all default data is not filled in, that it gets filled in
		return player;
	}

	//<summary>
	/// this should return a list of panel data to populate this scenes panels
	/// it should take into account the player stats, stage, and other data
	///</summary>
	ArrayList getStagePanels (Player player) {
		string currentRoom = player.getRoom();
		string stage = player.getStage();
		ArrayList startingRoomStagePanels = new ArrayList();

		// get room map for stage, player stats....
		// how?
		// how dose the level know?
		// it dosnt....
		// only one so far, add logic later


		return startingRoomStagePanels;
	}

	///<summary>
	/// a player should contain vital stats like health, or level
	/// it should be loaded from the level, or from a save file
	///</summary>
	public class Player{
		public Player (object obj) {
			throw new NotImplementedException ();
		}

		string stage;
		public string getStage(){
			return stage;
		}
		string roomName = "defualt"; // or "1" ?
		public string getRoom () {
			return roomName;
		}
		public void setRoom(string room){
			roomName = room;
		}
	}

	///<summary>
	/// a panel should be a ui drawable element
	/// should be able to draw text and images and buttons
	///</summary>
	public class Panel{
		string name;
		object output;
		public void setOutput(object outputData){
			this.output = outputData;
		}
		public void draw(){
			throw new NotImplementedException ();
		}
	}

	///<summary>
	/// a panel Item is pulled from level data
	/// should contain a name for a panel and some output
	/// parseing of data can occur at... some time
	///</summary>
	public class PanelItem{
		string panelName;
		public string getPanelName(){
			return panelName;
		}
		object outputData;
		public object getOutput(){
			return outputData;
		}
		public void setOutput(object outputRaw){
			this.outputData = outputRaw;
		}

	}

	///<summary>
	/// this should parse a file and get all the level data
	///</summary>
	Dictionary<object,object> getData () {
		throw new NotImplementedException ();
	}
}
