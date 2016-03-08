using UnityEngine;

public class ActivityInteractive : MonoBehaviour {

    /// while a player is looking at 
    /// the object, it can be 'activated'
    /// 
    /// when activated, toggles 'toggleTarget'


    // key to press
    public      CU_Key      activateKey;

    // should print button prompt?
    public      bool        showButtonPrompt = true;

    // is currently activated?
    private     bool        isActive = false;


    // this transform for the object. get distance, look at, etc.
    public      Transform   transform;

    // minimum distance player/other must be to activate
    public      float       distance = 10;

    // traps the player from moving while the object is active
    public      bool        keepsPlayer = true;
    // TODO: pauses game? other actions automatic?

    // a reference to the player target // TODO: remove when player is Global
    public      CU_Player      playerTarget;

    // the activity that will be activated/toggled when this script is activated // TODO: handle interaction between Scripts more better
    public      ActivityToggle  toggleTarget;


    // TODO: is on press, on key up, press and hold, sensitivity?... new logic for each press type


	// Use this for initialization
	void Start () {
	
	}

    // set to true when a button should be prompted, or a button press will activiate
    private bool canBeActivated = false;

	// Update is called once per frame
	void Update () {
        if( Vector3.Distance(transform.position, playerTarget.transform.position)
            <= distance ) {
            
            // TODO: add look at to interactive script
            // and looking at
            // and not obstructed
            //   getLookAt();

            canBeActivated = true;
        }

        // can be activated
        // can have ui activate me
        if( canBeActivated && showButtonPrompt ) {
            printButtonPrompt();
        }


        // if can activate and activated
        // send toggle to toggleTarget
        if( canBeActivated ) {
            toggleTheTarget();
        }

	}

    /// <summary>
    /// should handle the input
    /// toggle the target if input recieved
    /// </summary>
    private void toggleTheTarget() {
        if( HandleInput.hasKey( activateKey ) ) {
            toggleTarget.toggle();
        }
    }

    /// <summary>
    /// should this be in Interactive or some UI type class?
    /// 
    /// how to handle Printing to screen at certian stages of interactions?
    /// </summary>
    private void printButtonPrompt() {
        ButtonPromt.promt( "Press " + activateKey + " to Activate." );
    }
}
