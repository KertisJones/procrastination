using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentTimer : MonoBehaviour {

    public static PersistentTimer PersistentTimerInstance;
    public Camera camMain;
    //public Camera camTimer;

    public float studyLength = 600; //in seconds
    public float timeLeft;
    //public int totalDays = 0;
    public bool isStudyTime = true;

    public GameObject disableOnCompleteObj;

    public string sceneToLoad;

    public int minLeft = 0;
    public int secLeft = 0;
    public string timerStr = "xx:xx";

    void Awake()
    {
        if (PersistentTimerInstance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            PersistentTimerInstance = this;
        }
    }

    // Use this for initialization
    void Start () {
        timeLeft = studyLength;
    }
	
	// Update is called once per frame
	void Update () {
        //camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (isStudyTime)
        {
            timeLeft -= Time.deltaTime;
            
            if (timeLeft <= 0)
            {
                SceneManager.LoadScene(sceneToLoad);
                //totalDays += 1;
                timeLeft = 0;
                isStudyTime = false;

                //StartCoroutine(nextScene());
            }

            minLeft = Mathf.FloorToInt(timeLeft / 60);
            secLeft = Mathf.FloorToInt(timeLeft % 60);

            if (secLeft >= 10)
            {
                timerStr = minLeft.ToString() + ":" + secLeft.ToString();
            }
            else
            {
                timerStr = minLeft.ToString() + ":0" + secLeft.ToString();
            }


            this.GetComponentInChildren<UnityEngine.UI.Text>().text = timerStr;

        }
        else
        {
            disableOnCompleteObj.SetActive(false);
        }

        if (Input.GetKeyDown("f1") || Input.GetKeyDown("f2"))
        {
            disableOnCompleteObj.SetActive(false);
        }
    }


}
