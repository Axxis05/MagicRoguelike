using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile Ability")]
public class ProjectileAbility : Ability
{
    public GameObject projectile;

    public override void SetDefaults()
    {
        nextCast = -1f;
    }

    public override void Trigger(Player player, GameObject attackSpawner)
    {
        if(Time.time > nextCast)
        {
            //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Instantiate(projectile, attackSpawner.transform.position, Quaternion.identity);

            nextCast = Time.time + cooldownTimer;
        }
        
    }

    
}
