using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip  damageSoundClip;
    private int health;
    private int damageToFood;
    private int maxHealth;
    public Enemy(int health, int damageToFood)
    {
        this.health = health;
        maxHealth = health;
        this.damageToFood = damageToFood;
    }
    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        TakeDamage(1);
        AudioSource.PlayClipAtPoint(damageSoundClip, transform.position, 1f); 
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log(health);
            Kill();
        }
    }

    public int GetDamageToFood()
    {
        return damageToFood;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
