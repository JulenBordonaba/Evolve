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

    private Vector3 initialLocalPos;

    private bool isReturning = false;

    private bool canMove = true;

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

        if (!enabledAbility) return;

        if(isReturning)
        {
            Return();
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(canMove)
            {
                SetDestination();
            }
        }

    }

    void SetDestination()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;


        if(Physics.Raycast(ray,out hit, float.MaxValue,hitLayers))
        {

            canMove = false;

            float travelDist = Vector3.Distance(cube.position,hit.point);

            float movementTime = travelDist / distanceTravelledPerSecond;

            Tween myTween = cube.DOMove(hit.point, movementTime);

            myTween.OnComplete(OnCubeGone);
        }
    }

    void OnCubeGone()
    {
        print("OnCubeGone");
        isReturning = true;
    }

    void Return()
    {
        print("REturn");

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
