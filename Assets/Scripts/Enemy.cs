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
        Debug.Log(health);
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(damageSoundClip, transform.position, 1f);
        }
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSoundClip, transform.position, 1f);
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
