using UnityEngine;
using System.Collections;

public class PersistentScript : MonoBehaviour {

    private CustomerInteractionMaster customerInterationMaster;
    public Camera cam;

    public int playerGold = 0;
    public int playerBarley = 0;
    public int playerWater = 0;
    public int playerHops = 0;
    public int playerYeast = 0;
    public int playerReputation = 0;

    public int playerDrink1 = 0;
    public int playerDrink2 = 0;
    public int playerDrink3 = 0;

    public int playerDrink1WaterCost = 1;
    public int playerDrink1BarleyCost = 2;
    public int playerDrink1YeastCost = 2;
    public int playerDrink1HopsCost = 2;
    public int playerDrink1Price = 9;

    //public int playerDrink2WaterCost = 1;
    //public int playerDrink2BarleyCost = 2;
    //public int playerDrink2YeastCost = 2;
    //public int playerDrink2HopsCost = 2;
    //public int playerDrink2Price = 9;

    //public int playerDrink3WaterCost = 1;
    //public int playerDrink3BarleyCost = 2;
    //public int playerDrink3YeastCost = 2;
    //public int playerDrink3HopsCost = 2;
    //public int playerDrink3Price = 9;

    public float dayLength = 60; //in seconds
    private float timeLeft;
    public int totalDays = 0;

    public int nextTrack = 1;
    public float transitionDelay = 1;

    public bool isDaytime = true;
    public bool infWater = true;
    private void Start()
    {
        timeLeft = dayLength;
        customerInterationMaster = GameObject.FindGameObjectWithTag("Customer Interface").GetComponent<CustomerInteractionMaster>();
    }

    void Update()
    {
        if (isDaytime)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                totalDays += 1;
                timeLeft = dayLength;
                isDaytime = false;

                StartCoroutine(nextScene());
            }
        }

        if (infWater)
        {
            playerWater = 2000000000;
        }

        /*if (Input.GetKey("escape"))
        {
            Application.Quit();
        }*/
    }

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    public IEnumerator nextScene()
    {
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);

        //fade the music out
        GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().triggerMusic = true;
        GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().currentTrack = nextTrack;
        GameObject.Find("$MusicManager").gameObject.GetComponent<CrossfadeOnTrigger>().fadeTime = transitionDelay;

        customerInterationMaster.isCustomerMenuOpen = true;

        yield return new WaitForSeconds(transitionDelay);
        cam.transform.position = new Vector3(40, 0, -10);

        fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(-1);
        //SceneManager.LoadScene(nextLevel);
    }
}
