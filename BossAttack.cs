using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float damage = 1;
    public bool isDamaging;

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
            other.SendMessage((isDamaging) ? "TakeDamage" : "HealDamage", damage);
    }
}
