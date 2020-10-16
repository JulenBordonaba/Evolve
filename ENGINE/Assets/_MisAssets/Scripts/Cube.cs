using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject character;

    private AbilityManager abilityManager;

    public CubeAbility cubeAbility;

    public OnTouchDamage onTouchDamage;

    private void Start()
    {
        abilityManager = character.GetComponent<AbilityManager>();
    }


    private void Update()
    {
        onTouchDamage.disabled = !cubeAbility.abilityEnabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!cubeAbility.abilityEnabled) return;
        if(other.CompareTag("Enemy"))
        {
            if(other.GetComponentInParent<Ability>())
            {
                Ability ability = other.GetComponentInParent<Ability>();

                abilityManager.DisableAllAbilities();
                abilityManager.EnableAbility(ability.abilityID);
                

            }

            
        }
    }
}
