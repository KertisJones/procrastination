using UnityEngine;
using System.Collections;


/// <summary>
/// General class for all Power Ups
/// </summary>
public class PowerUp : MonoBehaviour
{ 
	public float timeActive;
    [SerializeField]
    public enum PowerUpTypes
{
    Basic,
    Attack,
    Mine,
    Shield,
    Slime,
    RepairKit,
    Speed
};

    public PowerUpTypes powerUpType;

	void OnTriggerEnter2D (Collider2D other){
		if (other.transform.root.GetComponent<PowerUpCoordinator> ().currentPup == null) {
			other.transform.root.GetComponent<PowerUpCoordinator> ().CollectPup (gameObject);
			Destroy (gameObject);
		}
	}

}
