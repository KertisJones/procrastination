﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;


public class Player : MovingObject {

	public int wallDamage = 1;
    public int enemyDamage = 2;
    public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
    public int pickUpAx = 0;
	public float restartLevelDelay = 1f;
	public Text foodText;
    public Text powerText;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;
    public GameObject throwingAxe;

    private Animator animator;
	private int food;
	private Vector2 touchOrigin = -Vector2.one;

    private void Awake()
    {
        if (GameObject.Find("GameManager(Clone)") != null && GameObject.Find("GameManager(Clone)").GetComponent<GameManager>().level != 1)
            GameObject.Find("GameManager(Clone)").GetComponent<GameManager>().InitGame();
    }

    // Use this for initialization
    protected override void Start () {
		animator = GetComponent<Animator> ();

		food = GameManager.instance.playerFoodPoints;

		foodText.text = "Food: " + food;

        

        powerText.text = "Throwing Ax: " + pickUpAx;


        base.Start ();
	}

	private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = food;
	}

	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.playersTurn)
			return;

		int horizontal = 0;
		int vertical = 0;

        // Player is throwing axe
        if (true) //if (Input.GetKey(KeyCode.LeftShift)) I switched off needing to use shift, here's the commented out code. -Jesse
        {
            // Which direction?
            if (Input.GetKeyDown(KeyCode.W))
                throwAx("up");
            else if (Input.GetKeyDown(KeyCode.A))
                throwAx("left");
            else if (Input.GetKeyDown(KeyCode.S))
                throwAx("down");
            else if (Input.GetKeyDown(KeyCode.D))
                throwAx("right");
        }


#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			var posVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var x = Mathf.RoundToInt(posVec.x) - transform.position.x;
			var y = Mathf.RoundToInt(posVec.y) - transform.position.y;
			if (Mathf.Abs(x) > Mathf.Abs(y)) {
				horizontal = x > 0 ? 1 : -1;
			} else {
				vertical = y > 0 ? 1 : -1;
			}
		}

		if (horizontal != 0)
			vertical = 0;

#else
		if (Input.touchCount > 0)
		{
			Touch myTouch = Input.touches[0];

			if (myTouch.phase == TouchPhase.Began)
			{
				touchOrigin = myTouch.position;
			} else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				Vector2 touchEnd = myTouch.position;
				float x = touchEnd.x - touchOrigin.x;
				float y = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1;
				if (Mathf.Abs(x) > Mathf.Abs(y))
					horizontal = x > 0 ? 1 : -1;
				else
					vertical = y > 0 ? 1 : -1;
			}
		}

#endif

		if (horizontal != 0 || vertical != 0)
			AttemptMove<Wall> (horizontal, vertical);
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		food--;
		foodText.text = "Food: " + food;

		base.AttemptMove <T> (xDir, yDir);

		RaycastHit2D hit;
		if (Move (xDir, yDir, out hit)) {
			SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
		}

		CheckIfGameOver ();

		GameManager.instance.playersTurn = false;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + " Food: " + food;
			SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
			other.gameObject.SetActive (false);
		} else if (other.tag == "Soda") {
			food += pointsPerSoda;
			foodText.text = "+" + pointsPerSoda + " Food: " + food;
			SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
			other.gameObject.SetActive (false);
		} else if (other.tag == "Ax") {
            wallDamage += 1;
            enemyDamage += 1;
            pickUpAx++;
            powerText.text = " Throwing Ax: " + pickUpAx;
            other.gameObject.SetActive(false);
        }else if (other.tag == "Thorns")
        {
            LoseFood(1);
        }
	}

	protected override void OnCantMove <T> (T component)
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("playerChop");
        }
    protected override void EnemyAttack<T>(T component)
    {
        
        Enemy baddie = component as Enemy;
        animator.SetTrigger("playerChop");
        baddie.TakeDamage(enemyDamage);

        {
            LoseFood(5);
        }

    }
    private void Restart()
	{
        GameObject.Find("GameManager(Clone)").GetComponent<GameManager>().level += 1;

        Application.LoadLevel (Application.loadedLevel);
	}

	public void LoseFood (int loss)
	{
		animator.SetTrigger ("playerHit");
		food -= loss;
		foodText.text = "-" + loss + " Food: " + food;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver()
	{
		if (food <= 0) {
			SoundManager.instance.PlaySingle(gameOverSound);
			SoundManager.instance.musicSource.Stop();
			GameManager.instance.GameOver ();
		}
	}

    protected override void EnemyHitWall<T>(T component)
    {
        throw new NotImplementedException();
    }

    //Handles the throw-axe functionality
    public void throwAx(String direction)
    {
        //If we have axes to throw
        if (this.pickUpAx > 0) {
            Debug.Log("Throw!");

            //Decrease our axe supply
            this.pickUpAx--;
            powerText.text = " Throwing Ax: " + pickUpAx; 

            //Gets the starting and end positions to perform the raycast from
            Vector3 startPos;
            Vector3 end;
            Vector3 thrownAxeSpeed;

            if (direction == "up")
            {
                startPos = new Vector3(this.transform.position.x, this.transform.position.y+1, 0f);
                end = new Vector3(this.transform.position.x, 8f, 0f);
                thrownAxeSpeed = new Vector3(0, 1, 0);
            }
            else if(direction == "left")
            {
                startPos = new Vector3(this.transform.position.x-1, this.transform.position.y, 0f);
                end = new Vector3(0f, transform.position.y, 0f);
                thrownAxeSpeed = new Vector3(-1, 0, 0);
            }
            else if (direction == "down")
            {
                startPos = new Vector3(this.transform.position.x, this.transform.position.y - 1, 0f);
                end = new Vector3(this.transform.position.x, 0f, 0f);
                thrownAxeSpeed = new Vector3(0, -1, 0);
            }
            else
            {
                startPos = new Vector3(this.transform.position.x+1, this.transform.position.y, 0f);
                end = new Vector3(8f, this.transform.position.y, 0f);
                thrownAxeSpeed = new Vector3(1, 0, 0);
            }

            GameObject thrownAxe = Instantiate(throwingAxe, startPos, Quaternion.identity);
            thrownAxe.GetComponent<MoveObject>().speed = thrownAxeSpeed;
            thrownAxe.GetComponent<MoveObject>().destination = end;

            //Performs the cast and, if we hit an enemy unit, destroys it
            RaycastHit2D hit = Physics2D.Linecast(startPos, end, blockingLayer);
            print(hit.collider.gameObject.name);
            if(hit.collider.gameObject.name.Contains("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(10);
                thrownAxe.GetComponent<MoveObject>().destination = hit.collider.gameObject.transform.position;
            }
        }
    }


}
