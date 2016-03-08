using UnityEngine;
using System.Collections;

public class ActivityScale : ActivityMoves {
    
    override
    protected bool areEqual(Transform transform, Transform targetTransform) {
        return transform.position.Equals( targetTransform.position );
    }

    override
    protected void move(Transform transform, Transform targetTransform) {
        base.move( transform, targetTransform );

        Vector3 s = lerpVect3( transform.localScale, targetTransform.localScale, curTime );
        transform.localScale = s;
    }
}
