using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class Tap : MonoBehaviour
{
    //TAP LEFT: PLAY A RHYTHM

    private float timer;
    public float shortClick;
    public TagGameManager gm;
    private string patternName = "";
    private float patternLength;

    public Boolean oneTimeUse = false;

    public Sprite shortClickSprite;
    public Sprite longClickSprite;

    public AudioClip failureClip;

    private AudioClip sound;
    private AudioSource audiosource;

    [Serializable]
    public struct PatternList
    {
        public string name; //"short long short"
        public AudioClip clip; //"loop1.mp3"
        public AudioClip secondClip; //Optional; "navi_happy.mp3"
    }

    //This is what you input in the inspector
    public PatternList[] patterns;

    //do not change in inspector; only for other scripts use
    public List<AudioClip> rhythms = new List<AudioClip>();

    private int rhythmnum = 0;

    private bool mouseBtnDwn = false;

    private Boolean foundPattern = false;

    // Use this for initialization
    void Start()
    {
        patternLength = 0;

        audiosource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

        if (mouseBtnDwn)
        {
            timer += Time.deltaTime;
        }

    }

    void OnMouseDown()
    {
        mouseBtnDwn = true;
    }

    void OnMouseUp()
    {
        mouseBtnDwn = false;

        patternLength += 1;
        if (timer <= shortClick)
        {
            patternName += "short";

            this.GetComponent<SpriteRenderer>().sprite = shortClickSprite;


            if (patternLength == gm.patternLimit)
            {
                for (int i = 0; i < patterns.Length; i++)
                {
                    if (patterns[i].name == patternName)
                    {
                        rhythms.Add(patterns[i].clip);
                        sound = patterns[i].clip;

                        audiosource.clip = sound;
                        audiosource.Play();

                        if (patterns[i].secondClip != null)
                        {
                            gm.GetComponent<AudioSource>().clip = patterns[i].secondClip;
                            gm.GetComponent<AudioSource>().Play();
                        }
                    
                        if(oneTimeUse)
                        {
                            this.GetComponent<BoxCollider2D>().enabled = false;
                            this.GetComponent<AudioSource>().loop = true;
                            gm.GetComponent<AudioSource>().loop = false;
                            GameObject.Find("_GM").GetComponent<GoToNextLevel>().triggerNextLevel = true;
                        }

                        foundPattern = true;
                    }

                }
                if (!foundPattern)
                {
                    sound = failureClip;

                    audiosource.clip = sound;
                    audiosource.Play();

                    foundPattern = false;
                }

                //Debug.Log("SOUND: " + sound);
                patternName = "";
                patternLength = 0;

            }
            else
            {
                patternName += " ";
                sound = null;
            }

        }


        else if (timer > shortClick)
        {
            patternName += "long";


            this.GetComponent<SpriteRenderer>().sprite = longClickSprite;



            if (patternLength == gm.patternLimit)
            {
                for (int i = 0; i < patterns.Length; i++)
                {
                    if (patterns[i].name == patternName)
                    {
                        rhythms.Add(patterns[i].clip);
                        sound = patterns[i].clip;

                        audiosource.clip = sound;
                        audiosource.Play();

                        foundPattern = true;

                        if (patterns[i].secondClip != null)
                        {
                            gm.GetComponent<AudioSource>().clip = patterns[i].secondClip;
                            gm.GetComponent<AudioSource>().Play();
                        }

                        if (oneTimeUse)
                        {
                            this.GetComponent<BoxCollider2D>().enabled = false;
                            this.GetComponent<AudioSource>().loop = true;
                            gm.GetComponent<AudioSource>().loop = false;
                            GameObject.Find("_GM").GetComponent<GoToNextLevel>().triggerNextLevel = true;
                        }
                    }
                    if (!foundPattern)
                    {
                        sound = failureClip;

                        audiosource.clip = sound;
                        audiosource.Play();

                        foundPattern = false;
                    }

                }


                //Debug.Log("SOUND: " + sound);
                patternName = "";
                patternLength = 0;

            }
            else
            {
                patternName += " ";
                sound = null;
            }


        }
        timer = 0;
    }


    // -------------------- wat -----------------------
    /*
    public void PlayBackRhythm()
    {
        if (rhythms.Count != 0)
        {
            rhythmSource.clip = rhythms[rhythmnum];
            rhythmSource.Play();
            Invoke("PlayBackRhythm", rhythmSource.clip.length);
            if (rhythmnum != rhythms.Count)
            {
                rhythmnum += 1;
            }
            else
            {
                rhythmnum = 0;
                CancelInvoke("PlayBackRhythm");
            }
        }

    }


    public void PlayBackMelody()
    {
        if (melodies.Count != 0)
        {
            melodySource.clip = melodies[melodynum];
            melodySource.Play();
            Invoke("PlayBackMelody", melodySource.clip.length);
            if (melodynum != melodies.Count)
            {
                melodynum += 1;
            }
            else
            {
                melodynum = 0;
                CancelInvoke("PlayBackMelody");
            }
        }

    }
    */

}