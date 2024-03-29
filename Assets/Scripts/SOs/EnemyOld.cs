﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOld : MonoBehaviour
{
    [SerializeField]
    private float _health = 50;
    private float _speed = 2.5f;
    public int horizontalDirection = 1;
    [SerializeField]
    public CapsuleCollider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //Lock movement on the Z axis to 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Movement()
    {
        CheckForWall();
        CheckForFloor();

        Vector3 direction = new Vector3(horizontalDirection, 0f, 0f);
        transform.Translate(direction * _speed * Time.deltaTime);   
    }

    void CheckForWall()
    {
        RaycastHit hit;
        float extraLength = 0.5f;
        bool raycastHit = Physics.Raycast(myCollider.bounds.center, Vector3.right * horizontalDirection, out hit, myCollider.bounds.extents.x + extraLength);
        Debug.DrawRay(myCollider.bounds.center, Vector3.right * horizontalDirection * (myCollider.bounds.extents.x + extraLength));
        Color rayColor;
        if (raycastHit && hit.transform.tag == "Environment")
        {
            rayColor = Color.green;
            horizontalDirection *= -1;
        }
        else
        {
            rayColor = Color.red;
            
        }
    }

    void CheckForFloor()
    {
        RaycastHit hit;
        float xOffset = 0.5f;
        float extraHeight = 0.3f;

        Vector3 rayCenter = myCollider.bounds.center + new Vector3(xOffset * horizontalDirection, 0, 0);
        bool raycastHit = Physics.Raycast(rayCenter, Vector3.down, out hit, myCollider.bounds.extents.x + extraHeight);
        Debug.DrawRay(rayCenter, Vector3.down * (myCollider.bounds.extents.x + extraHeight));
        Color rayColor;
        if (!raycastHit)
        {
            rayColor = Color.red;
            horizontalDirection *= -1;
        }
        else
        {
            rayColor = Color.green;
            
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
