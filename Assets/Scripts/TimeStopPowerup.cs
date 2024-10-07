using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeStopPowerup : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    Vector3 bottomLeft;
    Vector3 topRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Move", 0, 2);
        Camera camera = Camera.main;

        bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    private void OnMouseUp()
    {
        GameManager.instance.StopTime();
        Destroy(gameObject);
    }

    private void Move()
    {
        Vector2 movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movementDirection.Normalize();
        float speed = Random.Range(1f, 2f);
        rb.velocity = movementDirection * speed;

        if (transform.position.x < bottomLeft.x || transform.position.x > topRight.x
            || transform.position.y < bottomLeft.y || transform.position.y > topRight.y)
        {
            Destroy(gameObject);
        }
    }
}