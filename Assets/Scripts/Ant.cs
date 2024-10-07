using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Enemy
{
    [SerializeField] public float originalSpeed = 0.5f;
    [SerializeField] public float speed;
    [SerializeField] private Vector3 position;
    float angle;
    Animator animator;
    public Ant() : base(1, 1, 5) { }

    //public Ant() : base(1, 1) { }
    
    void Start() 
    {   
        //init original speed as a ref point to a mob's unmodified speed
        originalSpeed = speed;
    }
    private void Awake()
    {
        speed = 0.5f;
        originalSpeed = speed;
        float distance = Vector2.Distance(transform.position, position);
        Vector2 direction = position - transform.position;
        direction.Normalize();
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        transform.Rotate(0, 0, -90);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
        if (GameManager.instance.IsTimeStopped())
        {
            speed = 0;
        }
        else
        {
            speed = Mathf.Min(originalSpeed + 0.1f * (ScoreManager.Score / 100), 3f);
        }
    }
}