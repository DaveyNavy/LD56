using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip  damageSoundClip;
    [SerializeField] private AudioClip  deathSoundClip;
    private int health;
    private int damageToFood;
    private int maxHealth;
    private int score;
    public Enemy(int health, int damageToFood, int score)
    {
        this.health = health;
        this.maxHealth = health;
        this.damageToFood = damageToFood;
        this.score = score;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    public void Kill()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseUp()
    {
        TakeDamage(1); 
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(damageSoundClip, transform.position, 1f);
        }
        if (health <= 0)
        {
            Kill();
            ScoreManager.AddScore(score);
            AudioSource.PlayClipAtPoint(deathSoundClip, transform.position, 1f);
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
