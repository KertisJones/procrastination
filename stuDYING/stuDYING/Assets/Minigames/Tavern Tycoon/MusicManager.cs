using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	public static MusicManager Instance;

    public AudioClip splashTrack;
    public AudioClip mainTheme;
    public AudioClip fallTrack;

    public AudioClip eggTrack1;
    public AudioClip eggTrack2;
    public AudioClip eggTrack3;
    public AudioClip eggTrack4;
    public AudioClip eggTrack5;

    public AudioClip creditsTrack;

    public int rnd;
    void Awake ()
	{
        this.GetComponent<AudioSource>().clip = mainTheme;
        
		Instance = this;
        /*
        if (SceneManager.GetActiveScene().name == "Splash")
        {
            this.GetComponent<AudioSource>().clip = splashTrack;
        }
        else if (SceneManager.GetActiveScene().name == "Demo")
        {
            this.GetComponent<AudioSource>().clip = mainTheme;
        }
        else if (SceneManager.GetActiveScene().name == "Main Level")
        {
            rnd = Mathf.FloorToInt(Random.Range(1f, 200f));
            if (rnd == 1)
            {
                this.GetComponent<AudioSource>().clip = eggTrack1;
            }
            else if (rnd == 2)
            {
                this.GetComponent<AudioSource>().clip = eggTrack2;
            }
            else if (rnd == 3)
            {
                this.GetComponent<AudioSource>().clip = eggTrack3;
            }
            else if (rnd == 4)
            {
                this.GetComponent<AudioSource>().clip = eggTrack4;
            }
            else if (rnd == 5)
            {
                this.GetComponent<AudioSource>().clip = eggTrack5;
            }
            else
            {
                this.GetComponent<AudioSource>().clip = mainTheme;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Second Half")
        {
            rnd = Mathf.FloorToInt(Random.Range(1f, 200f));
            if (rnd == 1)
            {
                this.GetComponent<AudioSource>().clip = eggTrack1;
            }
            else if (rnd == 2)
            {
                this.GetComponent<AudioSource>().clip = eggTrack2;
            }
            else if (rnd == 3)
            {
                this.GetComponent<AudioSource>().clip = eggTrack3;
            }
            else if (rnd == 4)
            {
                this.GetComponent<AudioSource>().clip = eggTrack4;
            }
            else if (rnd == 5)
            {
                this.GetComponent<AudioSource>().clip = eggTrack5;
            }
            else
            {
                this.GetComponent<AudioSource>().clip = fallTrack;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Credits")
        {
            this.GetComponent<AudioSource>().clip = creditsTrack;
            this.GetComponent<AudioSource>().loop = false;
        }
        this.GetComponent<AudioSource>().Play();
        */
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonCrossfade(AudioClip newTrack)
	{
		Crossfade(newTrack);
	}

	public static void Crossfade(AudioClip newTrack)
	{
		Instance.StopAllCoroutines();
		
		if(Instance.GetComponents<AudioSource>().Length > 1)
		{
			Destroy(Instance.GetComponent<AudioSource>());
		}
		
		AudioSource newAudioSource = Instance.gameObject.AddComponent<AudioSource>();
		
		newAudioSource.volume = 0.0f;
		
		newAudioSource.clip = newTrack;
		
		newAudioSource.Play();
		
		Instance.StartCoroutine(Instance.ActuallyCrossfade(newAudioSource,1.0f));
	}

	public static void Crossfade(AudioClip newTrack, float fadeTime)
	{
		Instance.StopAllCoroutines();

		if(Instance.GetComponents<AudioSource>().Length > 1)
		{
			Destroy(Instance.GetComponent<AudioSource>());
		}

		AudioSource newAudioSource = Instance.gameObject.AddComponent<AudioSource>();

		newAudioSource.volume = 0.0f;

		newAudioSource.clip = newTrack;

		newAudioSource.Play();

		Instance.StartCoroutine(Instance.ActuallyCrossfade(newAudioSource,fadeTime));
	}

	IEnumerator ActuallyCrossfade(AudioSource newSource, float fadeTime)
	{
		float t = 0.0f;

		float initialVolume = GetComponent<AudioSource>().volume;

		while(t < fadeTime)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp(initialVolume,0.0f,t/fadeTime);
			newSource.volume = Mathf.Lerp(0.0f,1.0f,t/fadeTime);

			t += Time.deltaTime;
			yield return null;
		}

		newSource.volume = 1.0f;

		Destroy(GetComponent<AudioSource>());
	}


}
