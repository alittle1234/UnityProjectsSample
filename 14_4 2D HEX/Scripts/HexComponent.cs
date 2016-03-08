using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexComponent : BaseComponent {

	protected override void myStart () {
	}
	

	void Update () {
		performActions();
	}

	void performActions () {
		// do whatever a hex dose during an update loop
		temp_KeyDownChecks();
	}

	void temp_KeyDownChecks(){

		if(Input.GetKeyDown(KeyCode.A) && selectable.isSelected){
			makeBuildingResource();
		}

		if(Input.GetKeyDown(KeyCode.D) && selectable.isSelected){
			consumeBuildingResource();
		}

	}

	void consumeBuildingResource () {
		//Debug.Log("Perform Consume Resource", this);
		// create a building if one dose not exist
		temp_GiveHexABuilding();
		// force building to make a resource
		getBuilding().consumeResource(5); // can a building have more than one output?
	}

	void makeBuildingResource () {
		//Debug.Log("Perform Make Resource", this);
		// create a building if one dose not exist
		if(temp_GiveHexABuilding() != null ){
			// force building to make a resource
			getBuilding().makeResource(5); // can a building have more than one output?
		}
	}

	BuildingComponent buildingInst; // may be null.
	BuildingComponent getBuilding () {
		return buildingInst;
	}

	BuildingComponent temp_GiveHexABuilding () {
		if(buildingInst == null){
			//Debug.Log("Building is null, Making BuildingBehavior", this);

			// IF GAMERUNNER.MENU BUILDING SELECTED
			if(GameRunner.Instance.Menu.Buildings.Selected != null){
				buildingInst = ResourceController.Instance.makeBuilding(thisTransform.position,
				                                                        GameRunner.Instance.Menu.Buildings.Selected);
			}
		}
		return getBuilding();
	}

	
}
