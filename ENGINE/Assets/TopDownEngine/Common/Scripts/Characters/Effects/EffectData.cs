using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "Effects/Create Effect")]
public class EffectData : ScriptableObject
{
    public string id;
    public float duration;
    public Sprite icon;


    [Header("Damage Reduction")]

    [Tooltip("A Damage reduction percentaje modifier")]
    public float damageReduction;


    [Header("Health Modifiers")]

    [Tooltip("This modifier changes the characters max health in X value")]
    public int maxHealth;
    [Tooltip("This modifier is a multiplier to the characters max health")]
    public float maxHealthMultiplier = 1;


    [Header("Movement modifiers")]

    [Tooltip("This modifier penalizes the characters movement speed in X percentaje (0 - 100) it is the last modifier being applied to the movement speed.")]
    public float slow;
    [Tooltip("This modifier changes the characters movement speed in X value")]
    public float movementSpeed;
    [Tooltip("This modifier is a multiplier to the characters movement speed")]
    public float movementSpeedMultiplier = 1;


    [Header("Attack Speed Modifiers")]

    [Tooltip("This modifier changes the characters movement in X value")]
    public float attackSpeed;
    [Tooltip("This modifier is a multiplier to the characters max health")]
    public float attackSpeedMultiplier = 1;


    //public float attackDistance;
    //public float attackDamage;
    //public Color effectColor = Color.white;


    //public float maneuverability;
    //public float acceleration;
    //public float shotDamage;
    //[Tooltip("Invierte los controles de la nave")]
    //public bool invertControls = false;
    [Tooltip("Impide usar las habilidades")]
    public bool silenceAbilities = false;
    //[Tooltip("Impide usar las combustibles")]
    //public bool silenceFuels = false;
    //[Tooltip("Activar si el efecto no es permanente")]
    

    public bool permanent = false;
    public DOT dot = new DOT();
    public AudioClip effectClip;

    #region PhotonSerialize
    public byte classId { get; set; }

    public static object Deserialize(byte[] data)
    {
        EffectData result = new EffectData();
        result.classId = data[0];
        return result;
    }

    public static byte[] Serialize(object customType)
    {
        EffectData c = (EffectData)customType;
        return new byte[] { c.classId };
    }
    #endregion
}

[System.Serializable]
public class DOT
{

    public DOT()
    {
        loopTime = 0;
        damagePerTick = 0;
        dotEffect = null;
    }
    public float loopTime;
    public float damagePerTick;
    public Coroutine dotEffect;
}
