using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDamage : MonoBehaviour
{
    public GameObject instigator;

    public float flickerDuration = 0f;

    public float invulnerabilityTime = 0f;

    public int damage = 5;

    public string[] affectedTags;

    private void OnTriggerEnter(Collider other)
    {
        bool isObjective = false;

        foreach(string s in affectedTags)
        {
            if(other.CompareTag(s))
            {
                isObjective = true;
                break;
            }
        }


        if (isObjective)
        {
            if (other.GetComponentInParent<Health>())
            {
                Health health = other.GetComponentInParent<Health>();

                health.Damage(damage, instigator, flickerDuration, invulnerabilityTime);


            }
        }
    }
}
