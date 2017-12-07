using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleFieldsParallax : MonoBehaviour {

	[SerializeField]
	public enum FieldTypes
	{
		Ice,
		Asteroid,
		Junk
	};
	public bool spawnBothSides;
	public FieldTypes fieldType;
	public GameObject fieldObject;
	public Color iceMinColor;
	public Color iceMaxColor;
	public List <Sprite> iceSprites;
	public float iceMinSpeed;
	public float iceMaxSpeed;
	public float iceMinSize;
	public float iceMaxSize;
	public float iceMinRotate;
	public float iceMaxRotate;
	public float iceMinDelay;
	public float iceMaxDelay;
	public Color asteroidMinColor;
	public Color asteroidMaxColor;
	public List <Sprite> asteroidSprites;
	public float asteroidMinSpeed;
	public float asteroidMaxSpeed;
	public float asteroidMinSize;
	public float asteroidMaxSize;
	public float asteroidMinRotate;
	public float asteroidMaxRotate;
	public float asteroidMinDelay;
	public float asteroidMaxDelay;
	public Color junkMinColor;
	public Color junkMaxColor;
	public List <Sprite> junkSprites;
	public float junkMinSpeed;
	public float junkMaxSpeed;
	public float junkMinSize;
	public float junkMaxSize;
	public float junkMinRotate;
	public float junkMaxRotate;
	public float junkMinDelay;
	public float junkMaxDelay;
	BoxCollider2D myColl;
	Sprite spawnSprite;
	Color spawnColor;
	Vector3 spawnPos;
	float spawnSpeed;
	Vector3 spawnSize;
	float spawnRotate;
	float spawnDelay;
	bool timerStarted;

	// Use this for initialization
	void Start () {
		myColl = gameObject.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!timerStarted) {
			GetAttributes ();
			StartCoroutine (SpawnFieldObject ());
		}
	}

	IEnumerator SpawnFieldObject(){
		timerStarted = true;

		if (spawnBothSides && Random.value > 0.5f) {
			spawnPos = new Vector3 (spawnPos.x * -1, spawnPos.y, spawnPos.z);
		}

		GameObject spawnObject = (GameObject) Instantiate (fieldObject, spawnPos, transform.rotation);
		SpriteRenderer objectSprite = spawnObject.GetComponent<SpriteRenderer> ();
		objectSprite.sprite = spawnSprite;
		objectSprite.color = spawnColor;
		MoveHorizontal moveScript = spawnObject.GetComponent<MoveHorizontal> ();
		moveScript.mySpeed = spawnSpeed;
		moveScript.right = true;
		moveScript.myRotate = spawnRotate;

		if (objectSprite.transform.position.x > 0) {
			moveScript.left = true;
		}

		spawnObject.transform.localScale = spawnSize;
		spawnObject.transform.SetParent (transform);

		yield return new WaitForSeconds (spawnDelay);

		timerStarted = false;
	}

	public void GetAttributes(){
		spawnPos = new Vector3 (transform.position.x + myColl.offset.x, transform.position.y + Random.Range (-myColl.size.y/2, myColl.size.y/2), 0);
		if (fieldType == FieldTypes.Ice) {
			float colorValue = Random.Range (iceMinColor.r, iceMaxColor.r);
			float transValue = Random.Range (iceMinColor.a, iceMaxColor.a);
			spawnColor = new Color (colorValue, colorValue, colorValue, transValue);
			float sizeValue = Random.Range (iceMinSize, iceMaxSize);
			spawnSprite = iceSprites [Random.Range (0, iceSprites.Count)];
			spawnSpeed = Random.Range (iceMinSpeed, iceMaxSpeed);
			spawnSize = new Vector3 (sizeValue, sizeValue, 1);
			spawnRotate = Random.Range (iceMinRotate, iceMaxRotate);
			spawnDelay = Random.Range (iceMinDelay, iceMaxDelay);
		} else if (fieldType == FieldTypes.Asteroid) {
			float sizeValue = Random.Range (asteroidMinSize, asteroidMaxSize);
			float colorValue = Random.Range (asteroidMinColor.r, asteroidMaxColor.r);
			float transValue = Random.Range (asteroidMinColor.a, asteroidMaxColor.a);
			spawnColor = new Color (colorValue, colorValue, colorValue, transValue);
			spawnSprite = asteroidSprites [Random.Range (0, asteroidSprites.Count)];
			spawnSpeed = Random.Range (asteroidMinSpeed, asteroidMaxSpeed);
			spawnSize = new Vector3 (sizeValue, sizeValue, 1);
			spawnRotate = Random.Range (asteroidMinRotate, asteroidMaxRotate);
			spawnDelay = Random.Range (asteroidMinDelay, asteroidMaxDelay);
		} else if (fieldType == FieldTypes.Junk) {
			float sizeValue = Random.Range (junkMinSize, junkMaxSize);
			float colorValue = Random.Range (junkMinColor.r, junkMaxColor.r);
			float transValue = Random.Range (junkMinColor.a, junkMaxColor.a);
			spawnColor = new Color (colorValue, colorValue, colorValue, transValue);
			spawnSprite = junkSprites [Random.Range (0, junkSprites.Count)];
			spawnSpeed = Random.Range (junkMinSpeed, junkMaxSpeed);
			spawnSize = new Vector3 (sizeValue, sizeValue, 1);
			spawnRotate = Random.Range (junkMinRotate, junkMaxRotate);
			spawnDelay = Random.Range (junkMinDelay, junkMaxDelay);
		}
	}
}
