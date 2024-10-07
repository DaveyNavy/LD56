using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Food : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private AudioClip  eatSoundClip;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            health -= enemy.GetDamageToFood();
            enemy.Kill();
            AudioSource.PlayClipAtPoint(eatSoundClip, transform.position, 1f);
            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
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
