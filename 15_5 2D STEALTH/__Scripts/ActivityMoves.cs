using UnityEngine;
using System.Collections;

public class ActivityMoves : MonoBehaviour {
    /// <summary>
    /// moves an object from start position
    /// to end position when activated
    /// 
    /// restarts?
    /// when activated again?
    /// immediatly after end?
    /// flips direction when activated again?
    /// (moving backwards)
    /// 
    /// 
    // start moving obj from start to end
    // if at end, switch?

    // teleport to start on toggle?

    // over time, with speed inlcuded

    // needs transform
    
    // TODO: PHYSICS.... should not step transform physical objects. will create weird instances of overlap, jolts, etc
    // TODO: how to do scale physically? or any of it really.

    /// </summary>
    public      Transform   transform;

    public      Transform   startPosition;
    public      Transform   endPosition;

    public      bool        resetsOnEnd = true;

    protected   bool        hasStarted = false;
    protected   Transform   targetTransform;

    public      float       speed = 10;
    public      float       time = 1000;
    protected   float       curTime = 0;

    protected   bool        movingForward = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if( hasStarted ) {
            if( !areEqual( transform, targetTransform ) ) {
                move( transform, targetTransform );
            } else {
                hasStarted = false;
                movingForward = ( resetsOnEnd ) ? !movingForward : movingForward;
            }
        }
	}
    /// <summary>
    /// should see if the transforms are equal, mainly for stoping the Move when equal
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="targetTransform"></param>
    /// <returns></returns>
    protected bool areEqual(Transform transform, Transform targetTransform);

    /// <summary>
    /// should move the Transform toward the Target incrementally
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="targetTransform"></param>
    protected void move(Transform transform, Transform targetTransform) {
        setCurTime();
    }

    /// <summary>
    /// set the curTime varible based on delta time
    /// curTime should increment toward 'time'
    /// </summary>
    private void setCurTime() {
        curTime = Time.deltaTime + curTime;
        if( curTime > time ) {
            curTime = time;
        }
    }

    /// <summary>
    /// Interpolates x y and z of a Vector3
    /// </summary>
    /// <param name="cur"></param>
    /// <param name="targ"></param>
    /// <param name="elap"></param>
    /// <returns></returns>
    protected Vector3 lerpVect3(Vector3 cur, Vector3 targ, float elap) {
        return new Vector3(
           Mathf.Lerp( cur.x, targ.x, elap ),
           Mathf.Lerp( cur.y, targ.y, elap ),
           Mathf.Lerp( cur.z, targ.z, elap ) );
        
    }

    /// <summary>
    ///  should overide perform activity from somewhere else
    ///  
    /// meaning, this should be extended class of some other class that
    /// contains 'performActivity' such as BaseActivity
    /// </summary>
    void performActivity() {
        hasStarted = true;
        if( movingForward ) {
            targetTransform = endPosition;
        } else {
            targetTransform = startPosition;
        }
        curTime = 0;
    }
}
