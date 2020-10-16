using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool abilityEnabled = false;
    public string abilityID;
    public Color cubeColor;

    public virtual void OnAbilityEnabled()
    {

    }
    public virtual void OnAbilityDisabled()
    {

    }
}
