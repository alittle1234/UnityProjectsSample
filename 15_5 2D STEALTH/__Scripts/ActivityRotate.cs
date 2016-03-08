using UnityEngine;
using System.Collections;

public class ActivityRotate : ActivityMoves {
    
    override
    protected bool areEqual(Transform transform, Transform targetTransform) {
        return transform.position.Equals( targetTransform.position );
    }

    override
    protected void move(Transform transform, Transform targetTransform) {
        base.move( transform, targetTransform );

        Quaternion q = Quaternion.RotateTowards( transform.rotation, targetTransform.rotation, rotationSpeed );
        transform.rotation = q;
    }
}
