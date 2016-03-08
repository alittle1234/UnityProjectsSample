using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRunner : MonoBehaviour {
	bool gameLoaded = false;
	public Vector3[] HexPositions;
	ResourceController rc;

	GUIScript gui_woodLabel;


	public class GameMenu{
		BuildingButtonsGUI buildingMenu;
		public BuildingButtonsGUI Buildings {
			get{
				return buildingMenu;
			}
			set{
				buildingMenu = value;
			}
		}
	}
	GameMenu menu;
	public GameMenu Menu {
		get{
			return menu;
		}
		set{
			menu = value;
		}
	}

	void Start () {
		rc =  GetComponent<ResourceController>();
		gui_woodLabel = GetComponent<GUIScript>();
		ResourceController.Instance = rc;
		GameRunner.Instance = this;

		menu = new GameMenu();
		menu.Buildings = GetComponent<BuildingButtonsGUI>();
	}
	
	private static GameRunner gr;
	public static GameRunner Instance {
		get {
			return gr;
		}
		set {
			gr = value;
		}
	}

	void Update () {// Update is called once per frame
		if( !gameLoaded && Input.GetKeyDown(KeyCode.A) ){
			loadGame();
		}

		
		if( Input.GetKeyDown(KeyCode.J) ){
			startNewTurn();
		}

		updateGui();
	}

	void updateGui ()
	{
		if(gui_woodLabel != null){
			BuildingResource res = PlayerResources.Instance.getResource(PlayerResourceTypes.WOOD.ToString());
			gui_woodLabel.buttonText = "Player Wood: " + 	res.amount ;
		}
	}

	void loadGame ()
	{
		gameLoaded = true;
		// 4 positions
		for(int i = 0; i < HexPositions.Length; ++i){
			ResourceController.Instance.makeHexCell(HexPositions[i]);
		}
	}

	public static void debugIfNull<T>(string name, T obj){
		if(obj != null){
			Debug.Log(name +": Good(not null)", null);
		}
		else
			Debug.Log(name +": is Null ", null);
	}

	public void startNewTurn ()
	{
		//Debug.Log("Starting new Turn:", null);
		performTurn(ResourceController.Instance.TurnComponents);
	}
	
	void performTurn (List<TurnComponent> turns)
	{
		//Debug.Log("Turns# " + turns.Count, null);
		foreach(TurnComponent turn in turns){
			turn.performTurn();
		}
	}
}
