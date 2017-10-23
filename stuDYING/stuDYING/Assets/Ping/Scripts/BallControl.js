#pragma strict

function Start () {
	yield WaitForSeconds (1);
		var randomNumber = Random.Range(0, 2);
		GetComponent.<Rigidbody2D>().velocity.y = 0;
		GetComponent.<Rigidbody2D>().velocity.x = 0;
		var randomXR = Random.Range(50, 60);
		var randomXL = Random.Range(-50, -60);
		var randomY = Random.Range(-50, 50);
		if (randomNumber <= 0.5) {
			//Debug.Log("Shoot right");
			GetComponent.<Rigidbody2D>().AddForce (new Vector2 (randomXR, randomY));
		}
		else {
		//Debug.Log("Shoot left");
		GetComponent.<Rigidbody2D>().AddForce (new Vector2 (randomXL, randomY));
		}
}

function OnCollisionEnter2D (colInfo : Collision2D) {
	if (colInfo.collider.tag == "Player") {
		//Debug.Log("IIITSWORKS");
		GetComponent.<AudioSource>().Play();
		
		if (GetComponent.<Rigidbody2D>().velocity.y > 10) {
			GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y/3 + colInfo.collider.GetComponent.<Rigidbody2D>().velocity.y/2;
		}
		if (GetComponent.<Rigidbody2D>().velocity.y < .5) {
			var randomNumber = Random.Range(0, 2);
			if (randomNumber <= .5) {
				GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y + colInfo.collider.GetComponent.<Rigidbody2D>().velocity.y/2;
			}
			else {
				GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y + colInfo.collider.GetComponent.<Rigidbody2D>().velocity.y/2 + 2;
			}
		}
		else {
			GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y/2 + colInfo.collider.GetComponent.<Rigidbody2D>().velocity.y/2;
		}
	}
}



function OnTriggerEnter2D (hitInfo : Collider2D) {
	if(hitInfo.name == "leftWall" || hitInfo.name == "rihtWall");
	{
		yield WaitForSeconds (1);
		var randomNumber = Random.Range(0, 2);
		GetComponent.<Rigidbody2D>().velocity.y = 0;
		GetComponent.<Rigidbody2D>().velocity.x = 0;
		var randomXR = Random.Range(50, 90);
		var randomXL = Random.Range(-50, -100);
		var randomY = Random.Range(-50, 50);
		if (randomNumber <= 0.5) {
			//Debug.Log("Shoot right");
			GetComponent.<Rigidbody2D>().AddForce (new Vector2 (randomXR, randomY));
		}
		else {
		//Debug.Log("Shoot left");
		GetComponent.<Rigidbody2D>().AddForce (new Vector2 (randomXL, randomY));
		}
	}
}