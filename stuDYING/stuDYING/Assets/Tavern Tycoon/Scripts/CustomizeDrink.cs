using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class CustomizeDrink : MonoBehaviour
{
    public PersistentScript persistentScript;
    public Transform popupMenu;

    public enum SupplyType {/*Water,*/ Barley, Yeast, Hops, Price };
    public SupplyType supplyType;

    public bool increase = true;

    public AudioClip successClip;
    public AudioClip errorClip;

    public int priceMax = 100;




    // Use this for initialization
    void Start()
    {
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
        popupMenu = GameObject.FindGameObjectWithTag("Customize Popup").GetComponent<Transform>();
    }
    //void Update()
    // Update is called once per frame
    void OnMouseDown()
    {
        if (persistentScript.playerDrink1 > 0)
        {
            
            popupMenu.transform.position = new Vector3(40, -20, 0);
        }
        else
        {

            if (supplyType == SupplyType.Barley)  //------------------------ Barley ------------------------//
            {
                if (increase)
                {
                    if (persistentScript.playerDrink1WaterCost > 0)
                    {
                        persistentScript.playerDrink1WaterCost -= 1;
                        persistentScript.playerDrink1BarleyCost += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }

                }
                else
                {
                    if (persistentScript.playerDrink1BarleyCost > 0)
                    {
                        persistentScript.playerDrink1BarleyCost -= 1;
                        persistentScript.playerDrink1WaterCost += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                }

            }
            else if (supplyType == SupplyType.Yeast)  //------------------------ Yeast ------------------------//
            {
                if (increase)
                {
                    if (persistentScript.playerDrink1WaterCost > 0)
                    {
                        persistentScript.playerDrink1WaterCost -= 1;
                        persistentScript.playerDrink1YeastCost += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }

                }
                else
                {
                    if (persistentScript.playerDrink1YeastCost > 0)
                    {
                        persistentScript.playerDrink1YeastCost -= 1;
                        persistentScript.playerDrink1WaterCost += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                }
            }
            else if (supplyType == SupplyType.Hops)  //------------------------ Hops ------------------------//
            {
                if (increase)
                {
                    if (persistentScript.playerDrink1WaterCost > 0)
                    {
                        persistentScript.playerDrink1WaterCost -= 1;
                        persistentScript.playerDrink1HopsCost += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }

                }
                else
                {
                    if (persistentScript.playerDrink1HopsCost > 0)
                    {
                        persistentScript.playerDrink1HopsCost -= 1;
                        persistentScript.playerDrink1WaterCost += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                }
            }
            else if (supplyType == SupplyType.Price)  //------------------------ Price ------------------------//
            {
                if (increase)
                {
                    if (persistentScript.playerDrink1Price < priceMax)
                    {
                        persistentScript.playerDrink1Price += 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }

                }
                else
                {
                    if (persistentScript.playerDrink1Price > 0)
                    {
                        persistentScript.playerDrink1Price -= 1;

                        this.GetComponent<AudioSource>().clip = successClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        this.GetComponent<AudioSource>().clip = errorClip;
                        this.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
    }


}