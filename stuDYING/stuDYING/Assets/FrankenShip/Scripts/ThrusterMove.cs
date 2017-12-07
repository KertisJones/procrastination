
using UnityEngine;
using System.Collections;
using System.Linq;

public class ThrusterMove : MonoBehaviour {

	float vertAxe;
	float hoAxe;
	float lastVertAxe;
	Rigidbody2D shipBody;
	Transform parentPart;
	Vector3 forceDir;
	public float thrustPower;
	public float maxThrustPower;
	public float coolDownTime;
	public bool timerStarted;
	ParticleSystem[] thrustParticles;
	public InAudioNode thrustEnd;
	public InAudioNode thrusterSequence;

	// Use this for initialization
	void Start () {
		shipBody = transform.root.GetComponent<Rigidbody2D> ();
   		thrustParticles = gameObject.GetComponentsInChildren<ParticleSystem> ();
        if (transform.parent.tag != "IGOR")
        {
            parentPart = gameObject.GetComponentInChildren<SliderJoint2D>().connectedBody.transform.parent.transform;
        }
        else {
            parentPart = transform.parent;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.root.tag == "Player01")
        {
            vertAxe = ControlManager.P1LVert;
            hoAxe = ControlManager.P1RHriz;
        }
        else
        {
            vertAxe = ControlManager.P2LVert;
            hoAxe = ControlManager.P2RHriz;
        }
        forceDir = parentPart.position - transform.position;
		if (vertAxe != 0) {
			foreach (ParticleSystem system in thrustParticles){
				system.enableEmission = true;
			}
			if (lastVertAxe == 0) {
				InAudio.Play(gameObject, thrusterSequence, null);
			}
		} else {
			foreach (ParticleSystem system in thrustParticles){
				system.enableEmission = false;
			}
			if (lastVertAxe != 0) {
				InAudio.Stop(gameObject, thrusterSequence, 0.5f);
				InAudio.Play(gameObject, thrustEnd, null);
			}
		}
		lastVertAxe = vertAxe;
	}

	void FixedUpdate(){
		shipBody.AddForceAtPosition (forceDir * vertAxe * thrustPower, parentPart.position);
	}
}
