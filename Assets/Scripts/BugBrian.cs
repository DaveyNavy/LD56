using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBrianMovement : MonoBehaviour
{
    [SerializeField] public float radialSpeed;
    [SerializeField] public Rigidbody2D target;

    [SerializeField] public float radius;
    [SerializeField] public float period;
    Rigidbody2D rb;

    private float theta = 0f;

    void Start ()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    void Update ()
    {
        float angularVelocity = 2 * Mathf.PI / period;
        float vTangential = -angularVelocity * radius * Mathf.Sin(theta);
        theta += angularVelocity * Time.deltaTime;
        if (theta >= 2 * Mathf.PI) theta -= 2 * Mathf.PI;
        float vRadial = - radialSpeed;

        Vector3 distanceFromTarget = (transform.position - new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0)) - target.transform.position;
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
