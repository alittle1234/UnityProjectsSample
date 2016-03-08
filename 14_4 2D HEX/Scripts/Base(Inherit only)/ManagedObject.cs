using UnityEngine;
using System.Collections;

public class ManagedObject {

	private GameObject go;
	public GameObject GameObjectIns {
		get {
			return go;
		}
		set {
			go = value;
		}
	}
	private long id;
	public long Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	static long lastId = 0;
	static long makeId ()
	{
		lastId += 1;
		return lastId;
	}

	public ManagedObject(GameObject gameObj){
		GameObjectIns = gameObj;
		Id = makeId();
	}
}
