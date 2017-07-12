using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannonv2 : MonoBehaviour {
	/* ********************************** */
	public GameObject objCanion;
	public Rigidbody2D bullet;
	public GameObject objExplosion;	
	public float velocity;
	private float minAngle;
	private float maxAngle;
	private char sense;	
	private float tmrLoadBullet;
	public float erryBullet = 80;
	public float speedBullet;
	public float velBullet;
	private bool allOkay;
	/* ********************************** */
	public GameObject TrajectoryPointPrefeb;
	public GameObject BallPrefb;

	public Color colorLaser = Color.red;
	private GameObject ball;
	private bool isPressed, isBallThrown;
	//private float power = 1;
	public int numOfTrajectoryPoints = 10;
	private List<GameObject> trajectoryPoints;	
	private LineRenderer lr;
	private Vector2 velTempo;

	GameObject getCanionData; /*  Variable solo para acceder a los datos del cañon, para que el lase siempre se dibuje...*/

	public AudioHandler audioHandler;
	void Start () {

		getCanionData = new GameObject ();
		getCanionData.gameObject.AddComponent<Rigidbody2D> ();

		gameObject.GetComponent<LineRenderer> ().SetColors (Color.clear, colorLaser);


		/* ********************************** */
		minAngle = 0;
		maxAngle = 45;
		this.allOkay = true;
		this.sense = 'L';
		this.tmrLoadBullet = 0.0f;		
		if (this.velBullet <= 0){
			this.velBullet = 1;
		}
		if (this.speedBullet <= 0){
			this.speedBullet = 1f;
		}		
		if (this.velocity <= 0)
			this.velocity = .3f;		
		if (sense == 'L'){
			this.velocity *= 1;
		}
		else if (sense == 'R'){
			this.velocity *= -1;
		}
		this.minAngle = maxAngle*2;
		this.minAngle -= 1;
		/* ********************************** */
		lr = GetComponent<LineRenderer> ();		
		trajectoryPoints = new List<GameObject>();
		isPressed = isBallThrown =false;
		for(int i=0;i<numOfTrajectoryPoints;i++)
		{
			GameObject dot= (GameObject) Instantiate(TrajectoryPointPrefeb);
			dot.GetComponent<Renderer>().enabled = false;
			trajectoryPoints.Insert(i,dot);
		}
		/* ********************************** */
	}
	
	public void generateNewBullet(GameObject objCanion, float velBullet){		
		/*Rigidbody2D newBullet = (Rigidbody2D)Instantiate (this.bullet,objCanion.transform.position, objCanion.transform.rotation);		
		newBullet.rigidbody2D.AddForce(new Vector2(1,1));
		newBullet.velocity = objCanion.transform.right;
		newBullet.velocity *= 5.5f;
		newBullet.velocity *= -1;
		newBullet.velocity *= velBullet;*/
	}
	public void checkLimitsRotation(){
		switch(sense){
		case 'L':
			if ( this.objCanion.transform.localEulerAngles.z < maxAngle){
				this.sense = 'R';
				this.velocity *= -1;
			}
			break;
		case 'R':
			if ( this.objCanion.transform.localEulerAngles.z > minAngle){
				this.sense = 'L';
				this.velocity *= -1;
			}
			break;
		default:
			break;			
		}
	}
	public void rotateCanion(bool canRotate){
		return;
		this.checkLimitsRotation ();
		if (canRotate)
			objCanion.transform.Rotate (Vector3.back, velocity);
	}	
	public void loadBullet(){
		if (tmrLoadBullet >= erryBullet){
			tmrLoadBullet=0;
			throwBullet();
		}
		tmrLoadBullet += 1;
	}
	public void throwBullet(){
		//generateNewBullet(this.objCanion, velBullet);
		createBall ();
		throwBall ();
	}

	void Update () {
		this.isPressed = true;
		if(isPressed){
			setTrajectoryPoints(objCanion.transform.position,getBallVelocity());
		}
		/* ********************************** */
		if (!allOkay)
			return;
		loadBullet ();
		
		/* Stop canion before throwing a new bullet */
		/*if (tmrLoadBullet > erryBullet - (erryBullet*.1))
			rotateCanion (false);
		else
			rotateCanion (true);*/
		rotateCanion (true);
		/* ********************************** */
		if(isBallThrown)
			return;
		/*if(Input.GetMouseButtonDown(0))
		{
			isPressed = true;
			if(!ball)
				createBall();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isPressed = false;
			if(!isBallThrown)
			{
				throwBall();
			}
		}*/
	}
	//---------------------------------------	
	// When ball is thrown, it will create new ball
	//---------------------------------------	
	private void createBall()
	{
		ball = (GameObject) Instantiate(BallPrefb);
		Vector3 pos = transform.position;
		pos.z=1;
		ball.transform.position = pos;
		ball.SetActive(false);
	}
	//---------------------------------------	
	private void throwBall()
	{
		ball.SetActive (true);
		ball.transform.position = objCanion.transform.position;
		ball.transform.rotation = objCanion.transform.rotation;

		ball.GetComponent<Rigidbody2D>().velocity = objCanion.transform.right;
		ball.GetComponent<Rigidbody2D>().AddForce (new Vector2 (1, 1));
		ball.GetComponent<Rigidbody2D>().velocity *= 5.5f;
		ball.GetComponent<Rigidbody2D>().velocity *= -1;
		ball.GetComponent<Rigidbody2D>().velocity *= velBullet;
		ball.GetComponent<Rigidbody2D>().gravityScale = 1;
		audioHandler.InstanceCanonBallSound ();

	}

	private Vector2 getBallVelocity(){

		getCanionData.transform.position = objCanion.transform.position;
		getCanionData.transform.rotation = objCanion.transform.rotation;

		getCanionData.GetComponent<Rigidbody2D>().velocity = objCanion.transform.right;
		getCanionData.GetComponent<Rigidbody2D>().AddForce (new Vector2(1,1));
		getCanionData.GetComponent<Rigidbody2D>().velocity *= 5.5f;
		getCanionData.GetComponent<Rigidbody2D>().velocity *= -1;
		getCanionData.GetComponent<Rigidbody2D>().velocity *= velBullet;
		getCanionData.GetComponent<Rigidbody2D>().gravityScale = 1;
		return getCanionData.GetComponent<Rigidbody2D>().velocity;
	}
	//---------------------------------------	
	private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
	{
		return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y))*1;//*ball.rigidbody.mass;
	}
	//---------------------------------------	
	// It displays projectile trajectory path
	//---------------------------------------	
	void setTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity )
	{
		float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
		float angle = Mathf.Rad2Deg*(Mathf.Atan2(pVelocity.y , pVelocity.x));
		float fTime = 0;
		
		fTime += 0.1f;
		
		lr.SetVertexCount (numOfTrajectoryPoints);

		for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
		{
			float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
			Vector3 pos = new Vector3(pStartPosition.x + dx , pStartPosition.y + dy ,2);
			trajectoryPoints[i].transform.position = pos;
			trajectoryPoints[i].GetComponent<Renderer>().enabled = false;
			trajectoryPoints[i].transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude)*fTime,pVelocity.x)*Mathf.Rad2Deg);
			fTime += 0.1f;
			
			lr.SetPosition(i,pos);

		}
	}

}
