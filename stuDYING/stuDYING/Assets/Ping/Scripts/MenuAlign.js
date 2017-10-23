#pragma strict
var mainCam : Camera;
var TxtPong : Transform;

function Start () {
	TxtPong.position.x = mainCam.pixelWidth - (mainCam.pixelWidth / 10);
	TxtPong.position.y = mainCam.pixelHeight - (mainCam.pixelHeight / 3);
}