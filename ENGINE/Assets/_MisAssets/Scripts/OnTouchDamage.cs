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

    public bool disabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (disabled) return;
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
            
            if (instigator.CompareTag("Player"))
            {
                
                if (other.GetComponentInParent<Ability>())
                {
                    Ability ability = other.GetComponentInParent<Ability>();

                    print(ability.abilityID);
                    if (instigator.GetComponentInParent<AbilityManager>())
                    {
                        AbilityManager abilityManager = instigator.GetComponentInParent<AbilityManager>();
                        abilityManager.DisableAllAbilities();
                        abilityManager.EnableAbility(ability.abilityID);
                    }

                }
            }

            if (other.GetComponentInParent<Health>())
            {
                Health health = other.GetComponentInParent<Health>();

                health.Damage(damage, instigator, flickerDuration, invulnerabilityTime);



            }
        }
    }
}
