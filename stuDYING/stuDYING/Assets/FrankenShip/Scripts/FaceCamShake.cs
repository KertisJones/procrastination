using UnityEngine;
using System.Collections;

public class FaceCamShake : MonoBehaviour {

	public Vector2 faceStartPoint;
	public Vector2 randPoint;
	public bool shakeFaceCam;
	public float shakeDuration = 2.0f;
	public float shakeDivide;
	float currentTime01;
	public int division;
	Vector2 myStartPos;

	// Use this for initialization
	void Start () {
		myStartPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeFaceCam) {
			shakeDivide = shakeDuration / division;
			if (currentTime01 < shakeDivide) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / shakeDivide;
				transform.localPosition = Vector3.Lerp (faceStartPoint, randPoint, lerp);
			} else {
				currentTime01 = 0.0f;
				faceStartPoint = transform.localPosition;
				randPoint = myStartPos + Random.insideUnitCircle * 10.0f;
			}
		}
	}

	public IEnumerator FaceShakeDuration(){
		faceStartPoint = transform.localPosition;
		randPoint = myStartPos + Random.insideUnitCircle * 10.0f;
		shakeFaceCam = true;
		yield return new WaitForSeconds (shakeDuration);
		transform.localPosition = myStartPos;
		shakeFaceCam = false;
	}
}
