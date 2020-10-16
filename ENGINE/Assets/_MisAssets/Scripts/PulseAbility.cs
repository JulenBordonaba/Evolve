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

    private void Start()
    {
        //chicos, no hagáis esto en casa
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myCamera = Camera.main;
    }

    private void Update()
    {
        if (isAI) return;
        if (!abilityEnabled) return;

        if (Input.GetKeyDown(abilityKey))
        {

            if (!isCasting)
            {
                isCasting = true;
                effectGuide.SetActive(true);
            }
        }

        if (isCasting)
        {
            //código de cancelar
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Vector3 aimPoint = AimAbility();
            if (aimPoint != Vector3.zero)
            {
                effectGuide.transform.position = aimPoint;
            }
            if (Input.GetMouseButton(0))
            {
                CastAbility(aimPoint);
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

        effectGuide.SetActive(false);

    }

    public override void OnAbilityDisabled()
    {
        base.OnAbilityDisabled();

        if (laserCoroutine != null)
        {
            StopCoroutine(laserCoroutine);
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }


    public void CastAbility(Vector3 castPosition)
    {
        if (!abilityEnabled) return;
        GameObject laser = Instantiate(laserPrefab, castPosition, Quaternion.identity);

        OnTouchDamage onTouchDamage = laser.GetComponent<OnTouchDamage>();

        onTouchDamage.instigator = gameObject;
        onTouchDamage.damage = damage;
        onTouchDamage.affectedTags = affectedTags;

        laser.transform.localPosition = Vector3.zero;

        Destroy(laser, destroyTime);
    }

    IEnumerator LaserCoroutine()
    {
        while (true)
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            Vector3 castPosition = player.position;
            yield return new WaitForSeconds(0.5f);
            CastAbility(castPosition);
            yield return new WaitForSeconds(laserRate - 0.5f);
        }
    }
}
