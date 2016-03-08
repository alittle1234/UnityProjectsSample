using UnityEngine;
using System.Collections;

public class TurnComponent : BaseComponent {

	public long parentId;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void performTurn ()
	{
		myTurn();
	}

	public virtual void myTurn ()
	{
		throw new System.NotImplementedException();
	}
}
