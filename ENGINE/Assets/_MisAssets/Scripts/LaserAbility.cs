using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAbility : Ability
{
    public GameObject laserPrefab;

    public Transform spawnPoint;

    public float laserRate = 2f;

    public float destroyTime = 0.5f;

    private Coroutine laserCoroutine;

    public string[] affectedTags;

    public int damage = 5;


    public override void OnAbilityEnabled()
    {
        base.OnAbilityEnabled();
        laserCoroutine = StartCoroutine(LaserCoroutine());

    }

    public override void OnAbilityDisabled()
    {
        base.OnAbilityDisabled();
        if(laserCoroutine!=null)
        {
            StopCoroutine(laserCoroutine);
        }
    }
    

    public void CastLaser()
    {
        if (!abilityEnabled) return;
        GameObject laser = Instantiate(laserPrefab, spawnPoint);

        OnTouchDamage onTouchDamage = laser.GetComponent<OnTouchDamage>();

        onTouchDamage.instigator = gameObject;
        onTouchDamage.damage = damage;
        onTouchDamage.affectedTags = affectedTags;

        laser.transform.localPosition = Vector3.zero;

        Destroy(laser, destroyTime);
    }

    IEnumerator LaserCoroutine()
    {
        while(true)
        {
            CastLaser();
            yield return new WaitForSeconds(laserRate);
        }
    }
}
