using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    public GameObject wood;
    public GameObject particle;
    public GameObject explosion;
    public float damage = 4f;
    public GameObject hit;

    private void Start()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject;

            if (enemy.GetComponent<EnemyHealth>().InvTime <= 0)
            {
                enemy.GetComponent<EnemyHealth>().InvTime = 1.5f;
                enemy.GetComponent<EnemyHealth>().enemyHealth -= damage;
                Instantiate(hit, transform.position, transform.rotation);
                
                if (enemy.GetComponent<EnemyHealth>().enemyHealth <= 0.0f)
                {
                    Die();
                    Instantiate(particle,transform.position,transform.rotation);
                }
            }
        }
        if (other.tag == "Boss")
        {
            boss = other.gameObject;
            if (boss.GetComponent<EnemyHealth>().InvTime <= 0)
            {
                boss.GetComponent<EnemyHealth>().InvTime = 2.5f;
                boss.GetComponent<EnemyHealth>().enemyHealth -= damage;
                boss.GetComponent<Animation>().CrossFade("Hit");
                if (boss.GetComponent<EnemyHealth>().enemyHealth <= 15f)
                    {
                    damage = 1f;
                }
                if (boss.GetComponent<EnemyHealth>().enemyHealth <= 0.0f)
                {
                    Destroy(boss);
                    PlayerController pc = GetComponentInParent<PlayerController>();
                    pc.triggeringEnemy = false;
                    pc.attacked = false;
                    Instantiate(explosion, transform.position, transform.rotation);
                    Destroy(wood);
                    boss.GetComponent<EnemyBoss>().sound3.SetActive(false);
                }
            }
        }
    }

    public void Die()
    {
        Destroy(enemy);        
        PlayerController pc = GetComponentInParent<PlayerController>();
        pc.triggeringEnemy = false;
        pc.attacked = false;
    }

}
