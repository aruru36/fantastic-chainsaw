using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float movementSpeed;
    Animation anim;
    public Vector3 Move;
    public float MoveSpeed = 1;
    private Vector3 StartPos;
    private float MoveTime;
    public GameObject player;
    public float damage = 5;
    public bool isDamaging;

    private bool triggeringPlayer;
    public bool aggro;

    private bool attacked;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animation>();
        BossIdle();
    }

    void BossIdle()
    {
        MoveTime += Time.deltaTime;
        transform.position = StartPos + Move * Mathf.Sin(MoveTime * MoveSpeed);
        anim.CrossFade("Idle");
    }

    void Update()
    {
        if (anim.IsPlaying("Hit"))
        {
            return;
        }
        //PMR is touching enemy collider
        if (aggro)
        {
            BossFollowPlayer();
            float hitDistance = Vector3.Distance(transform.position, player.transform.position);
            if (hitDistance <= 3f)
            {
                transform.LookAt(player.transform);
                BossAttack();
            }
        }
    }

    public void BossFollowPlayer()
    {
        //player is running from enemy
        if (!triggeringPlayer)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);
            this.transform.LookAt(player.transform);
            anim.CrossFade("Run");
        }
    }

    public void BossAttack()
    {
        //player is attacking enemy
        if (!attacked)
        {
            attacked = true;
            print("Boss is Attacking");
        }
        anim.CrossFade("skill1");

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringPlayer = true;
            other.SendMessage((isDamaging) ? "TakeDamage" : "HealDamage", damage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringPlayer = false;
        }
    }

}
