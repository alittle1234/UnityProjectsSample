using UnityEngine;
using System.Collections;

public class ActivityTranslate : ActivityMoves {
    
    override
    protected bool areEqual(Transform transform, Transform targetTransform) {
        return transform.position.Equals( targetTransform.position );
    }

    override
    protected void move(Transform transform, Transform targetTransform) {
        base.move( transform, targetTransform );

        //TODO: Physics based move. not adjusting transforms manually
        Vector3 p = lerpVect3( transform.position, targetTransform.position, curTime );
        transform.position = p;
    }
}
