using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuildingResource
{
	public BuildingResource ()
	{
	}
	public BuildingResource (string type, int amount)
	{
		this.type = type;
		this.amount = amount;
	}
	
	public string type;

	public string Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}

	public int amount;

	public int Amount {
		get {
			return amount;
		}
		set {
			amount = value;
		}
	}
}

