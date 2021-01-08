using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    private float _damage = 10f;
    [SerializeField]
    public SphereCollider _sc;
    Player player;
    [SerializeField]
    AttackSpawn attackSpawner;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        attackSpawner = FindObjectOfType<AttackSpawn>();
        _direction = attackSpawner.transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        //Lock movement on the Z axis to 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = new Quaternion(0, 0, 0, 0);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("orb collided");
        if (collision.transform.CompareTag("Enemy"))
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool destroy = false;
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "Walls":
                destroy = true;
                break;

            case "Enemy":
                Enemy enemy = other.transform.GetComponent<Enemy>();
                enemy.TakeDamage(_damage);
                destroy = true;
                break;

            case "Player":
                destroy = false;
                break;

            default:
                destroy = false;
                break;
        }

        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }
}
