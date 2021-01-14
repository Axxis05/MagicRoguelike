using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : ScriptableObject
{
    public bool isRanged;
    public Animation attackAnimation;
    public float animationDelay;
    public GameObject projectile;
    private bool attacking = false;

    public void PerformAttack(Enemy attacker)
    {
        if (!attacking)
        {
            attacker.StartCoroutine(PerformAttack(attacker.weapon.transform.position));
        }
    }

    IEnumerator PerformAttack(Vector3 spawnPosition)
    {
        if (attackAnimation != null) 
        {
            attackAnimation.Play();
            attacking = true;
        }
        yield return new WaitForSeconds(animationDelay);
        Instantiate(projectile, spawnPosition, Quaternion.identity);
        if (!attackAnimation.isPlaying)
        {
            attacking = false;
        }
    }
}
