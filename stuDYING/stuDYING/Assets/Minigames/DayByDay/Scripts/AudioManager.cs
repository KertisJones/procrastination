using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	public GameObject gameManager;
	public Player player;
	public AudioSource SoundEffectManager;
	public AudioClip dayAmbient;
	public AudioClip acceptSale;
	public AudioClip declineSale;

	public AudioClip nightAmbient;
	public AudioClip woodCollectProcess;
	public AudioClip woodCollectSuccess;
	public AudioClip berriesCollectProcess;
	public AudioClip berriesCollectSuccess;
	public AudioClip coalCollectProcess;
	public AudioClip coalCollectSuccess;


	public void Awake(){
		//DontDestroyOnLoad (this);
	}

	public void Start(){
		if (SceneManager.GetActiveScene ().name == "Day" || SceneManager.GetActiveScene ().name == "StarterScene")
			this.GetComponent<AudioSource> ().clip = dayAmbient;
		else {
			this.GetComponent<AudioSource> ().clip = nightAmbient;
			this.GetComponent<AudioSource> ().Stop ();
		}
		this.GetComponent<AudioSource> ().Play ();
	}

	public void ChangeToDay(){
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().clip = dayAmbient;
		this.GetComponent<AudioSource> ().volume = 1.0f;
		this.GetComponent<AudioSource> ().Play ();
	}

	public void ChangeToNight(){
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().clip = nightAmbient;
		this.GetComponent<AudioSource> ().volume = 0.375f;
		this.GetComponent<AudioSource> ().Play ();
	}

	public void AcceptSale(){
		SoundEffectManager.clip = acceptSale;
		SoundEffectManager.Play ();
	}

	public void DeclineSale(){
		SoundEffectManager.clip = declineSale;
		SoundEffectManager.Play ();
	}

	public void WoodCollectProcess(){
		if (Time.timeScale != 0) {
			SoundEffectManager.clip = woodCollectProcess;
			SoundEffectManager.loop = true;
			SoundEffectManager.volume = 1.0f;
			SoundEffectManager.Play ();
		}
	}

	public void WoodCollectSuccess(){
		//SoundEffectManager.Stop ();
		if (Time.timeScale != 0) {
			SoundEffectManager.clip = woodCollectSuccess;
			SoundEffectManager.loop = false;
			SoundEffectManager.volume = 1.0f;
			SoundEffectManager.Play ();
		}
	}

	public void BerriesCollectProcess(){
		if (Time.timeScale != 0) {
			SoundEffectManager.clip = berriesCollectProcess;
			SoundEffectManager.loop = true;
			SoundEffectManager.volume = 0.6f;
			SoundEffectManager.Play ();
		}
	}

	public void BerriesCollectSuccess(){
		//SoundEffectManager.Stop ();
		if (Time.timeScale != 0) {
			SoundEffectManager.clip = berriesCollectSuccess;
			SoundEffectManager.loop = false;
			SoundEffectManager.volume = 0.6f;
			SoundEffectManager.Play ();
		}
	}

	public void CoalCollectProcess(){
		if (Time.timeScale != 0) {
			SoundEffectManager.clip = coalCollectProcess;
			SoundEffectManager.loop = true;
			SoundEffectManager.volume = 1.0f;
			SoundEffectManager.Play ();
		}
	}

	public void CoalCollectSuccess(){
		//SoundEffectManager.Stop ();
		if (Time.timeScale != 0) {
			SoundEffectManager.clip = coalCollectSuccess;
			SoundEffectManager.loop = false;
			SoundEffectManager.volume = 1.0f;
			SoundEffectManager.Play ();
		}
	}
}
