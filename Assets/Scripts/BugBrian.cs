using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBrian : Enemy
{
    [SerializeField] public float radialSpeed;
    [SerializeField] Vector3 position;

    [SerializeField] public float radius;
    [SerializeField] public float period;

    private float theta = 0f;
    Rigidbody2D rb;

    public BugBrian() : base(2, 2, 10) { }

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update ()
    {
        float angularVelocity = 2 * Mathf.PI / period;
        float vTangential = -angularVelocity * radius * Mathf.Sin(theta);
        theta += angularVelocity * Time.deltaTime;
        if (theta >= 2 * Mathf.PI) theta -= 2 * Mathf.PI;
        float vRadial = - radialSpeed;

        Vector3 distanceFromTarget = (transform.position - new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0)) - position;
        float angle = Mathf.Atan2(distanceFromTarget.y, distanceFromTarget.x);
        
        Vector3 velocity = new Vector3(
            vRadial * Mathf.Cos(angle) - vTangential * Mathf.Sin(angle),
            vRadial * Mathf.Sin(angle) + vTangential * Mathf.Cos(angle),
            0
        );

        transform.position += velocity * Time.deltaTime;
        
        var vAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb.MoveRotation(vAngle - 90);
    }
}
