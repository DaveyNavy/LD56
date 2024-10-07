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
    bool hundredMark = false;
    bool threehundredMark = false;
    bool sixhundredMark = false;
    bool eighthundred = false;

    void Update()
    {
        if(ScoreManager.Score >= 100 && !hundredMark)
        {
            hundredMark = true;
            //Debug.Log("!!!!!!!!!!!Score > 100, Slow time!!!!!!!!!!");
            //ApplyAugment(AugmentType.Slow);
            ApplyAugment(AugmentType.Slow);
            ApplyAugment(AugmentType.Knockback);
            ApplyAugment(AugmentType.Knockback);
        }
        if (ScoreManager.Score >= 300 && !threehundredMark)
        {
            threehundredMark = true;
            //Debug.Log("!!!!!!!!!!!Score > 300, SLOW + KB time!!!!!!!!!!");
            ApplyAugment(AugmentType.Slow);
            ApplyAugment(AugmentType.Knockback);
            ApplyAugment(AugmentType.RapidSpeed);
        }
        if (ScoreManager.Score >= 600 && !sixhundredMark)
        {
            sixhundredMark = true;
            //Debug.Log("!!!!!!!!!!!Score > 600, Slow+KB+SPEEDUP time!!!!!!!!!!");
            ApplyAugment(AugmentType.Slow);
            ApplyAugment(AugmentType.Knockback);
            ApplyAugment(AugmentType.RapidSpeed);
        }
        if (ScoreManager.Score >= 800 && !eighthundred)
        {
            eighthundred = true;
            //Debug.Log("!!!!!!!!!!!Score > 600, Slow+KB+SPEEDUP time!!!!!!!!!!");
            ApplyAugment(AugmentType.Slow);
            ApplyAugment(AugmentType.Knockback);
            ApplyAugment(AugmentType.RapidSpeed);
        }
    }
    private void Start() 
    {
        knockbackForce = 0;

        // Debug.Log("///////////original CDs:" + knockbackCooldown +"  "+ slowCooldown);
        ApplyAugment(AugmentType.Slow);  // Apply slow effect for testing
        ApplyAugment(AugmentType.Knockback);
        // Debug.Log("////////original CDs:" + knockbackCooldown + "  " + slowCooldown);
        // ApplyAugment(AugmentType.RapidSpeed);
        // ApplyAugment(AugmentType.RapidSpeed);
        // ApplyAugment(AugmentType.RapidSpeed);
        // ApplyAugment(AugmentType.RapidSpeed);
        // ApplyAugment(AugmentType.RapidSpeed);

        // Debug.Log("///////4x SPEED:" + knockbackCooldown +"  "+ slowCooldown);
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
        // Debug.Log("applying KB by:");

        if (augPoints[1] == 0) 
        {
            // Debug.Log("initiating KB");
            // Initializing knockback augment for the first time
            augPoints[1] = 1;
            knockbackForce = 2;
            knockbackCooldown = 4f; 

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
                // Debug.LogError("Failed to instantiate knockback circle prefab.");
                return;
            }

            // Get the KnockbackCircle component
            KnockbackCircle knockbackCircleScript = knockbackCircleObj.GetComponent<KnockbackCircle>();
                // Check if the prefab has the required components
            if (knockbackCircleScript == null)
            {
                // Debug.LogError("Failed to instantiate knockback circle script.");
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
            // Debug.Log("upgrading KB");

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
        //each slow should occur for a NON SCALING Flat 1.5s
        if (augPoints[3] == 0) 
        {
            // Initializing slow augment for the first time
            augPoints[3] = 1;
            slowStrength = 0.35f;
            slowCooldown = 4f; // 4 seconds cooldown

            // If we already have the speed buff active, adjust this skill's cooldown
            if (augPoints[4] != 0) {
                slowCooldown = 7f - augPoints[4];
            }

            //Debug.Log("coroutine calledd for slow");
            //initiate the coroutine for applying the slow debuff once it is first activated
            StartSlowDebuffCoroutine();
        }
        else if (augPoints[3] < 5) 
        {
            // Buffing existing slow augment
            augPoints[3]++;
            slowStrength += 0.03f;
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
                // Debug.Log("ACTIVE CIRLCE CD:" + activeKnockbackCircle.knockbackCooldown);
            }
        }
        else if (augPoints[4] < 5) 
        {
            // Buffing existing speed augment
            augPoints[4]++;
            if (augPoints[1] > 0) knockbackCooldown -= 0.2f; // Only adjust if Knockback is active
            if (augPoints[3] > 0) slowCooldown -= 0.2f; // Only adjust if Slow is active

            //PROBLEM: Does not seem like the CD reduction is being propogated to KB script
            if (activeKnockbackCircle != null)
            {
                activeKnockbackCircle.knockbackCooldown = knockbackCooldown;
                // Debug.Log("ACTIVE CIRLCE CD:" + activeKnockbackCircle.knockbackCooldown);

            }
        }
    }

    //=================================================================//
    //========   APPLYING THE BUFF/DEBUFF TO ACTIVE ENEMIES   ========//
    //===============================================================//
    private void StartSlowDebuffCoroutine()
    {
        if (!slowAugmentActive)
        {
            slowAugmentActive = true;
            StartCoroutine(ApplySlowRepeatedly());
        }
    }

    // Coroutine to apply the slow debuff at regular intervals (based on slowCooldown)
   private IEnumerator ApplySlowRepeatedly()
    {
        while (true)
        {
            Ant[] ants = FindObjectsOfType<Ant>();
            // Slow down all ants
            foreach (Ant ant in ants)
            {
                // Debug.Log($"Applying slow to ant: {ant}, current speed: {ant.speed}, original speed: {ant.originalSpeed}");
                ant.speed = Mathf.Max(0.5f - slowStrength, 0);  // Apply slow, ensuring the speed doesn't go negative
            }

            // Wait for the duration of the slow debuff
            yield return new WaitForSeconds(3f);

            // Restore the original speed to all ants
            foreach (Ant ant in ants)
            {
                ant.speed = 0.5f;  // Restore individual original speed
                // Debug.Log($"Restoring speed of ant: {ant}, restored speed: {ant.speed}");
            }

            // Wait for the cooldown before applying the slow debuff again
            yield return new WaitForSeconds(slowCooldown);
        }
    }
}
