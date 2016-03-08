using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerResources  {
	//public BuildingResource playerWood = new BuildingResource("wood", 0);



	private static PlayerResources instance = null;
	public static PlayerResources Instance {
		get {
			if (instance == null) {
				instance = new PlayerResources ();
			}
			return instance;
		}
	}

	private Dictionary<string, BuildingResource> allResources  = new Dictionary<string, BuildingResource>();
	public BuildingResource getResource (string type)
	{
		BuildingResource value;
		if (!allResources.TryGetValue(type, out value))
				{
			allResources.Add(type, new BuildingResource(type,0));
			value = allResources[type];
				}

		return value;
//		if(allResources[type] == null){
//
//		}
//		return allResources[type];
	}
}
