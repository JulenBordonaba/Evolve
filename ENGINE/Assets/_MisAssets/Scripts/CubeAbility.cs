using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAbility : MonoBehaviour
{

    public Transform cube;

    public Camera myCamera;


    private Vector3 initialLocalPos;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;

        initialLocalPos = cube.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
