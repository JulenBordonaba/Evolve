using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject character;

    private AbilityManager abilityManager;

    private void Start()
    {
        abilityManager = character.GetComponent<AbilityManager>();
    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemies"))
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
