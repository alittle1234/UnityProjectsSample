using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_Runner : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		clampVelocity();
		doInputMovement();
	}


	void doInputMovement ()
	{
		isRunning = false;
		if (Input.GetKey("w")) 
		{
			moveForward();
		}
		if (Input.GetKey ("a")) 
		{
			rotateLeft();
		}
		if (Input.GetKey("s")) 
		{
			moveBack();
		}
		if (Input.GetKey ("d")) 
		{
		 	rotateRight();
		}

		if (Input.GetKey ("space")) 
		{
			liftUp();
		}

		if (Input.GetKey ("left ctrl")) 
		{
			liftDown();
		}

		if(isRunning){
			turnOnParticle();
		}else{
			turnOffParticle();
		}

		applyDrag();
		knownVelocity = rigidbody.velocity.magnitude;

		affectGravity();
		//cameraFollow();
		setRotationByMouse();

		updateGui();
	}

	void updateGui () {
		GameObject objGrCent = GameObject.FindGameObjectWithTag("controlSquare");
		s_UIDraw uiScript = objGrCent.GetComponent<s_UIDraw>();
		foreach(MyButton button in uiScript.myButtons){
			//button.texture = null;
			if(button.id.Equals("velocity")){
				button.text = "Speed: " + (int)(knownVelocity);
			}else if( button.id.Equals("height") ){
				
				button.text = "Height: " + (int)(transform.position.y);
			}else if( button.id.Equals("winglevel") ){
				// z axis
				button.text = "Wing Angle: " + (int)(
					transform.rotation.eulerAngles.z);

			}else if( button.id.Equals("noseangle") ){
				button.text = "Nose: " + (int)(
					transform.rotation.eulerAngles.x);
			}

			else if( button.id.Equals("wing_graphic") ){
				//button.texture = uiScript.textures[0];
			}
		}
	}


	void turnOnParticle ()
	{
		s_ParticleEmitor peComp = (s_ParticleEmitor)GetComponent("s_ParticleEmitor");
		peComp.start();
	}

	void turnOffParticle ()
	{
		s_ParticleEmitor peComp = (s_ParticleEmitor)GetComponent("s_ParticleEmitor");
		peComp.stop();
	}

	void runToTarget ()
	{

	}


	


	void settleRigidBody ()
	{
		Vector3 pos = rigidbody.position;
		pos.y = yPos;
		rigidbody.position = pos;
	}
	
	public float baseSpeed = 10f;
	public float dragAdd = 1f;
	public float yPos = 1;
	public bool isRunning = false;
	public float tagRadius = 0.2f;
	public float knownVelocity = 0;
	public float maxForwdVelocity = 20;
	public float rotationSpeed = 1.2f;
//	public s_BaseEnum.AtBase runTarget = s_BaseEnum.AtBase.Home;
//	public s_BaseEnum.AtBase runnerLocation = s_BaseEnum.AtBase.Home;

	void moveForward () {
		rigidbody.drag = 0;
		isRunning = true;
		applyForce(rigidbody.transform.forward, baseSpeed);
		applyLift();
	}

	void moveBack () {
		rigidbody.drag = 0;
		isRunning = true;
		applyForce(-rigidbody.transform.forward, baseSpeed);
		
	}

	void liftUp () {
		rigidbody.drag = 0;
		isRunning = true;
		applyForce(rigidbody.transform.up, baseSpeed);
		
	}

	void liftDown () {
		rigidbody.drag = 0;
		isRunning = true;
		applyForce(-rigidbody.transform.up, baseSpeed);
		
	}

	void applyDrag () {
		if(!isRunning && rigidbody.velocity.normalized != Vector3.zero){
			rigidbody.drag = dragAdd;
		}
	}

	void clampVelocity () {
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxForwdVelocity);
	}

	float tiltAngle = 0;
	float yawAngle = 0;
	float pitchAngle = 0;
	void rotateLeft () {
//		Quaternion rot = rigidbody.transform.rotation;
//		rot.eulerAngles = getRotation() + rotationLeft();
		tiltAngle += 5;
//		rigidbody.transform.rotation =  Quaternion.AngleAxis(tiltAngle, Vector3.forward);
	}

	void rotateRight () {
//		Quaternion rot = rigidbody.transform.rotation;
//		rot.eulerAngles = getRotation() + rotationRight();
//
//		rigidbody.transform.rotation = rot;
		
		tiltAngle += -5;
//		rigidbody.transform.rotation =  Quaternion.AngleAxis(tiltAngle, Vector3.forward);
	}

	Vector3 getRotation () {
		return rigidbody.transform.rotation.eulerAngles;
	}

	Vector3 rotationRight () {
		return (-rigidbody.transform.forward) * rotationSpeed;
	}

	Vector3 rotationLeft () {
//		transform.rotation = Quaternion.AngleAxis(30, Vector3.up);
		return (rigidbody.transform.forward) * rotationSpeed;
	}

	void cameraFollow () {
//		GameObject mc = GameObject.FindGameObjectWithTag("MainCamera");
//
//		Quaternion rot = rigidbody.transform.rotation;
//		rot.eulerAngles = transform.rotation.eulerAngles + (new Vector3(18,0,0) );
//		mc.transform.rotation = rot;
//
//		mc.transform.position = transform.position - (rigidbody.transform.forward * 10f) + (new Vector3(0,5,0) );

	}

	float x = 0.0f;
	float y = 0.0f;

	float distance = 10.0f;
	
	float xSpeed = 100.0f;
	float ySpeed = 120.0f;
	
	float yMinLimit = -360; //-20;
	float yMaxLimit = 360; //80;

	float mouseSpeed = 0.08f;


	void applyLift(){
		float liftVel = (rigidbody.velocity.magnitude * 0.034f)
			+ (Mathf.Abs(tiltAngle) * 0.005f);
		applyForce(rigidbody.transform.up, liftVel);
	}

	void affectGravity(){
//		float liftVel = -0.25f;
//		applyForce(Vector3.up, liftVel);
	}

	void setRotationByMouse(){
		float xdiff = Input.GetAxis("Mouse X") * xSpeed * mouseSpeed;
		float ydiff = Input.GetAxis("Mouse Y") * ySpeed * mouseSpeed;
		x += xdiff;
		y -= ydiff;
		
		y = ClampAngle(y, yMinLimit, yMaxLimit);




//		float dt = sensitivity * Time.deltaTime;
//		float dx = Input.GetAxis("Mouse Y") * dt;
//		float dy = -Input.GetAxis("Mouse X") * dt;
//		float dz = Input.GetAxis("Horizontal") * dt;
//		transform.Rotate(dx, dy, dz);
		yawAngle += xdiff;
		pitchAngle -= ydiff;
		pitchAngle = ClampAngle(pitchAngle, yMinLimit, yMaxLimit);
		tiltAngle = ClampAngle(tiltAngle, -360, 360);

//        Quaternion rbRot = rigidbody.transform.rotation;
//		rbRot = Quaternion.Euler(pitchAngle,
//		                         yawAngle, 
//		                         tiltAngle);
								// y diff  pitch
								// x diff  yaw
		// OBJECT
		rigidbody.transform.Rotate( -ydiff, xdiff, tiltAngle);
		tiltAngle = 0;

		// CAMERA
		Quaternion rotation = rigidbody.transform.rotation;
		Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + rigidbody.transform.position;
		
		GameObject mc = GameObject.FindGameObjectWithTag("MainCamera");
		mc.transform.rotation = rotation;
		mc.transform.position = position;
		 
//		rigidbody.transform.rotation = rbRot;
	}
	
	float  ClampAngle (float angle, float min, float max ) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}

	void moveToHome ()
	{
		isRunning = true;
//		runTarget = s_BaseEnum.AtBase.Home;
		runToTarget();
	}

	void applyForce (Vector3 direction, float amount)
	{
		Vector3 forceVec = direction * amount;
		
		rigidbody.AddForce(forceVec, ForceMode.Impulse);
	}


	

	void refreshBaseStrings ()
	{

	}

}
