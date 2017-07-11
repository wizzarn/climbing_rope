using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	public GameObject objExplosion;
	public float timeToExplode;
	private bool canDestroy;
	private int tmr;
	public GameObject objLight;

	public int dmgBullet;
	void Start () {
		//objLight.GetComponent<Light>().color = getRangomColor ();
		dmgBullet = 10;
	}
	void Update () {
	}

	IEnumerator  OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "main_character" || col.gameObject.tag == "wooden_bar") {
			GameObject explo2 = (GameObject)Instantiate (objExplosion,this.transform.position, this.transform.rotation);
			//GameObject.Find("monobot").GetComponent<MonoBotController>().getHurt(dmgBullet);
			//HealthBarController.Instance.decraseHealth(dmgBullet);
			Destroy (this.gameObject);
		}else{
			yield return new WaitForSeconds(Random.Range(2, 5));
			GameObject explo = (GameObject)Instantiate (objExplosion,this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
		}
	}
	Color getRangomColor(){
		return new Color(Random.value, Random.value, Random.value);
	}
}
