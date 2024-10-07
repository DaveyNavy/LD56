using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackCircle : MonoBehaviour
{
    public float knockbackForce = 0f;    // The force applied to ants when they are knocked back
    public float expandSpeed = 7f;        // The speed at which the circle expands
    public float maxRadius = 10f;         // The maximum size the circle can reach
    public float shrinkSpeed = 60f;       // The speed at which the circle shrinks
    public float knockbackDuration = 1f;  // Duration of the knockback effect
    public float knockbackCooldown = 2f;  // Cooldown between expansions
    public CircleCollider2D circleCollider; // Reference to the circle collider
    private Vector3 originalScale;        // Store the original scale of the circle
    private bool canKnockback = true;     // Tracks whether knockback can occur
    private HashSet<Ant> knockedBackAnts = new HashSet<Ant>();  // Track which ants have been knocked back

    public bool knockbackAugmentActive;

    void Start()
    {
        // Store the original scale
        originalScale = transform.localScale;

        // Start the coroutine to handle expansion, shrinking, and cooldown
        if (knockbackAugmentActive){
            StartCoroutine(ExpandAndShrink());
        }
    }

    private IEnumerator ApplyKnockback(Ant ant)
    {
        Rigidbody2D rb = ant.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (ant.transform.position - transform.position).normalized;
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(knockbackDuration);

            // Stop the knockback by resetting velocity
            // rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator ExpandAndShrink()
    {
        while (true)
        {
            Debug.Log("expanding");

            // Reset knocked-back ants for this expansion cycle
            knockedBackAnts.Clear();
            canKnockback = true;

            // Expand phase
            while (transform.localScale.x < maxRadius)
            {
                transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
                circleCollider.radius = transform.localScale.x / 2f;

                // Continuously check for ants within the knockback circle
                ApplyKnockbackToAntsInRadius();

                yield return null;
            }

            // Wait at max size
            yield return new WaitForSeconds(knockbackDuration);

            canKnockback = false; // Disable knockback after the expansion is complete

            // Shrink phase
            Debug.Log("shrinking");
            while (transform.localScale.x > originalScale.x)
            {
                transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
                circleCollider.radius = transform.localScale.x / 2f;

                yield return null;
            }

            // Wait for cooldown before expanding again
            Debug.Log("waiting KB CD = " + knockbackCooldown);
            yield return new WaitForSeconds(knockbackCooldown);
        }
    }

    // Manually check for ants inside the circle's radius and apply knockback
    private void ApplyKnockbackToAntsInRadius()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);
        //Debug.Log(hitColliders);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Ant ant = hitCollider.GetComponent<Ant>();
            if (ant != null && canKnockback && !knockedBackAnts.Contains(ant))
            {
                StartCoroutine(ApplyKnockback(ant));
                knockedBackAnts.Add(ant);  // Knock back each ant only once per expansion cycle
            }
        }
    }
}
