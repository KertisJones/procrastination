using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX7_EnemyController : MonoBehaviour
{
    // The target to aim at
    public Transform target;
    // The cannon to aim and fire
    public EX6_Cannon cannon;
    // The time between shots
    public float shotDelay = 1;

    private void Awake()
    {
        // InvokeRepeating(<function name>, <start delay>, <repeat delay>) will call the given function name once every x seconds after a start delay
        InvokeRepeating("Fire", 0, shotDelay);
    }

    private void Update()
    {
        if(target)
            cannon.AimAt(target.position);
    }

    private void Fire()
    {
        if(target && cannon)
            cannon.Fire();
    }
}
