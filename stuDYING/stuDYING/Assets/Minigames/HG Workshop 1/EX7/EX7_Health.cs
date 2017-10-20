using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX7_Health : MonoBehaviour
{
    // The object's max health
    public int max = 10;
    // The object's current health
    public int current;
    
    // Awake is called as soon as the object is loaded, before anything else happens
    private void Awake()
    {
        // If current is unset (0), set it to the maxHealth
        if (current == 0) current = max;
    }

    // Damage the object by a certain amount
    public void Damage(int amount)
    {
        /* Implement a function that will modify the current health value according to the input amount
         * Note:
         *   The Die function should be called once the current health falls to (or below) 0
         */
    }

    // What to do once the object should die
    private void Die()
    {
        // Disable this components parent GameObject
        gameObject.SetActive(false);
    }
}
