using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    protected float nextCast;
    public string aName;
    public string aDescription;
    public float cooldownTimer;


    public abstract void Trigger(Player player, GameObject obj);
    public abstract void SetDefaults();
}
