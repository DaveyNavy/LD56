using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private int damageToFood;
    public Enemy(int health, int damageToFood)
    {
        this.health = health;
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
}
