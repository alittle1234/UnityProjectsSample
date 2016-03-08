using UnityEngine;

public class ActivityToggle : MonoBehaviour {

    /// <summary>
    ///  should toggle the state of the activity
    ///  
    ///  current state ->
    ///  next state....
    ///  
    ///  repeats?
    /// </summary>
    public void toggle() {
        // if next state > size, next state = 0
        // state[nextstate]. performActivity();
    }

    public AS states[];
    public int nextState = -1;

}
