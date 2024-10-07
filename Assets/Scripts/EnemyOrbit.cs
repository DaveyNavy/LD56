using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbit : Enemy
{
    public float speed;
    public float radius;
    public float waitTime;
    public float higherSpeed;
    public Vector3 position;

    private float timer;
    private Rigidbody2D rb;

    EnemyOrbit() : base(1, 2, 20) { }

    void Awake()
    {
        timer = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 distance = transform.position - position;
        Vector3 velocity = -distance;
        if (distance.magnitude <= radius && timer <= waitTime) {
            timer += Time.deltaTime;
            velocity = Quaternion.Euler(0, 0, 90) * distance;
        }
        velocity.Normalize();
        velocity *= (timer >= waitTime) ? higherSpeed : speed;

        transform.position += velocity * Time.deltaTime;
        
        var vAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb.MoveRotation(vAngle - 90);
    }
}
