using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : MonoBehaviour
{
    public enum AugmentType { Wave, Knockback, Spray, Slow, RapidSpeed }

    [SerializeField] private int[] augPoints = new int[5]; // Points for each augment type
    public int waveRadius = 0;
    public float waveDmg = 0f;
    public int knockbackForce = 0;
    public float knockbackCooldown = Mathf.Infinity;
    public float sprayCooldown = Mathf.Infinity;
    public float sprayDmg = 0f;
    public float sprayLingerTime = 0f;
    public int slowStrength = 0;
    public float slowCooldown = Mathf.Infinity;
    public int speedReduction = 0;

    public void ApplyAugment(AugmentType augmentType)
    {
        switch (augmentType)
        {
            case AugmentType.Wave:
                ApplyWaveBuff();
                break;
            case AugmentType.Knockback:
                ApplyKnockbackBuff();
                break;
            case AugmentType.Spray:
                ApplySprayBuff();
                break;
            case AugmentType.Slow:
                ApplySlowDebuff();
                break;
            case AugmentType.RapidSpeed:
                ApplySpeedBuff();
                break;
        }
    }

    private void ApplyWaveBuff()
    {
        if (augPoints[0] == 0) 
        {
            // Initializing wave augment for the first time
            augPoints[0] = 1;
            waveRadius = 1;
            waveDmg = 0.25f;
        }
        else if (augPoints[0] < 5) 
        {
            // Buffing existing wave augment
            augPoints[0]++;
            waveRadius++;
            waveDmg++;
        }
    }

    private void ApplyKnockbackBuff()
    {
        if (augPoints[1] == 0) 
        {
            // Initializing knockback augment for the first time
            augPoints[1] = 1;
            knockbackForce = 3;
            knockbackCooldown = 8f; // 8 seconds cooldown

             // If we already have the speed buff active, adjust this skill's cooldown
            if (augPoints[4] != 0) {
                knockbackCooldown = 8f - augPoints[4];
            }
        }
        else if (augPoints[1] < 5) 
        {
            // Buffing existing knockback augment
            augPoints[1]++;
            knockbackForce++;
        }
    }

    private void ApplySprayBuff()
    {
        if (augPoints[2] == 0) 
        {
            // Initializing spray augment for the first time
            augPoints[2] = 1;
            sprayDmg = 0.25f;
            sprayLingerTime = 0.5f;
            sprayCooldown = 6f; // 6 seconds cooldown

            // If we already have the speed buff active, adjust this skill's cooldown
            if (augPoints[4] != 0) {
                sprayCooldown = 6f - augPoints[4];
            }
        }
        else if (augPoints[2] < 5) 
        {
            // Buffing existing spray augment
            augPoints[2]++;
            sprayDmg++;
            sprayLingerTime++;
        }
    }

    private void ApplySlowDebuff()
    {
        if (augPoints[3] == 0) 
        {
            // Initializing slow augment for the first time
            augPoints[3] = 1;
            slowStrength = 1;
            slowCooldown = 7f; // 7 seconds cooldown
            // If we already have the speed buff active, adjust this skill's cooldown
            if (augPoints[4] != 0) {
                slowCooldown = 7f - augPoints[4];
            }
        }
        else if (augPoints[3] < 5) 
        {
            // Buffing existing slow augment
            augPoints[3]++;
            slowStrength++;
        }
    }

    private void ApplySpeedBuff()
    {
        if (augPoints[4] == 0) 
        {
            // Initializing speed augment for the first time
            augPoints[4] = 1;
            knockbackCooldown -= 1;
            sprayCooldown -= 1;
            slowCooldown -= 1;
        }
        else if (augPoints[4] < 5) 
        {
            // Buffing existing speed augment
            augPoints[4]++;
            if (augPoints[1] > 0) knockbackCooldown -= 1; // Only adjust if Knockback is active
            if (augPoints[2] > 0) sprayCooldown -= 1; // Only adjust if Spray is active
            if (augPoints[3] > 0) slowCooldown -= 1; // Only adjust if Slow is active
        }
    }
}
