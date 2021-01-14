using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teleport Ability")]
public class TeleportAbility : Ability
{
    public float teleportDistance = 5.0f;

    public GameObject abilityEffect;

    public override void SetDefaults()
    {
        Debug.Log("Setting default values for " + aName);
        nextCast = -1f;
        Debug.Log("nextCast for " + aName + " set to " + nextCast.ToString());
    }

    public override void Trigger(Player player, GameObject obj)
    {
        Debug.Log(Time.time + " = time, " + nextCast + " = next teleport.");
        if(Time.time > nextCast)
        {
            Debug.Log("Teleporting.");
            int direction = 0;
            Vector3 endLocation = player.transform.position;

            nextCast = Time.time + cooldownTimer;

            if (Input.GetAxis("Horizontal") >= 0)
            {
                direction = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                direction = -1;
            }
            else
            {
                Debug.LogError("Input.GetAxis('Horizontal' is undefined.");

            }

            float newX = player.transform.position.x + (teleportDistance * direction);
            endLocation.x = newX;

            player.transform.position = endLocation;

            if (abilityEffect != null)
            {
                Instantiate(abilityEffect, player.transform.position, Quaternion.identity);
            }
        }
        
    }
}
