using UnityEngine;
using System.Collections;

public class BuildingComponent : BaseComponent  {


	BuildingResource playerResource; // mono behaviour or straight class? 
									 //can not make NEW mono. can only INSTANCE a prefab
	BuildingResource localResource;

	//public BuildingGUI gui;
	BuildingGUI guiInst;

	protected override void myStart ()  {
		guiInst = GetComponent<BuildingGUI>();

		//this.playerResource = PlayerResources.Instance.playerWood;
		//this.localResource = new BuildingResource(this.playerResource.Type, 0);

		//Debug.Log("Instanced:  Pr: " + this.playerResource.Type, null);
	}

	PlayerResourceTypes package; // not really needed yet, this variable here just in case.
	public void setPackage (PlayerResourceTypes package_)
	{
		this.package = package_;

		this.localResource = new BuildingResource(package.ToString(), 0);
		this.playerResource = PlayerResources.Instance.getResource(package.ToString());

	}
	
	public BuildingResource getLocalResource(){
		return this.localResource; // returns the building componet 'local' resource; res for just this building.
	}
	
	void Update () { // Update is called once per frame
		updateGUI();
	}

	private void updateGUI(){
		if(guiInst!=null){
			if( this.localResource != null && localResource.Amount > 0 )
				guiInst.buttonText = localResource.Type + ": " + localResource.Amount;
			else
				guiInst.buttonText = "";

			Vector3 screenPos = getTransScreenPos(thisTransform.position);
			guiInst.xPos= (int)screenPos.x;
			guiInst.yPos= (int)screenPos.y + 28;
		}
	}

	public void consumeResource (int amnt)
	{
		//Debug.Log("Building:  Make Resource", null);
		
		if(this.playerResource != null){
			//Debug.Log("playerResource:  Not Null", null);

			this.playerResource.Amount -=  amnt;
			this.localResource.Amount -=  amnt;
		}
	}

	public void makeResource(int amnt){
		//Debug.Log("Building:  Make Resource", null);

		if(this.playerResource != null){
			//Debug.Log("playerResource:  Not Null", null);
			this.playerResource.Amount +=  amnt;
			this.localResource.Amount +=  amnt;
		}
	}


}
