using UnityEngine;
using System.Collections;

public class s_Movement : MonoBehaviour {

	public static void moveToTarget(Rigidbody _rigidbody, Vector3 targetPos, float baseSpeed, bool noY){
		Vector3 direction = getMoveDirection(_rigidbody.position, targetPos);
		if(noY){ direction.y = 0; }
		moveBody(_rigidbody, direction.normalized, baseSpeed);
	}

	public static void moveBody(Rigidbody _rigidbody, Vector3 direction, float baseSpeed){
		_rigidbody.position += direction * baseSpeed * Time.deltaTime;
	}

	public static Vector3 getMoveDirection (Vector3 objLocation, Vector3 targetLocation)
	{
		//heading = target.position - player.position;
		return targetLocation - objLocation;
	}
	
	public static bool withInDist (Vector3 position1, Vector3 position2, float radius)
	{
		return Vector3.Distance(position1, position2) <= radius;
	}

	/** get maximum distance an object will travel given velocity and angle(Radian)
	 * 	gravity is constant(9.81)
	 */
	public static float getMaxDistance (float velocity, float angleR)
	{
		//maxDist = ((velocity * velocity) * ((Mathf.Sin((-angle * Mathf.Deg2Rad) *2))))/9.81;
		return (sqr(velocity) * Mathf.Sin(2 * angleR)) / 9.81f;
	}

	/** get the location of an object when endHeight equal to startingHeight
	 */
	public static Vector3 getFinalImpactPoint (Vector3 startPosition, float maxRange, Vector3 dirctionNormalized)
	{
		return startPosition + (maxRange * dirctionNormalized);
	}

	
	static float sqr(float num){
		return num * num;
	}
}
