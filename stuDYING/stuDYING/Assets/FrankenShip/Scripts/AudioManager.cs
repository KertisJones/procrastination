using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    //variables list
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public static AudioManager instance = null; //allows other scripts to call functions from AudioManager
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
	public AudioClip clip01;
	public AudioClip clip02;

    void Awake()
    {
        //check for instance of AudioManager
        if (instance == null)
            //if not, set to this
            instance = this;
        else if (instance != this)
            //destroy this; there can only be one instance of AudioManager
            Destroy(gameObject);

        //protects AudioManager during loading sequences
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        //sets the clip of the sfxSource to the clip passed in as a parameter
        sfxSource.clip = clip;

        //plays clip
        sfxSource.Play();
    }

    public void SFXRandomizer(params AudioClip[] clips)
    {
        //creates a random number between 0 and the length of the array
        int randomIndex = Random.Range(0, clips.Length);

        //chooses a random pitch between high and low ranges
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //set the clip to the clip from random index
        sfxSource.clip = clips[randomIndex];

        //set the clip to have pitch from random generator
        sfxSource.pitch = randomPitch;

        //plays clip
        sfxSource.Play();
    }
}