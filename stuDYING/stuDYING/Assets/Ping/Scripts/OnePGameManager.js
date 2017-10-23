#pragma strict
static var startTime : int;
static var HiTime : String = "00:00:000";
static var textTime : String;

static var Himinutes : int = 0;
static var Hiseconds : int = 0;
static var Hifraction : int = 0;

var theSkin : GUISkin;
//added this member variable here so we can access it through other scripts

function Awake() {

	startTime = Time.time;

}

static function reset () {

	var guiTime = Time.time - startTime;

	var minutes : int = guiTime / 60;
	var seconds : int = guiTime % 60;
	var fraction : int = (guiTime * 100) % 100;
	
	if (minutes >= Himinutes) {
		Himinutes = minutes;
		if (seconds >= Hiseconds) {
			Hiseconds = seconds;
			if (fraction >= Hifraction) {
				Hifraction = fraction;
			}
		}
	}

	startTime = Time.time;
	
	Debug.Log(Hifraction);
		Debug.Log(Hiseconds);
			Debug.Log(Himinutes);
	HiTime = String.Format("{0:00}:{1:00}:{2:000}", Himinutes, Hiseconds, Hifraction);


	
}

function OnGUI () {

	var guiTime = Time.time - startTime;

	var minutes : int = guiTime / 60;
	var seconds : int = guiTime % 60;
	var fraction : int = (guiTime * 100) % 100;

	GUI.skin = theSkin;

	textTime = String.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
	GUI.Label (Rect (Screen.width/4, 25, 1000, 300), textTime);
	GUI.Label (Rect (Screen.width/1.5, 25, 1000, 300), HiTime);
	//changed variable name to textTime -->text is not a good variable name since it has other use already

}