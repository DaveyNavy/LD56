using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private int damageToFood;
    private int maxHealth;
    private int score;
    private GameManager gameManager;
    public Enemy(int health, int damageToFood, int score)
    {
        this.health = health;
        this.maxHealth = health;
        this.damageToFood = damageToFood;
        this.score = score;
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    public void Kill()
    {
        Destroy(gameObject);
        gameManager.IncreaseScore(score);
    }

    private void OnMouseUp()
    {
        TakeDamage(1);
        if (health <= 0)
        {
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
