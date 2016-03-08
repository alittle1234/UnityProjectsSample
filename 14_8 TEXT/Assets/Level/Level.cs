using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace AssemblyCSharp.Level {
public class Checkable{
    [XmlIgnore]
    public bool isChecked;
    [XmlIgnore]
    public bool isUnFolded;
}

[XmlRoot("Level")]
public class Level : Checkable {
    public long lastId;

    public Level(){ }

    [XmlElement ("Player")]
    public Player player;
    
    public class Player {
        // current location
        [XmlAttribute ("locationVarName")]
        public string locationVarName;

        // current room
        [XmlAttribute ("roomVarName")]
        public string roomVarName;
    }

    [XmlElement ("Location")]
	public List<Location> locations = new List<Location>();

    public class Location : Checkable  {
    
        public Location(){ }

        [XmlIgnore]
		public Dictionary<string, Room> roomMap;

        [XmlIgnore]
        public bool isChecked;
        [XmlIgnore]
        public bool isUnFolded;

        [XmlAttribute ("varname")]
        public string varname;

        [XmlAttribute ("title_txt")]
		public string title_txt;
		
        [XmlElement ("Room")]
        public List<Room> rooms = new List<Room>();
	}

    public class Room  : Checkable {

        [XmlAttribute ("varname")]
		public string varname;

        // description given by default
        [XmlAttribute ("descView_txt")]
        public string descView_txt;

        // items contained in the room
        // includes doors, pickup objects, and characters

        [XmlElement ("Item")]
        public List<Item> items = new List<Item>();

        // pick-up items should auto create a pick up scritp?
        //      same with talk-to characters
        //      same with locked/unlocked doors

        // scripts that can be executed anytime in the room? creates a button clickable
        // public Script[] scripts;

            //		public Option[] options;
            [XmlIgnore]
            public bool showItems;
	}
//	public class Content {		
//		public string varname;
//		public string desc_txt;
//		public Option[] options;
//	}

    // list of scripts that can be run on actions
//	public Script[] scripts;
//	public class Script {		
//		public string varname;
//		public string script;
//		public string[] target;
//	}
	
//	public class Option {
//		public string varname;
//		public string label_txt;
//		public string script_varname;
//	}
	
	



	
    // declare world items here?
    // all items?
    // item templates?
//    public Item[] levelItems;

        public class Item  : Checkable {

        [XmlAttribute ("itemType")]
        public string itemType;

        [XmlAttribute ("varname")]
        public string varname;

        [XmlAttribute ("name_txt")]
        public string name_txt;

        [XmlIgnore]
        public string x;
        [XmlIgnore]
		public string y;
	}


//	public Panel[] panels;
}

}