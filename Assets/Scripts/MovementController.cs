using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    public NavMeshAgent myNavMeshAgent;
    private Rigidbody myRigidbody;
    private Enemy myEnemy;

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(float newSpeed)
    {
        myNavMeshAgent.speed = newSpeed;
    }
    public void MoveTo(Vector3 destination)
    {
        myNavMeshAgent.SetDestination(destination);
    }

    public void Jump(float jumpForce)
    {
        Vector3 jumpVector = new Vector3(0f, jumpForce, 0f);
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0, 0);
        myRigidbody.AddForce(jumpVector, ForceMode.Impulse);
    }
}
