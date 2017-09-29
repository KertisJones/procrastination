using System.Collections;
using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {


		private DeathCountManager DeathCountManager;
		
		void Awake ()
		{
			DeathCountManager = GetComponent<DeathCountManager> ();
			
		}



        private void OnTriggerEnter2D(Collider2D other)
        {


            if (other.tag == "Player")
            {
				//GetComponent<AudioSource>().Play();



				Vector3 temp = other.transform.position;



				temp.x = -8f;
				temp.y = 1.8f;

				other.transform.position = temp; 




				DeathCountManager.deaths += .5;
				//DeathCountManager.deaths = Math.Ceiling (DeathCountManager.deaths);

				//other.transform.position.x = -9;
				//other.transform.position.x = 1.8;
                //Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }
}
