using UnityEngine;
using System.Collections;

public class HandleInput : MonoBehaviour {


    public static bool hasKey(CU_Key cukey) {
        // TODO: returned on the frame the key is pressed
        // is this what we are wanting? needs to be checked every frame
        // should this be an event? is that possible?
        return  Input.GetButtonDown(cukey.key.ToString());
    }

    public KeyCode key = KeyCode.A;
}
