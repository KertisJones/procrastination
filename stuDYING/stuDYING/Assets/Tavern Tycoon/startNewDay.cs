using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startNewDay : MonoBehaviour {

    public PersistentScript persistentScript;
    private CustomerInteractionMaster customerInterationMaster;
    public Camera cam;

    public int nextTrack = 0;
    public float transitionDelay = 1;


    // Use this for initialization
    void Start () {
        customerInterationMaster = GameObject.FindGameObjectWithTag("Customer Interface").GetComponent<CustomerInteractionMaster>();
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        StartCoroutine(nextScene());
    }

    public IEnumerator nextScene()
    {
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);

        //fade the music out
        GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().triggerMusic = true;
        GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().currentTrack = nextTrack;
        GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().fadeTime = transitionDelay;

        //persistentScript.isDaytime = true;
        customerInterationMaster.isCustomerMenuOpen = false;

        

        yield return new WaitForSeconds(transitionDelay);
        cam.transform.position = new Vector3(0, 0, -10);

        fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(-1);
        //SceneManager.LoadScene(nextLevel);
    }
}
