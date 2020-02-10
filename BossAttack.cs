using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float damage = 1;
    public bool isDamaging;
    public GameObject eff;

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(eff, transform.position, transform.rotation);
            other.SendMessage((isDamaging) ? "TakeDamage" : "HealDamage", damage);
        }
            
    }
}
