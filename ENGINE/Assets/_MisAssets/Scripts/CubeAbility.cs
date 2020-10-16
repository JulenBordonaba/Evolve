using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAbility : Ability
{

    public Transform cube;

    public Camera myCamera;

    public float distanceTravelledPerSecond = 5f;

    public LayerMask hitLayers;

    public OnTouchDamage onTouchDamage;

    private Vector3 initialLocalPos;

    private bool isReturning = false;

    private bool isGoing = false;

    private bool canMove = true;

    private Transform target;

    //Tween returnTween;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;

        initialLocalPos = cube.localPosition;
    }

    // Update is called once per frame
    void Update()
    {


        if(isReturning)
        {
            Return();
        }

        if(isGoing)
        {
            Go();
        }

        if (!abilityEnabled) return;
        if (Input.GetMouseButtonDown(0))
        {
            if(canMove)
            {
                SetDestination();
            }
        }

    }

    public void Go()
    {
        float travelDist = Vector3.Distance(cube.position, target.position);

        float movementTime = travelDist / distanceTravelledPerSecond;

        Tween myTween = cube.DOMove(target.position, movementTime);

        myTween.OnComplete(OnCubeGone);
    }

    public override void OnAbilityDisabled()
    {
        base.OnAbilityDisabled();
        //cube.gameObject.SetActive(false);
        onTouchDamage.enabled = false;
    }

    public override void OnAbilityEnabled()
    {
        base.OnAbilityEnabled();
        //cube.gameObject.SetActive(true);
        onTouchDamage.enabled = true;
    }

    void SetDestination()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;


        if(Physics.Raycast(ray,out hit, float.MaxValue,hitLayers))
        {

            canMove = false;

            isGoing = true;

            target = hit.transform;
        }
    }

    void OnCubeGone()
    {
        isReturning = true;

        isGoing = false;
    }

    void Return()
    {

        float travelDist = Vector3.Distance(cube.position, cube.parent.position + initialLocalPos);

        float movementTime = travelDist / distanceTravelledPerSecond;

        Tween returnTween = cube.DOMove(cube.parent.position + initialLocalPos, movementTime);

        returnTween.OnComplete(OnCubeReturned);
        
    }


    void OnCubeReturned()
    {
        isReturning = false;
        canMove = true;
    }
}
