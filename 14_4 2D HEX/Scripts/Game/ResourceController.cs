using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceController : MonoBehaviour {

	private static ResourceController instance = null;
	public static ResourceController Instance {
		get {
			return instance;
		}
		set {
			instance = value;
		}
	}

	public GameObject HexCellObject; // Hex pre fab- for instantiation
	public GameObject BuildingObject; // this should be like a base 'Actor'// a pre-fab with building neccesary components

	public HexComponent makeHexCell (Vector3 vector3)
	{
		return HexComponents[InstantiateMG(HexCellObject, vector3).Id];
	}

	public BuildingComponent makeBuilding (Vector3 vector3, PlayerResourceTypes package)
	{	BuildingComponent bc = BuildComponents[InstantiateMG(BuildingObject, vector3).Id];
		bc.setPackage(package);
		return bc;
	}

	/// <summary>
	/// Instantiates a Managed Object wich has an Id and a GameObject
	/// that can be used to quickly find a component in the Maps.
	/// </summary>
	/// <param name="obj"> Parameter description for s goes here</param>
	/// <seealso cref="ManagedObject">
	ManagedObject InstantiateMG(GameObject obj, Vector3 vector3){
		GameObject OBJ = (GameObject) Instantiate(obj, vector3, Quaternion.identity);
		ManagedObject mo = new  ManagedObject(OBJ);
		addToMaps(mo);
		return mo;
	}

	private Dictionary<long, ManagedObject> allMangObjs  = new Dictionary<long, ManagedObject>();
	private Dictionary<long, BuildingComponent> buildingComps  = new Dictionary<long, BuildingComponent>();
	public Dictionary<long, BuildingComponent> BuildComponents {
		get { return buildingComps; }
	}
	private Dictionary<long, HexComponent> hexCellComps  = new Dictionary<long, HexComponent>();
	private Dictionary<long, HexComponent> HexComponents {
		get { return hexCellComps; }
	}
	private Dictionary<long, TurnComponent> turnComps  = new Dictionary<long, TurnComponent>();
	private Dictionary<long, TurnComponent> TurnComponentsMap {
		get { return turnComps; }
	}
	private List<TurnComponent> turnCache;
	bool turnCacheIsFresh = false;
	public List<TurnComponent> TurnComponents {
		get { //GameRunner.debugIfNull("TurnVals", TurnComponentsMap.Values);
			return getTurnList();
		}
	}

	List<TurnComponent> getTurnList ()
	{
		if(turnCache== null || !turnCacheIsFresh){

			turnCache = new List<TurnComponent>(TurnComponentsMap.Values);
			turnCacheIsFresh = true;
		}
		return turnCache;
	}

	private ManageType[] manTypeVals = (ManageType[])Enum.GetValues(typeof(ManageType));
	void addToMaps (ManagedObject mo)
	{
		//Debug.Log("Adding MO" + mo.Id, this);
		allMangObjs.Add(mo.Id,mo);
		foreach(ManageType type in manTypeVals){
			switch(type){
			case ManageType.Building:
				BuildingComponent bc = mo.GameObjectIns.GetComponent<BuildingComponent>();
				if(bc!=null){
					bc.setTransform(mo.GameObjectIns.transform);
					buildingComps.Add(mo.Id, bc);
				}
				break;
			case ManageType.HexCell:
				HexComponent hc = mo.GameObjectIns.GetComponent<HexComponent>();
				if(hc!=null){
					hc.setTransform(mo.GameObjectIns.transform);
					hexCellComps.Add(mo.Id, hc);
				}
				break;
			case ManageType.Turn:
				TurnComponent tc = mo.GameObjectIns.GetComponent<TurnComponent>();
				if(tc!=null){
					//Debug.Log("Adding Turn" + mo.Id, this);
					tc.setTransform(mo.GameObjectIns.transform);
					turnCacheIsFresh = false;
					tc.parentId = mo.Id;
					turnComps.Add(mo.Id, tc);
				}
				break;
			}
		}
	}
	enum ManageType{
		Building,
		HexCell,
		Turn
	};

	
	//	Dictionary example = new Dictionary<string,string>();
	//	example.Add("hello","world");
	//	...
	//		Console.Writeline(example["hello"]);
	//	if (otherExample.TryGetValue("key", out value))
	//	{
	//		otherExample["key"] = value + 1;
	//	}
	
//	List<TurnComponent> collectAllTurnObjects ()
//	{
//		GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
//		foreach(GameObject obj in objs){
//			if(obj.GetComponent<TurnComponent>() != null)
//				turns.Add(obj.GetComponent<TurnComponent>());
//		}
//		return turns;
//	}
}

