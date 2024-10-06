using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Enemy
{
    [SerializeField] private float speed = 0;
    private float baseSpeed;
    [SerializeField] private Vector3 position;
    float angle;
    public Ant() : base(1, 1, 5) { }

    private void Awake()
    {
        speed = 0.5f;
        baseSpeed = speed;
        float distance = Vector2.Distance(transform.position, position);
        Vector2 direction = position - transform.position;
        direction.Normalize();
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
        speed = baseSpeed + 0.1f * (GameManager.instance.GetScore() / 100);
    }
}