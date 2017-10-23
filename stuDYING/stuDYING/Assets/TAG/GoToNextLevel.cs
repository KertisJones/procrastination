using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour {

    public bool triggerNextLevel = false;
    public float transitionDelay = 1.0f;
    public string nextLevel = "Sanctuary";
    //public int nextTrack = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(nextScene());
        }
    }

    void Update()
    {
        if (triggerNextLevel)
            StartCoroutine(nextScene());
    }

    public IEnumerator nextScene()
    {
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);

        //fade the music out
        //GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().triggerMusic = true;
        //GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().currentTrack = nextTrack;
        //GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().fadeTime = transitionDelay;

        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(nextLevel);
    }

}
