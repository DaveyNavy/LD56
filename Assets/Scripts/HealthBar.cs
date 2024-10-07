using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Slider healthBar;
    [SerializeField] Food food;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = food.GetHealth();
        healthBar.value = food.GetMaxHealth();
    }

    private void Update()
    {
        healthBar.value = food.GetHealth();
    }
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
