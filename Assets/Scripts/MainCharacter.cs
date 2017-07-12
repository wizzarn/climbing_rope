using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {

	string tagColliderName = "rope_child";
	private float oldGravityScale = 0;
	public bool isJumping = false;
	float yVelocity = 1f;
	Vector2 jumpForce = new Vector2(6,0);
	Animator anim;
	public bool isMoving = false;
	public bool isClimbing= true;
	public LayerMask ropeMask;
	public LayerMask obstacleMask;
	public CurrentSide currentSide;
	public float minSwipeDistY = 200;
	public float minSwipeDistX = 200;
	private Vector2 startPos;
	public AudioHandler audioHandler;
	public GameManager gameManager;

	/* Animator fields */
	public float idle = 0;
	public bool moving = false;
	public float velY = 0;
	public float velX = 0;
	public bool jumping = false;
	Rigidbody2D rigidBody;
	public enum CurrentSide{
		LEFT,
		RIGHT
	}
	public enum Actions{
		JumpLeft,
		JumpRight,
		GoUp,
		GoDown
	}
	void Start () {
		rigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
		oldGravityScale = this.GetComponent<Rigidbody2D> ().gravityScale;
		currentSide = CurrentSide.LEFT;
		anim = this.GetComponent<Animator> ();
	}

	void Update () {
		Inputs();
		UpdateRayCasts ();
		SwipeMotions ();
		if(isJumping){
		}
		UpdateAnimations ();
	}
	void SwipeMotions(){
		return;
		if (Input.touchCount > 0){
			Touch touch = Input.touches [0];
			switch(touch.phase){
			case TouchPhase.Began:
				startPos = touch.position;
				break;

			case TouchPhase.Ended:
				float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) {
					float swipeValue = Mathf.Sign (touch.position.y - startPos.y);
					if (swipeValue > 0) { // UP
						MainActions (Actions.GoUp);
					} else if (swipeValue < 0) {
						MainActions (Actions.GoDown);
					}
				}
				float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
				if (swipeDistHorizontal > 0) {
					MainActions (Actions.JumpRight);
				} else if (swipeDistHorizontal < 0) {
					MainActions (Actions.JumpLeft);
				}
				break;
			}
		}
	}
	void AnimationsLogic(){
		
		if (rigidBody.velocity.y < 0 && !isJumping) {
			velY = -1;
			moving = true;
		}
		if (rigidBody.velocity.y > 0  && !isJumping) {
			velY = 1;
			moving = true;
		}
		if (rigidBody.velocity.y == 0) {
			velY = 0;
			moving = false;
		}
		if (currentSide == CurrentSide.LEFT) {
			idle = -1;
		}
		if (currentSide == CurrentSide.RIGHT) {
			idle = 1;
		}
		jumping = isJumping;

		if (rigidBody.velocity.x < 0 && isJumping) {
			velX = -1;
		}
		if (rigidBody.velocity.x > 0 && isJumping) {
			velX = 1;
		}
	}
	void UpdateAnimations(){
		AnimationsLogic ();
		anim.SetFloat ("idle",idle);
		anim.SetFloat ("velY",velY);
		anim.SetFloat ("velX",velX);
		anim.SetBool ("moving",moving);
		anim.SetBool ("jumping",jumping);
	}
	void Jump(){
		if (currentSide == CurrentSide.LEFT) {
			this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-19, 1.5f),ForceMode2D.Force);
		} else if (currentSide == CurrentSide.RIGHT) {
			this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (19, 1.5f),ForceMode2D.Force);
		}
		Vector3 velocity = this.GetComponent<Rigidbody2D> ().velocity;
		if (velocity.y > 1.5f)
			velocity.y = 1.5f;
		this.GetComponent<Rigidbody2D> ().velocity = velocity;
	}
	void MainActions(Actions action){
		if (action == Actions.JumpLeft){
			if (currentSide == CurrentSide.RIGHT) {
				currentSide = CurrentSide.LEFT;
				return;
			} else {
				isJumping = true;
				isMoving = false;
				Jump ();
			}
		}
		else if (action == Actions.JumpRight){
			if (currentSide == CurrentSide.LEFT) {
				currentSide = CurrentSide.RIGHT;
				return;
			} else {
				isJumping = true;
				isMoving = false;
				Jump ();
			}
		}
		else if (action == Actions.GoUp){
			this.GetComponent<Rigidbody2D> ().gravityScale = oldGravityScale;
			Vector2 currentVelocity = this.GetComponent<Rigidbody2D> ().velocity;
			currentVelocity.y = yVelocity;
			this.GetComponent<Rigidbody2D> ().velocity = currentVelocity;
			isMoving = true;
		}
		else if (action == Actions.GoDown){
			this.GetComponent<Rigidbody2D> ().gravityScale = oldGravityScale;
			Vector2 currentVelocity = this.GetComponent<Rigidbody2D> ().velocity;
			currentVelocity.y = -yVelocity;
			this.GetComponent<Rigidbody2D> ().velocity = currentVelocity;
			isMoving = true;
		}
	}
	void Inputs(){
		if (isJumping)
			return;
		//Jump left
		if (Input.GetKeyUp(KeyCode.A)){
			MainActions (Actions.JumpLeft);
		}
		//Jump Right
		if (Input.GetKeyUp(KeyCode.D)){
			MainActions (Actions.JumpRight);
		}
		// Go Up
		if (Input.GetKeyDown(KeyCode.W)){
			MainActions (Actions.GoUp);
		}
		// Go Down
		if (Input.GetKeyDown(KeyCode.S)){
			MainActions (Actions.GoDown);
		}
	}
	void UpdateRayCasts(){

		// LEFT
		float ray = 0.2f;
		float lastInputX = -1;
		float bodyRayDistanceLeft = lastInputX * -ray;
		float bodyRayDistanceRight = lastInputX * ray;

		// UP
		float bodyRayDistanceUp = lastInputX * ray;

		Vector3 transformPositionLeft = this.transform.position;
		transformPositionLeft.x -= .4f;

		Vector3 transformPositionRight = this.transform.position;
		transformPositionRight.x += .4f;

		Vector3 transformPositionUp = this.transform.position;
		transformPositionUp.y += .4f;

		RaycastHit2D bodyRayInfoRight = Physics2D.Raycast (transformPositionRight, Vector2.right * lastInputX, Mathf.Abs (bodyRayDistanceRight), ropeMask);
		RaycastHit2D bodyRayInfoLeft = Physics2D.Raycast (transformPositionLeft, Vector2.right * lastInputX, Mathf.Abs (bodyRayDistanceLeft), ropeMask);

		RaycastHit2D headRayInfo = Physics2D.Raycast (transformPositionUp, Vector2.up, ray, obstacleMask); // valor de distancia fue puesto al tanteo

		if (bodyRayInfoLeft.collider != null) {
			Debug.DrawRay (transformPositionLeft, new Vector3 (bodyRayDistanceLeft, 0, 0), Color.blue);
		} else {
			Debug.DrawRay (transformPositionLeft, new Vector3 (bodyRayDistanceLeft, 0, 0), Color.red);
		}

		if (bodyRayInfoRight.collider != null) {
			Debug.DrawRay (transformPositionRight, new Vector3 (bodyRayDistanceRight, 0, 0), Color.blue);
		} else {
			Debug.DrawRay (transformPositionRight, new Vector3 (bodyRayDistanceRight, 0, 0), Color.red);
		}

		if (headRayInfo.collider != null) {
			Debug.DrawRay (transformPositionUp, new Vector3 (0, bodyRayDistanceUp, 0), Color.blue);
			Vector3 currentVelocity = this.GetComponent<Rigidbody2D> ().velocity;
			if (currentVelocity.y > 0) {
				currentVelocity.y = 0;
				this.GetComponent<Rigidbody2D> ().velocity = currentVelocity;
			}
		} else {
			Debug.DrawRay (transformPositionUp, new Vector3 (0, bodyRayDistanceUp, 0), Color.red);
		}


	}
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == tagColliderName && !isJumping) {
			this.GetComponent<Rigidbody2D> ().gravityScale = 0;
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, this.GetComponent<Rigidbody2D>().velocity.y);
			if (currentSide == CurrentSide.LEFT) {
				float xPos = other.transform.position.x;
				xPos -= .2f;
				Vector3 newPosition = this.gameObject.transform.position;
				newPosition.x = xPos;
				this.gameObject.transform.position = newPosition;
			}
			else if (currentSide == CurrentSide.RIGHT) {
				float xPos = other.transform.position.x;
				xPos += .2f;
				Vector3 newPosition = this.gameObject.transform.position;
				newPosition.x = xPos;
				this.gameObject.transform.position = newPosition;
			}
		}
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == tagColliderName) {
			this.GetComponent<Rigidbody2D> ().gravityScale = 0;
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, this.GetComponent<Rigidbody2D>().velocity.y);
			if (currentSide == CurrentSide.LEFT) {
				float xPos = other.transform.position.x;
				xPos -= .2f;
				Vector3 newPosition = this.gameObject.transform.position;
				newPosition.x = xPos;
				this.gameObject.transform.position = newPosition;
			}
			else if (currentSide == CurrentSide.RIGHT) {
				float xPos = other.transform.position.x;
				xPos += .2f;
				Vector3 newPosition = this.gameObject.transform.position;
				newPosition.x = xPos;
				this.gameObject.transform.position = newPosition;
			}
		}
		if (other.gameObject.tag == tagColliderName && isJumping) {
			isJumping = false;
			isMoving = false;
			if (currentSide == CurrentSide.LEFT)
				currentSide = CurrentSide.RIGHT;
			else if (currentSide == CurrentSide.RIGHT)
				currentSide = CurrentSide.LEFT;
		}
		if (other.gameObject.tag == "coin") {
			ScordeHandler.AddCoin (100);
			Destroy (other.gameObject);
			audioHandler.InstanceCoinSound ();

		}
		if (other.gameObject.tag == "falling_object") {
			Destroy (this.gameObject);
			gameManager.SetGameOver ();
		}
		if (other.gameObject.tag == "canon_ball") {
			Destroy (this.gameObject);
			gameManager.SetGameOver ();

		}
		if (other.gameObject.tag == "finish_level") {
			Destroy (this.gameObject);
			gameManager.SetSuccess ();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == tagColliderName) {
			this.GetComponent<Rigidbody2D> ().gravityScale = oldGravityScale;
		}
	}
}
