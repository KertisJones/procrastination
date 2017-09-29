using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CustomerInteractionMaster : MonoBehaviour {

    public CustomerInteractionMove moveOpen;
    public CustomerInteractionMove moveClose;

    public bool isCustomerMenuOpen = false;

    public bool soldDrinkTrigger = false;
    //public bool newCustomerTrigger = false;

    public float customerWaitTimeBaseline = 10;
    private float customerWaitTime = 10;
    private float waitTimeTick = 0;
    private bool triggerCustomer = false;
	// Use this for initialization
	void Start () {
        StartCoroutine("FindCustomer");
        customerWaitTime = customerWaitTimeBaseline;
    }
	
	// Update is called once per frame
	void Update () {
        if (!triggerCustomer)
        {
            waitTimeTick -= Time.deltaTime;
            if (waitTimeTick < 0)
            {
                customerWaitTime = Random.Range((customerWaitTimeBaseline * .2f), (customerWaitTimeBaseline * 1.5f));
                waitTimeTick = customerWaitTime;

                triggerCustomer = true;
            }
        }

        if (soldDrinkTrigger)
        {
            triggerCustomer = false;
            isCustomerMenuOpen = false;
            soldDrinkTrigger = false;
            //newCustomerTrigger = false;
            moveOpen.triggerMovement = false;
            moveClose.triggerMovement = true;
        }

    }

    IEnumerator FindCustomer()
    {
        for (;;)
        {
            if (!isCustomerMenuOpen && triggerCustomer)
            {
                isCustomerMenuOpen = true;
                //newCustomerTrigger = true;
                moveOpen.triggerMovement = true;
                moveClose.triggerMovement = false;
                this.GetComponent<AudioSource>().Play();
            }

            yield return new WaitForSeconds(.1f);
        }
    }

}
