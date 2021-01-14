using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Patrol AI")]
public class PatrolBrain : EnemyBrain
{
    public override void Think(Enemy enemy)
    {
        if (enemy.chasing)
        {

        }
        else
        {
            if (enemy.mc.myNavMeshAgent.remainingDistance <= enemy.mc.myNavMeshAgent.stoppingDistance && !enemy.mc.myNavMeshAgent.pathPending)
            {
                enemy.nextWaypoint = (enemy.nextWaypoint + 1) % enemy.waypoints.Length;
            }

            enemy.Move(enemy.waypoints[enemy.nextWaypoint]);
        }
    }
}
