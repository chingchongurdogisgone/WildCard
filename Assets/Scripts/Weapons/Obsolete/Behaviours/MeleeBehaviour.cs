using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("This will be replaced by the Aura class.")]
// Base script of all Melee Weapon behaviours
// Not Placed anywhere

public class MeleeBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;

    // Current Melee Stats
    protected float currentDamage;
    protected float currentArea;
    protected float currentDuration;
    //protected float currentCooldown;
    protected float currentKnockback;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentArea = weaponData.Area;
        currentDuration = weaponData.Duration;
        //currentCooldown = weaponData.Cooldown;
        currentKnockback = weaponData.Knockback;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position, currentKnockback); // Use GetCurrentDamage() since multiplier might be applied
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage();
            }
        }
    }
}
