using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
	
	public static HealthBarController Instance;

	private float MaxLife = 100.0f;
	[Range(0, 100)]
	public float actualHealt;
	private Image imageBar;

	void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of HealthBarController!");
		}
		
		Instance = this;
	}

	void Start() {
		actualHealt = MaxLife;
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


	public void decraseHealth(float decrease) {

		decrease = Mathf.Abs (decrease);
		actualHealt -= decrease;
		actualHealt = Mathf.Max (actualHealt, 0);

		if (!imageBar)
			return;
		float pointHealth = actualHealt / MaxLife;
		this.imageBar.fillAmount = pointHealth;
	}
}
