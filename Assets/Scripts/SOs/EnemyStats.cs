using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public float health;
    public float meleeDamage;
    public float moveSpeed;
    public float jumpForce;
    public float attackRange = 2f; //default value is melee range
}
