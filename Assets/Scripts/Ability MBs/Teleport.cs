using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private float _damage;
    private Player _player;
    private float _duration = 0.1f;
    private float _spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _damage = _player.equippedWand.damage;
        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _spawnTime > _duration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.transform.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
        }
    }
}
