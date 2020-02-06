using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    public float movementSpeed;
    Animation anim;

    public float attackTimer;
    private float currentAttackTimer;

    //player
    public GameObject playerMovePoint;
    private Transform pmr;
    private bool triggeringPMR;
    private bool moving;
    private bool attacking;
    private float damage;
    

    //enemy
    public bool triggeringEnemy;
    private GameObject attackingEnemy;
    private bool followingEnemy;
    public bool attacked;

    private Rigidbody rb;
    //functions

    void Start()
    {
        pmr = Instantiate(playerMovePoint.transform, this.transform.position, Quaternion.identity);
        pmr.GetComponent<BoxCollider>().enabled = false;
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        currentAttackTimer = attackTimer;
    }

    void Update()
    {
        //player movement
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float hitDistance = 0.0f;

        //player movement towards enemy OR when there is no enemy
        if (playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 mousePosition = ray.GetPoint(hitDistance);

            if (Input.GetMouseButtonDown(0))
            {
                triggeringEnemy = false;
                moving = true;
                triggeringPMR = false;
                pmr.transform.position = mousePosition;
                pmr.GetComponent<BoxCollider>().enabled = true;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Enemy")                    
                        {
                        attackingEnemy = hit.collider.gameObject;
                        followingEnemy = true;
                        if (attackingEnemy.GetComponent<Enemy>() != null)
                        {
                            attackingEnemy.GetComponent<Enemy>().aggro = true;
                        }
                        if (attackingEnemy.GetComponent<EnemyBoss>() != null)
                        {
                            attackingEnemy.GetComponent<EnemyBoss>().aggro = true;
                        }
                        attacking = true;
                    }
                    if (hit.collider.tag == "Boss")
                    {
                        attackingEnemy = hit.collider.gameObject;
                        followingEnemy = true;
                        if (attackingEnemy.GetComponent<Enemy>() != null)
                        {
                            attackingEnemy.GetComponent<Enemy>().aggro = true;
                        }
                        if (attackingEnemy.GetComponent<EnemyBoss>() != null)
                        {
                            attackingEnemy.GetComponent<EnemyBoss>().aggro = true;
                        }
                        attacking = true;
                    }
                    else
                    {
                        attackingEnemy = null;
                        followingEnemy = false;
                        attacking = false;
                    }
                }
            }
            //on button click, player is moving OR attacking OR idle
            if (moving)
                Move();
            else
            {
                if (attacking)
                    Attack();
                else
                    Idle();
            }

            // if colliding with boxPMR movement is stopped
            if (triggeringPMR)
            {
                moving = false;
            }

            // if colliding with unit tagged "Enemy" player is attacking
            if (triggeringEnemy)
                Attack();

            // if attacking then attack timer decrease by 1 per real time second
            if (attacked)
            {
                currentAttackTimer = -1 * Time.deltaTime;
            }

            //if attack timer become zero, attack is stopped 
            if (currentAttackTimer <= 0)
            {
                currentAttackTimer = attackTimer;
                attacked = false;
            }
        }
    }

    //playing animation "idle"
    public void Idle()
    {
        anim.CrossFade("idle");
    }

    // if moving and no enemy in sight then move normally
    public void Move()
    {
        if (followingEnemy)
        {
			var moveEnemy = attackingEnemy.transform.position - transform.position;
            this.transform.LookAt(attackingEnemy.transform);
            rb.velocity = moveEnemy;
            //rb.MovePosition(Vector3.MoveTowards(transform.position, attackingEnemy.transform.position, movementSpeed));
            
        }
        else
        {
            //rb.MovePosition(Vector3.MoveTowards(transform.position, pmr.transform.position, movementSpeed));
            
            var move = pmr.transform.position - transform.position;
            if (Vector3.Distance(transform.position, pmr.transform.position) >= 0.1f)
            {
                this.transform.LookAt(pmr.transform);
            }
            if (move.magnitude >= 15)
            {
                move = move.normalized * 15;
            }

            rb.velocity = move.normalized * 2 + move / 2;
        }
        //playing animation "run"
        anim.CrossFade("run");
    }

    //rb.velocity

    // attacking while deal random damage to enemy
    public void Attack()
    {
        if (!attacked)
        {
            attacked = true;
        }
        if (attackingEnemy)
        {
            transform.LookAt(attackingEnemy.transform);
            if (attackingEnemy.GetComponent<Enemy>() != null)
            {
                attackingEnemy.GetComponent<Enemy>().aggro = true;
            }
            if (attackingEnemy.GetComponent<EnemyBoss>() != null)
            {
                attackingEnemy.GetComponent<EnemyBoss>().aggro = true;
            }
        }
        //playing animation "attack"
        anim.CrossFade("attack");        
    }
    

    //colliding with PMR
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PMR")
        {
            triggeringPMR = true;
        }

        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyHealth>().Health > 0.0f)
            {
                triggeringEnemy = true;
            }
        }
        if (other.tag == "Boss")
        {
            if (other.GetComponent<EnemyHealth>().Health > 0.0f)
            {
                triggeringEnemy = true;
            }
        }
    }

    //when not colliding with enemy
    //when not colliding with PMR
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PMR")
        {
            triggeringPMR = false;
        }
    }
}


