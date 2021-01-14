using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MovementController mc;
    [SerializeField] private EnemyBrain brain;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] public GameObject weapon;                   //projectile spawn point
    [SerializeField] public  EnemyStats stats;

    public float health;
    public float meleeDamage;
    public float moveSpeed;
    public float jumpForce;
    public float attackRange;
    public bool chasing;

    [SerializeField] public Transform[] waypoints;
    public int nextWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        mc = GetComponent<MovementController>();

        health = stats.health;
        meleeDamage = stats.meleeDamage;
        moveSpeed = stats.moveSpeed;
        jumpForce = stats.jumpForce;
        attackRange = stats.attackRange;
        chasing = false;

        mc.SetSpeed(moveSpeed);
        nextWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        brain.Think(this);

        //Lock movement on the Z axis to 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        attack.PerformAttack(this);
    }

    public void Move(Transform waypoint)
    {
        Vector3 destination = waypoint.position;
        destination.z = 0;
        mc.MoveTo(destination);
    }

    public void Jump()
    {
        mc.Jump(jumpForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(meleeDamage);

        }
    }
}
