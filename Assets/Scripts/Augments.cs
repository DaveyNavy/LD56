using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : MonoBehaviour
{
    public enum AugmentType { AOE, Knockback, Spray, Slow, RapidSpeed }

    [SerializeField] private int[] augPoints = new int[5]; // Points for each augment type
    public int knockbackForce = 0;
    public float knockbackCooldown = 1f;
    public float slowStrength = 0f;
    public float slowCooldown = Mathf.Infinity;
    //Active Augment Bools
    public bool slowAugmentActive = false;
    public bool knockbackAugmentActive = false;

    //Knockback prefab vars
    public GameObject knockbackCirclePrefab;  // Reference to the knockback circle prefab
    public Transform foodTransform;  // Reference to the food's position
    private KnockbackCircle activeKnockbackCircle;
    private void Start() 
    {
        knockbackForce = 0;

        Debug.Log("///////////original CDs:" + knockbackCooldown +"  "+ slowCooldown);
        ApplyAugment(AugmentType.Slow);  // Apply slow effect for testing
        ApplyAugment(AugmentType.Knockback);
        Debug.Log("////////original CDs:" + knockbackCooldown + "  " + slowCooldown);
        ApplyAugment(AugmentType.RapidSpeed);
        ApplyAugment(AugmentType.RapidSpeed);
        ApplyAugment(AugmentType.RapidSpeed);
        ApplyAugment(AugmentType.RapidSpeed);
        Debug.Log("///////4x SPEED:" + knockbackCooldown +"  "+ slowCooldown);
    }
    //===================================================================//
    //========  ADJUSTING THE AUGMENT STRENGTHS BASED ON POINTS ========//
    //=================================================================//
    public void ApplyAugment(AugmentType augmentType)
    {
        switch (augmentType)
        {
            case AugmentType.Knockback:
                ApplyKnockbackBuff();
                break;
            case AugmentType.Slow:
                ApplySlowDebuff();
                break;
            case AugmentType.RapidSpeed:
                ApplySpeedBuff();
                break;
        }
    }
    private void ApplyKnockbackBuff()
    {
        Debug.Log("applying KB by:");

        if (augPoints[1] == 0) 
        {
            Debug.Log("initiating KB");
            // Initializing knockback augment for the first time
            augPoints[1] = 1;
            knockbackForce = 3;
            knockbackCooldown = 7f; // 7 seconds cooldown

            if (augPoints[4] != 0) 
            {
                knockbackCooldown = 8f - augPoints[4];
            }
            knockbackAugmentActive = true;

            // Instantiate the knockback circle at the food's position
            GameObject knockbackCircleObj = Instantiate(knockbackCirclePrefab, foodTransform.position, Quaternion.identity);
            
            // Check if the prefab has the required components
            if (knockbackCircleObj == null)
            {
                Debug.LogError("Failed to instantiate knockback circle prefab.");
                return;
            }

            // Get the KnockbackCircle component
            KnockbackCircle knockbackCircleScript = knockbackCircleObj.GetComponent<KnockbackCircle>();
                // Check if the prefab has the required components
            if (knockbackCircleScript == null)
            {
                Debug.LogError("Failed to instantiate knockback circle script.");
                return;
            }
            knockbackCircleScript.knockbackForce = knockbackForce;
            knockbackCircleScript.knockbackCooldown = knockbackCooldown;

            activeKnockbackCircle = knockbackCircleScript;  // Track the active circle

            // activeKnockbackCircle.knockbackAugmentActive = knockbackAugmentActive;
            // Debug.Log("coroutine called for KB");


        }
        else if (augPoints[1] < 5) 
        {
            Debug.Log("upgrading KB");

            // Buffing existing knockback augment
            augPoints[1]++;
            knockbackForce++;
            if (activeKnockbackCircle != null)
            {
                activeKnockbackCircle.knockbackForce = knockbackForce;
            }
        }
    }
    private void ApplySlowDebuff()
    {
        // Stop the knockback if it's active
        if (activeKnockbackCircle != null)
        {
            Debug.Log("Stopping knockback effect.");
            Destroy(activeKnockbackCircle.gameObject);  // Remove the active knockback circle
            activeKnockbackCircle = null;
        }

        // Debug to check if any KnockbackCircle is left active
        KnockbackCircle[] knockbackCircles = FindObjectsOfType<KnockbackCircle>();
        Debug.Log("Number of active KnockbackCircles: " + knockbackCircles.Length);
        //each slow should occur for a NON SCALING Flat 1.5s
        if (augPoints[3] == 0) 
        {
            // Initializing slow augment for the first time
            augPoints[3] = 1;
            slowStrength = 0.55f;
            slowCooldown = 6f; // 4 seconds cooldown

            // If we already have the speed buff active, adjust this skill's cooldown
            if (augPoints[4] != 0) {
                slowCooldown = 7f - augPoints[4];
            }

            Debug.Log("coroutine calledd for slow");
            //initiate the coroutine for applying the slow debuff once it is first activated
            slowAugmentActive = true;
            StartSlowDebuffCoroutine();
        }
        else if (augPoints[3] < 5) 
        {
            // Buffing existing slow augment
            augPoints[3]++;
            slowStrength += 0.05f;
        }
    }
    private void ApplySpeedBuff()
    {
        if (augPoints[4] == 0) 
        {
            // Initializing speed augment for the first time
            augPoints[4] = 1;
            knockbackCooldown -= 1; //this needs to also be set for script obj
            slowCooldown -= 1;

            if (activeKnockbackCircle != null)
            {
                activeKnockbackCircle.knockbackCooldown = knockbackCooldown;
                Debug.Log("ACTIVE CIRLCE CD:" + activeKnockbackCircle.knockbackCooldown);
            }
        }
        else if (augPoints[4] < 5) 
        {
            // Buffing existing speed augment
            augPoints[4]++;
            if (augPoints[1] > 0) knockbackCooldown -= 1; // Only adjust if Knockback is active
            if (augPoints[3] > 0) slowCooldown -= 1; // Only adjust if Slow is active

            //PROBLEM: Does not seem like the CD reduction is being propogated to KB script
            if (activeKnockbackCircle != null)
            {
                activeKnockbackCircle.knockbackCooldown = knockbackCooldown;
                Debug.Log("ACTIVE CIRLCE CD:" + activeKnockbackCircle.knockbackCooldown);

            }
        }
    }

    //=================================================================//
    //========   APPLYING THE BUFF/DEBUFF TO ACTIVE ENEMIES   ========//
    //===============================================================//
    private void StartSlowDebuffCoroutine()
    {
        StartCoroutine(ApplySlowRepeatedly());
    }

    // Coroutine to apply the slow debuff at regular intervals (based on slowCooldown)
    private IEnumerator ApplySlowRepeatedly()
    {
        while (true)
        {
            //SLOW ANTS BY SLOW STRENGTH
            Ant[] ants = FindObjectsOfType<Ant>();
            foreach (Ant ant in ants)
            {
                Debug.Log("Applying slow to ant:" + ant);
                //ant.ApplySlow(slowStrength);  // Apply slow to each ant
                Debug.Log("SLOW current Speed:" + ant.speed + " original speed:" + ant.originalSpeed);

                ant.speed -= slowStrength;
            }

            //APPLY THE SLOW FOR THE DURATION TIME
            yield return new WaitForSeconds(3f);  // Wait for the slow duration
            
            //RESTORE ANT SPEED TO REGULAR SPEED
            foreach (Ant ant in ants)
            {
                Debug.Log("RESTORING current Speed:" + ant.speed + " original speed:" + ant.originalSpeed);

                ant.speed = ant.originalSpeed;
            }

            Debug.Log("waiting for cooldown time");
            // Wait for the cooldown before applying the slow debuff again
            yield return new WaitForSeconds(slowCooldown);
        }
    }
}
