using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Enemy
{
    [SerializeField] private GameObject food;
    [SerializeField] private float speed; 

    public Ant() : base(1, 1) { }

    // Update is called once per frame
    void Update()
    {
        float distance  =  Vector2.Distance(transform.position, food.transform.position);
        Vector2 direction = food.transform.position - transform.position;
        direction.Normalize(); 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        transform.position = Vector2.MoveTowards(this.transform.position, food.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle); 
    }
}
