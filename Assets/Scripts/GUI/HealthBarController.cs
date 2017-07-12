using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
	
	public static HealthBarController Instance;

	private float MaxLife = 100.0f;
	[Range(0, 100)]
	public float actualHealt;
	private Image imageBar;
	GameObject icon;
	void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of HealthBarController!");
		}
		icon = GameObject.Find ("ProgressIcon");
		Instance = this;
	}

	void Start() {
		actualHealt = 0;
		imageBar = this.GetComponent<Image> ();
	}
 
	void Update () {
		if (!imageBar)
			return;
		float pointHealth = actualHealt / MaxLife;
		this.imageBar.fillAmount = pointHealth;
	}
	
	public void setMaxLife(float newMaxLife) {
		this.MaxLife = Mathf.Abs (newMaxLife);
		this.actualHealt = this.actualHealt < MaxLife ? MaxLife : actualHealt;
		actualHealt = Mathf.Max (actualHealt, 0);

		if (!imageBar)
			return;
		float pointHealth = actualHealt / MaxLife;
		this.imageBar.fillAmount = pointHealth;
	}


	public void increaseHealth(float increase) {

		increase = Mathf.Abs (increase);
		actualHealt += increase;
		actualHealt = Mathf.Max (actualHealt, 0);

		if (!imageBar)
			return;
		float pointHealth = actualHealt / MaxLife;
		this.imageBar.fillAmount = pointHealth;
	}
	public void setHealth(float points) {
		actualHealt = points;
		actualHealt = Mathf.Max (actualHealt, 0);

		if (!imageBar)
			return;
		float pointHealth = actualHealt / MaxLife;
		this.imageBar.fillAmount = pointHealth;
		if (icon && points < 100)
			icon.transform.Translate (0, .024f, 0);
	}
}
