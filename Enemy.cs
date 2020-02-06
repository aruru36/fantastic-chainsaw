using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //variables
    public float movementSpeed;
    Animation anim;
    public Vector3 Move;
    public float MoveSpeed = 1;
    private Vector3 StartPos;
    private float MoveTime;

    public GameObject player;
    public GameObject target1;
    public GameObject target2;
    private int movetarget = 0;

    private bool triggeringPlayer;
    public bool aggro;

    private bool attacked;

    // function   

    void Start()
    {
        StartPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animation>();
        Idle();
    }

    void Idle()
    {
        MoveTime += Time.deltaTime;
        transform.position = StartPos + Move * Mathf.Sin(MoveTime * MoveSpeed);
        anim.CrossFade("EnemyIdle");

    }
    void Update()
    {

        //PMR is touching enemy collider

        if (aggro)
        {
            FollowPlayer();
            float hitDistance = Vector3.Distance(transform.position, player.transform.position);
            if (hitDistance <= 1.3f)
            {
                transform.LookAt(player.transform);
                Attack();
            }
        }
        else
        {
            if (movetarget == 0)
            {
                //gerak ke target a
                this.transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, movementSpeed);
                this.transform.LookAt(target1.transform);
                float hitDistance = Vector3.Distance(transform.position, target1.transform.position);
                if (hitDistance <= 0.3f)
                {
                    movetarget = 1;
                }
            }
            else
            {
                //gerak ke target b
                this.transform.position = Vector3.MoveTowards(transform.position, target2.transform.position, movementSpeed);
                this.transform.LookAt(target2.transform);
                float hitDistance = Vector3.Distance(transform.position, target2.transform.position);
                if (hitDistance <= 0.3f)
                {
                    movetarget = 0;
                }
            }
            anim.CrossFade("Enemy Walk");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringPlayer = true;
            transform.LookAt(player.transform);
            Attack();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringPlayer = false;
        }
    }

    public void Attack()
    {
        //player is attacking enemy
        if (!attacked)
        {
            attacked = true;
            print("Attacked");
        }
        anim.CrossFade("EnemyAttack");
    }

    public void FollowPlayer()
    {
        //player is running from enemy
        if (!triggeringPlayer)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);
            this.transform.LookAt(player.transform);
            anim.CrossFade("Enemy Walk");
        }
    }
}
