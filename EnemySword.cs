using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public float damage = 3;
    public bool isDamaging;
    public GameObject ehit;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(ehit,transform.position,transform.rotation);
            other.SendMessage((isDamaging) ? "TakeDamage" : "HealDamage", damage);
        }
    }
}
