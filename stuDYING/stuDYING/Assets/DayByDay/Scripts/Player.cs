using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public GameObject camera;
	public AudioManager audioManager;
	public GameObject player;
	private SectionManager section;

	public static int LumberSupply = 10;
	public static int BerrySupply = 10;
	public static int CoalSupply = 10;
	public static int MoneySupply = 0;
	public float hForce = 100f;
	public float maxSpeed = 3f;

	public static int LumberSold = 0;
	public static int BerrySold = 0;
	public static int CoalSold = 0;
	public static int MoneyMade = 0;
	public static int CustomersServed = 0;

	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		player = GameObject.Find ("Player");
		section = player.GetComponent<SectionManager> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name != "Day") {
//			//Application.LoadLevel("_Scenes/Day");
//			section.next_left_checkpoint = -11.5f;
//			section.next_right_checkpoint = 11.5f;
//			section.left_adjustment = -34.5f;
//			section.right_adjustment = 34.5f;
//			var position = this.transform.position;
//			this.transform.position = new Vector3 (0, position.y, position.z);
//			//audioManager.ChangeToDay ();
//		}
	}

	void FixedUpdate()
	{
		if (SceneManager.GetActiveScene ().name == "Workshop") {
			float h = Input.GetAxis ("Horizontal");
			//Debug.Log (h);
			rb2d.velocity = new Vector2 (h * maxSpeed, rb2d.velocity.y);
		} else {
			this.transform.position = new Vector3 (0, 3.55f, -1);
			rb2d.velocity = new Vector2 (0f, 0f);
		}
	}

	void Awake(){
		//DontDestroyOnLoad (this);
		//DontDestroyOnLoad (camera);
	}
}
