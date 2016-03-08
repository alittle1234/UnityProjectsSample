using UnityEngine;
using System.Collections;

public class BuildingTurn : TurnComponent {

	int amnt = 5;
	override public void myTurn ()
	{
		// local resource
		// increase by an amount
		ResourceController.Instance.BuildComponents[parentId].makeResource(amnt);
	}
}
