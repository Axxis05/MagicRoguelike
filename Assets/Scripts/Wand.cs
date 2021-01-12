using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wand")]
public class Wand : ScriptableObject
{
    public new string name;
    public string description;
    public float damage;

    public Ability lAttack;
    public Ability hAttack;
    public Ability uAttack;

    public void UseLAttack(Player player, GameObject obj)
    {
        lAttack.Trigger(player, obj);
    }

    public void UseHAttack(Player player, GameObject obj)
    {
        hAttack.Trigger(player, obj);
    }

    public void UseUAttack(Player player, GameObject obj)
    {
        uAttack.Trigger(player, obj);
    }

    public void SetAbilityDefaults()
    {
        if(lAttack != null)
            lAttack.SetDefaults();
        if (hAttack != null)
            hAttack.SetDefaults();
        if (uAttack != null)
            uAttack.SetDefaults();
    }
}
