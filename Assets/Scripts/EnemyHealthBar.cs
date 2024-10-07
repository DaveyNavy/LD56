using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarScript : MonoBehaviour
{
    private Slider healthBar;
    [SerializeField] Enemy enemy;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = enemy.GetHealth();
        healthBar.value = enemy.GetMaxHealth();
    }

    private void Update()
    {
        healthBar.value = enemy.GetHealth();
        transform.position = enemy.transform.position + new Vector3(0, 1, 0);
    }
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }

}
