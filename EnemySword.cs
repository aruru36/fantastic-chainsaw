using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public float damage = 3;
    public bool isDamaging;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.SendMessage((isDamaging) ? "TakeDamage" : "HealDamage", damage);
    }
}
