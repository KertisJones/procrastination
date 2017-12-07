using UnityEngine;
using System.Collections;

public class MoveHorizontal : MonoBehaviour {

	public float mySpeed;
	public float myRotate;
	public bool left;
	public bool right;

	// Use this for initialization
	void Start () {
		StartCoroutine (Suicide ());
	}
	
	// Update is called once per frame
	void Update () {
		if (left) {
			transform.position += Vector3.left * mySpeed * Time.deltaTime;
		} else if (right) {
			transform.position += Vector3.right * mySpeed * Time.deltaTime;
		}
		transform.eulerAngles += Vector3.forward * myRotate * Time.deltaTime;
	}

	IEnumerator Suicide(){
		yield return new WaitForSeconds (150);
		Destroy (gameObject);
	}
}
