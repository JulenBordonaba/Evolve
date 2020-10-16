using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseAbility : Ability
{
    public bool isAI = false;

    public GameObject laserPrefab;


    public float laserRate = 2f;

    public float destroyTime = 0.5f;

    private Coroutine laserCoroutine;

    public string[] affectedTags;

    public int damage = 5;

    public KeyCode abilityKey;

    public float cooldown = 3f;

    private bool canUse = true;

    private bool isCasting = false;

    public Camera myCamera;

    public LayerMask hitLayers;

    public Transform player;

    public GameObject effectGuide;

    public float attackRangeForAI = 10f;

    private void Start()
    {
        myCamera = Camera.main;
    }

    private void Update()
    {
        if (isAI) return;
        if (!abilityEnabled) return;

        if (Input.GetKeyDown(abilityKey))
        {
            if(canUse)
            {
                if (!isCasting)
                {
                    isCasting = true;
                    effectGuide.SetActive(true);
                }
            }
        }

        if (isCasting)
        {
            Vector3 aimPoint = AimAbility();
            if (aimPoint != Vector3.zero)
            {
                effectGuide.transform.position = aimPoint+ new Vector3(0,0.05f,0);
            }
            if (Input.GetMouseButton(0))
            {
                CastAbility(aimPoint);
                StartCoroutine(Cooldown());
                effectGuide.SetActive(false);
                isCasting = false;
            }
            else if (Input.GetMouseButton(1))
            {
                isCasting = false;
                effectGuide.SetActive(false);
            }
        }

    }

    public Vector3 AimAbility()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, hitLayers))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldown);
        canUse = true;
    }


    public override void OnAbilityEnabled()
    {
        base.OnAbilityEnabled();
        if (isAI)
        {
            laserCoroutine = StartCoroutine(LaserCoroutine());
        }
        if(effectGuide)
        {
            effectGuide.SetActive(false);
        }
        if(!player)
        {
            try
            {
                //chicos, no hagáis esto en casa
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch
            {

            }
        }

    }

    public override void OnAbilityDisabled()
    {
        base.OnAbilityDisabled();

        if (laserCoroutine != null)
        {
            StopCoroutine(laserCoroutine);
        }


        if (effectGuide)
        {
            effectGuide.SetActive(false);
        }

    }


    public void CastAbility(Vector3 castPosition)
    {
        print("Cast Meteorito");
        if (!abilityEnabled) return;
        
        castPosition += new Vector3(0, 0.05f, 0);

        GameObject laser = Instantiate(laserPrefab, castPosition, Quaternion.identity);

        OnTouchDamage onTouchDamage = laser.GetComponent<OnTouchDamage>();

        onTouchDamage.instigator = gameObject;
        onTouchDamage.damage = damage;
        onTouchDamage.affectedTags = affectedTags;


        Destroy(laser, destroyTime);
    }

    IEnumerator LaserCoroutine()
    {
        yield return null;
        while (true)
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            if (Vector3.Distance(transform.position, player.transform.position)<attackRangeForAI)
            {
                
                Vector3 castPosition = player.position;
                CastAbility(castPosition);
                yield return new WaitForSeconds(laserRate);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
            
        }
    }
}
