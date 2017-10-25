#pragma strict

static var playerScore01 : int = 0;
static var playerScore02 : int = 0;



var theSkin : GUISkin;

static function Score (wallName : String) {
	if (wallName == "rightWall")
		{
			playerScore01 += 1;
			//Debug.Log(playerScore01);

		}
	else
		{
			playerScore02 += 1;
		}
	if (playerScore01 >= 21) {
		ClassicGameManager.playerScore01 = 0;
		ClassicGameManager.playerScore02 = 0;
		Application.LoadLevel("PingPlayerOnewins");
	}
	else if (playerScore02 >= 21) {
		ClassicGameManager.playerScore01 = 0;
		ClassicGameManager.playerScore02 = 0;
		Application.LoadLevel("PingPlayerTwoWins");
	}
	//Debug.Log("Player Score 1 is " + playerScore01);
	//Debug.Log("Player Score 1 is " + playerScore02);
}

function OnGUI () {
	GUI.skin = theSkin;
	// the 100, 100 determines the clipping on x and y
	GUI.Label (new Rect (Screen.width/2-150, 20, 100, 100), "" + playerScore01);
	GUI.Label (new Rect (Screen.width/2+150, 20, 100, 100), "" + playerScore02);
}