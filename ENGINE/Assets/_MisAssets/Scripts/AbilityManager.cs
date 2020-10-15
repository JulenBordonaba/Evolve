using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability>();

    public string startingAbilityID;




    // Start is called before the first frame update
    void Start()
    {
        abilities = new List<Ability>( GetComponents<Ability>());

        DisableAllAbilities();

        EnableAbility(startingAbilityID);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAbility(string _id)
    {
        Ability abilityToEnable = GetAbilityByID(_id);


        abilityToEnable.enabledAbility = true;
    }

    public void DisableAllAbilities()
    {
        foreach(Ability ability in abilities)
        {
            ability.enabledAbility = false;
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
