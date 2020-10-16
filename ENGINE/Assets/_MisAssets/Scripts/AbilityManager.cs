using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability>();

    public string[] startingAbilityIDs;

    public Renderer cubeRenderer;


    // Start is called before the first frame update
    void Start()
    {
        abilities = new List<Ability>( GetComponents<Ability>());

        DisableAllAbilities();
        for (int i = 0; i < startingAbilityIDs.Length; i++)
        {
            EnableAbility(startingAbilityIDs[i]);
        }

    }
    
    public void EnableAbility(string _id)
    {
        Ability abilityToEnable = GetAbilityByID(_id);


        abilityToEnable.abilityEnabled = true;
        if(cubeRenderer)
        {
            cubeRenderer.material.color = abilityToEnable.cubeColor;
        }
        abilityToEnable.OnAbilityEnabled();
    }

    public void DisableAllAbilities()
    {
        foreach(Ability ability in abilities)
        {
            ability.abilityEnabled = false;
            ability.OnAbilityDisabled();
        }
    }
    

    public Ability GetAbilityByID(string _id)
    {
        if (abilities == null) return null;

        if (abilities.Count <= 0) return null;

        foreach(Ability ability in abilities)
        {
            if(ability.abilityID == _id)
            {
                return ability;
            }
        }


        return null;
    }
}
