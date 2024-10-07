using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyErratic : Enemy
{
    [SerializeField] public float netSpeed;
    [SerializeField] public float noiseSpeed;
    [SerializeField] public Vector3 position;

    Rigidbody2D rb;
    float theta;

    EnemyErratic() : base(5, 2, 25) { }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        theta = Random.Range(0, 2 * Mathf.PI);
    }

    void Update()
    {
        Vector3 velocity = position - transform.position;
        velocity.Normalize();
        velocity *= netSpeed;
        theta += Random.Range(-0.1f, 0.1f);
        velocity.x += noiseSpeed * Mathf.Cos(theta);
        velocity.y += noiseSpeed * Mathf.Sin(theta);

        transform.position += velocity * Time.deltaTime;

        var vAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb.MoveRotation(vAngle - 90);
    }
}