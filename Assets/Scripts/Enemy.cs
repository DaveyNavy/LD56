using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 10;
    public Enemy(int health)
    {
        this.health = health;
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
}
