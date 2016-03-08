using UnityEngine;
using System.Collections;

using LitJson;
using System.Collections.Generic;
using AssemblyCSharp.Level;

public class s_Scene : MonoBehaviour {


	public static string output;
	public static MyButton[] options;

	public static s_UIDraw uiControler;


	private static Level level = null;
	public static Level.Player player = null;

    /// <summary>
    /// contains a map of all the viewing panels perscribed by a level.
    /// probably will not be needed to define more than once
    /// </summary>
    static Dictionary<string, Panel> panelsMap;

    /// <summary>
    /// map of locations in the level
    /// each location has a room map
    /// </summary>
    static Dictionary<string, Level.Location> locationMap;

    /// <summary>
    /// the scripts that can be activated for the level
    /// </summary>
    static Dictionary<string, Script> levelScriptsMap;

	public static void refreshUI() {
		print("s_Scene: Refreshing UI." + uiControler);

		s_UIDraw uidraw = uiControler;


//		print("s_Scene: LOADING LEVEL." + buttons);
        ScreenPlay play = TextLoader.loadXml<ScreenPlay>("screen play");
		TextUtil.add(TextLoader.loadXml<TextLibrary>("textLibrary"));
		
		string content = TextUtil.get("text");

		cs_SceneLoader sceneLoader = new cs_SceneLoader();
		if(level == null) {
			level = sceneLoader.loadScene("testJson");
			player = level.player;
			createScriptMap();
			initializePanels(level);
			initializeLocations(level);
            getLevelItems(level);
		}


		uidraw.myButtons = convertPanelsToButtons(level);

		// how to get panels text loaded?
		// each button changes the text
		// each panel needs to have text refershed

		// set up scenario
		// change the text and options
		// than go back?, the next frame?, etc... adjusting the schema as you go


		print("s_Scene: Done Refresh."); 
	}

	/// <summary>
	/// Creates the level scripts map for the level already loaded.
	/// </summary>
	static void createScriptMap() {
		levelScriptsMap = new Dictionary<string, Script>();
//		for(int i = 0; i < level.scripts.Length; ++i) {
//			Script script = level.scripts[i];
//			levelScriptsMap[script.varname] = script;
//		}
	}

	/// <summary>
	/// Initializes the panels used to print info when level loads.
	/// </summary>
	/// <param name="level">Level.</param>
	static void initializePanels(Level level) {
		panelsMap = new Dictionary<string, Panel>();
//		for(int i = 0; i < level.panels.Length; ++i) {
//			panelsMap[level.panels[i].varname] = level.panels[i];
//		}
	}

	/// <summary>
	/// Stores the locations on level load in an easy to access map.
	/// </summary>
	/// <param name="level">Level.</param>
	static void initializeLocations(Level level) {

		locationMap = new Dictionary<string, Level.Location>();
		for(int i = 0; i < level.locations.Count; ++i) {
			Level.Location loc = level.locations[i];
			loc.roomMap = getRoomMap(loc);
			locationMap[level.locations[i].varname] = loc;
		}
	}
	/// <summary>
	/// Gets the rooms for each location in this level
	/// </summary>
	/// <returns>The stage map.</returns>
	/// <param name="loc">Location.</param>
	static Dictionary<string, Level.Room> getRoomMap(Level.Location location) {

		Dictionary<string, Level.Room> roomMap = new Dictionary<string, Level.Room>();
        for(int i = 0; i < location.rooms.Count; ++i) {
			Level.Room room = location.rooms[i];
			roomMap[room.varname] = room;
		}
		return roomMap;
	}
    
    
    static Dictionary<string, Level.Item> levelItems;
    static void getLevelItems(Level level) {
        
        levelItems = new Dictionary<string, Level.Item>();
//        for(int i = 0; i < level.levelItems.Length; ++i) {
//            Level.Item item = level.levelItems[i];
//            levelItems[item.varname] = item;
//        }
    }

    /// <summary>
    /// will convert all of the descriptive content into viewable 
    /// and clickable content on screen
    /// </summary>
    /// <returns>The panels to buttons.</returns>
    /// <param name="level">Level.</param>
	static MyButton[] convertPanelsToButtons(Level level) {

		ArrayList uiContent_butArray = new ArrayList();

		string location = level.player.locationVarName;
		string room = level.player.roomVarName;
        string description = TextUtil.get( locationMap[location].roomMap[room].descView_txt );

        // put desc in panel 1
        // put options in panel 2

        MyButton panelMain_But = new MyButton();
		Panel panel = panelsMap["panel1"]; //[stagePanels[0].varname];
        panelMain_But.h = panel.h;
        panelMain_But.w = panel.w;
        
        panelMain_But.x = panel.x;
        panelMain_But.y = panel.y;
        
		panelMain_But.text = description;

        uiContent_butArray.Add(panelMain_But);

		uiContent_butArray = getOptionsButtons(uiContent_butArray, panelMain_But, locationMap[location].roomMap[room]);

        MyButton[] temparray = uiContent_butArray.ToArray(typeof(MyButton)) as MyButton[];
        // string[] array = list.ToArray(typeof(string)) as string[];
        return temparray;
	}

	/// <summary>
	/// Gets the options buttons for a panel. 
	/// puts buttons in list form, non scrollable, starting from y_start
    /// 
    /// should accept entire level as param
    /// use items to create default buttons
    ///     doors
    ///     pickups
    ///     characters
    /// 
	/// </summary>
	/// <returns>The options buttons.</returns>
	/// <param name="buttonArr">Button arr.</param>
	/// <param name="thisPanel">This panel.</param>
	/// <param name="content">Content.</param>
	static ArrayList getOptionsButtons(ArrayList buttonArr, MyButton thisPanel, Level.Room room) {

        // for each item
        // make button from item
        int optNum = 0;
//        Level.ItemTemplate[] items = room.items;
//        if(items != null){
//            for(int i = 0; i < items.Length; ++i) {
////                if(levelItems[items[i].itemVarname].itemType.Equals("character") &&
////                   items[i].state.Equals("active")){
////                    buttonArr.Add(getOptionButton("text", "dialog", getButtonPosition(optNum++)));
////                }
//            }
//        }

//		Level.Option[] options = room.options;
//		for(int i = 0; i < options.Length; ++i) {
//
//			// todo -- expanded options, nested options recurse
//            buttonArr.Add(getOptionButton(
//                options[i].label_txt,
//                options[i].script_varname,
//                getButtonPosition(optNum++)));
//
//		}
		return buttonArr;
	}

    static MyButton getOptionButton(string label, string scriptVarName, Vector2 xy) {
        MyButton option_but = new MyButton();
        option_but.h = 10;
        option_but.w = 25;

        option_but.x = (int)xy.x;
        option_but.y = (int)xy.y; // <--- placement of buttons?
        // scrollable field
        
        option_but.text = TextUtil.get(label);
        
        option_but.script = levelScriptsMap[scriptVarName];
        return option_but;
    }

	static int x_start = 50;
	static int y_start = 50;
	static int interval = 20;
	static Vector2 getButtonPosition(int num) {
		// get the panel content height?
		//
		return new Vector2(x_start, y_start + num * interval);
	}

	static protected void print(string mesg) {
		Debug.Log(mesg);
	}
}
