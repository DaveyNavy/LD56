// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class EnemyErratic : Enemy
// // {
// //     [SerializeField] public float netSpeed;
// //     [SerializeField] public float noiseSpeed;
// //     [SerializeField] public Vector3 position;

// //     Rigidbody2D rb;
// //     float theta;

// //     EnemyErratic() : base(5, 2, 25) { }

// //     void Awake()
// //     {
// //         rb = GetComponent<Rigidbody2D>();
// //         theta = Random.Range(0, 2 * Mathf.PI);
// //     }

// //     void Update()
// //     {
// //         Vector3 velocity = position - transform.position;
// //         velocity.Normalize();
// //         velocity *= netSpeed;
// //         theta += Random.Range(-0.1f, 0.1f);
// //         velocity.x += noiseSpeed * Mathf.Cos(theta);
// //         velocity.y += noiseSpeed * Mathf.Sin(theta);

// //         if (!GameManager.instance.IsTimeStopped())
// //             transform.position += velocity * Time.deltaTime;

// //         var vAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
// //         rb.MoveRotation(vAngle - 90);
// //     }
// // }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyErratic : Enemy
// {
//     [SerializeField] public float netSpeed;
//     [SerializeField] public float noiseSpeed;
//     [SerializeField] public Vector3 targetPosition;

//     Rigidbody2D rb;
//     float theta;

//     EnemyErratic() : base(5, 2, 25) { }

//     void Awake()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         theta = Random.Range(0, 2 * Mathf.PI);
//     }

//     void Update()
//     {
//         if (GameManager.instance.IsTimeStopped())
//             return;

//         // Calculate base velocity towards the target position
//         Vector3 direction = targetPosition - transform.position;
//         direction.Normalize();
//         Vector3 baseVelocity = direction * netSpeed;

//         // Add erratic noise to the movement
//         theta += Random.Range(-0.1f, 0.1f);  // Slight random change in angle
//         baseVelocity.x += noiseSpeed * Mathf.Cos(theta);
//         baseVelocity.y += noiseSpeed * Mathf.Sin(theta);

//         // Move using Rigidbody2D's velocity to allow physics interaction
//         rb.velocity = baseVelocity;

//         // Calculate rotation based on movement direction
//         float angle = Mathf.Atan2(baseVelocity.y, baseVelocity.x) * Mathf.Rad2Deg;
//         rb.MoveRotation(angle - 90);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyErratic : Enemy
{
    [SerializeField] public float netSpeed;    // Base movement speed towards the target
    [SerializeField] public float noiseSpeed;  // Erratic movement noise
    [SerializeField] public Vector3 targetPosition;  // The position the enemy is moving towards
    private Rigidbody2D rb;
    private float theta;

    EnemyErratic() : base(5, 2, 25) { }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        theta = Random.Range(0, 2 * Mathf.PI);
    }

    void FixedUpdate()
    {
        if (GameManager.instance.IsTimeStopped())
            return;

        // Calculate the direction and speed towards the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 velocity = direction * netSpeed;

        // Add erratic movement
        theta += Random.Range(-0.1f, 0.1f);  // Randomize the angle for erratic movement
        velocity.x += noiseSpeed * Mathf.Cos(theta);
        velocity.y += noiseSpeed * Mathf.Sin(theta);

        // Apply velocity to Rigidbody2D, allowing it to interact with forces like knockback
        rb.velocity = velocity;

        // Rotate the enemy based on the movement direction
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle - 90);  // Rotate the enemy to face the direction of movement
    }
}
